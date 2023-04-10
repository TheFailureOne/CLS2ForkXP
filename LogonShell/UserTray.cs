using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace LogonShell
{
    internal class UserTray : ApplicationContext
    {
        public UserTray()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            SystemEvents.SessionEnded += new SessionEndedEventHandler(SystemEvents_SessionEnded);
            Program.sw.Stop();
        }
        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.SystemShutdown)
                File.WriteAllText(Environment.CurrentDirectory + "\\shutdown", "true");
            if (new WUApiLib.SystemInformation().RebootRequired)
                File.Create(Environment.CurrentDirectory + "\\updates");
            Application.Exit();
        }
        void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            if (e.Reason == SessionEndReasons.SystemShutdown)
                File.WriteAllText(Environment.CurrentDirectory + "\\shutdown", "true");
            if (new WUApiLib.SystemInformation().RebootRequired)
                File.Create(Environment.CurrentDirectory + "\\updates");
            Application.Exit();
        }
    }
}
