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
            var ip = new InjectProcess();
            ip.Inject(user, Environment.CurrentDirectory + @"\LogonShell.exe", arg);
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
