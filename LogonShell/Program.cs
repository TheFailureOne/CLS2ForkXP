using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LogonShell
{
    static class Program
    {
        public static Stopwatch sw;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            sw = new Stopwatch();
            sw.Start();
            Cache.Initialize(args);
            if (Settings.Read(28).ToLower() != "classic")
                Application.EnableVisualStyles();
            if (args.Length == 0)
                Application.Run(new BackgroundForm());
            else
                switch(args[0])
                {
                    case "/quiet":
                        Cache.Refresh();
                        sw.Stop();
                        Application.Exit();
                        break;
                    case "/shutdown":
                        Application.Run(new ShutdownBackgroundForm());
                        break;
                    case "-tray":
                        Application.Run(new UserTray());
                        break;
                    default:
                        Application.Run(new BackgroundForm());
                        break;
                }
        }
    }
}
