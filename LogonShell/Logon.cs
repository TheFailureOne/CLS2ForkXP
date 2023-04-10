using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace LogonShell
{
    internal class Logon
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        [DllImport("user32.dll")]
        static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public static void Login(string user, string password, bool IsLastUser)
        {
            Process p = Process.GetProcessesByName("LogonUI").FirstOrDefault();
            if (p != null)
            {
                BackgroundForm.form.StopWorker();
                string pwd = new string(password.Where(c => !char.IsControl(c)).ToArray()); // Need to sanitize it
                var app = FlaUI.Core.Application.Attach(p);
                using (var automation = new UIA3Automation())
                {
                    Window window;
                    AutomationElement[] items;
                    IntPtr h = app.MainWindowHandle;
                    SetForegroundWindow(h);
                    SendKeys.SendWait("{BACKSPACE}");
                    Thread.Sleep(100);
                    window = app.GetMainWindow(automation);
                    SetForegroundWindow(h);
                    bool dontShowLastUser = window.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit)).Count() > 1;
                    if (!IsLastUser)
                    {
                        if (dontShowLastUser)
                            throw new Exception("Mode not supported!");
                        items = window.FindAllDescendants();
                        foreach (var item in items)
                        {
                            string name;
                            try
                            {
                                name = item.Name;
                                if (name.Contains(user))
                                {
                                    BackgroundForm.form.MakeTransparent();
                                    item.Click();
                                    BackgroundForm.form.RemoveTransparent();
                                }
                                Thread.Sleep(200);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    AutomationElement[] textboxes = window.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
                    if (textboxes.Count() > 0)
                    {
                        if (dontShowLastUser)
                        {
                            if (textboxes.First().Name.ToLower() != "password")
                                textboxes.First().Focus();
                            else
                                textboxes.Last().Focus();
                            SendKeys.SendWait(user);
                        }
                        if (!string.IsNullOrEmpty(pwd))
                        {
                            if (dontShowLastUser && textboxes.First().Name.ToLower() != "password")
                                textboxes.Last().Focus();
                            else
                                textboxes.First().Focus();
                            SendKeys.SendWait(pwd);
                        }
                    }
                    SendKeys.SendWait("{ENTER}");
                }
                BackgroundForm.form.StartWorker();
            }
            else
                throw new Exception("LogonUI not found!");
        }
    }
}
