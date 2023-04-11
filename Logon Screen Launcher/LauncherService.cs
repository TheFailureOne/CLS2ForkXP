using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Threading;

namespace ClassicLogonShell
{
    public partial class LauncherService : ServiceBase
    {
        public bool shouldStart;
        public LauncherService()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            CanHandleSessionChangeEvent = true;
            CanHandlePowerEvent = true;
        }

        public static void startProcess(string arg = null, string user = @"WinSta0\Winlogon")
        {
            try
            {
                string certPK = "3082010A0282010100B0C0516ABAE078E51C9BACEC9034937423EFB46E20120A4A4ABB8A5CC534AE999F132E8CAE533BACB8B8FA54686390CCB73294D731E86F5A99BA00FE851D31E8D48662CA818FE4549BD6896043ECA890C6925F4CA8E1A70782D61AFE167A7916AE82E9393854C6FAFDFB86A212C9D1E1309F86344BA06B795AB0982AA06CF0A4018A1C6356DEC54353CD6AEEC25A20F93BB28E1326BACD140775E1AD97A796D8F8F8FB154FD9A89E26687136CD14A55BF0937D8DBF6B2D59B9E8B33CEA839BDB30A12CBD6BB822D0CE588001C620C7F921812E0CAAFBB9E818AB4E0824EDB068D8EED1D89F9CC40E5588C278C47A504FFCE71CEFA9F9F52529CBB512E32497A10203010001";
                var cert = X509Certificate.CreateFromSignedFile(Environment.CurrentDirectory + "\\LogonShell.exe");
                if (cert.GetPublicKeyString() == certPK)
                {
                    var ip = new InjectProcess();
                    ip.Inject(user, Environment.CurrentDirectory + "\\LogonShell.exe", arg);
                }
                else
                    File.WriteAllText(Environment.CurrentDirectory + "\\log.txt", DateTime.Now.ToString() + " Critical error: LogonShell.exe signature mismatch!\nExpected public key: " + certPK + "\nReceived public key" + cert.GetPublicKeyString());

            }
            catch (Exception)
            {
                File.WriteAllText(Environment.CurrentDirectory + "\\log.txt", DateTime.Now.ToString() + " Critical error: LogonShell.exe signature missing!");
            }
        }

        public static void stopProcess()
        {
            var ip = new InjectProcess();
            ip.TerminateSystemProcess(Environment.CurrentDirectory + "\\LogonShell.exe");
        }

        protected override void OnStart(string[] args)
        {
            startProcess("-startup");
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            if (changeDescription.Reason == SessionChangeReason.SessionLock)
                startProcess();

            if (changeDescription.Reason == SessionChangeReason.SessionLogoff)
            {
                if (File.Exists(Environment.CurrentDirectory + "\\shutdown"))
                {
                    startProcess("-shutdown");
                    File.Delete(Environment.CurrentDirectory + "\\shutdown");
                }
                else
                    shouldStart = true;
            }

            if (changeDescription.Reason == SessionChangeReason.SessionLogon)
            {
                Process p = Process.GetProcessesByName("LogonUI").FirstOrDefault();
                do
                {
                    p = Process.GetProcessesByName("LogonUI").FirstOrDefault();
                    Thread.Sleep(100);
                } while (p != null);
                startProcess("-tray", "winsta0\\default");
                shouldStart = false;
            }

            if (changeDescription.Reason == SessionChangeReason.ConsoleConnect && shouldStart)
            {
                startProcess();
            }

        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            if (powerStatus.HasFlag(PowerBroadcastStatus.ResumeSuspend))
            {
                stopProcess();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
                ManagementObjectCollection collection = searcher.Get();
                string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
                if (username == null)
                    startProcess("-startup");
                else
                    startProcess();
            }

            return base.OnPowerEvent(powerStatus);
        }
    }
}
