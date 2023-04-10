using System;
using System.Drawing;

namespace LogonShell
{
    public class Settings
    {
        public static string ProgramVersion = "R1_2", FullVersion = "Release 1.2";
        public static string Read(int id, bool IsLocale = false)
        {
            return (IsLocale) ? Cache.Config[id + Default.Length] : Cache.Config[id];
        }
        public static int[] Images { get; } = new int[] {
            3, 7, 15, 19, 21, 22, 23, 29, 30, 31, 32, 23, 3, 31, 32, 39, 39
        };
        public static Nullable<Size>[] ImageSizes { get; } = new Nullable<Size>[] {
            new Size(409,93), null, new Size(48,32), null, new Size(32,32), new Size(32, 32), new Size(32, 32), new Size(32, 32), new Size(32, 32), new Size(409,93), new Size(409,93), new Size(16, 16), null, null, null, new Size(409,93), null
        };
        public static IniKey[] Default { get; } = new IniKey[] {
            new IniKey("Version", ProgramVersion, "Program"),
            new IniKey("ResetSettings", "False", "Main"),
            new IniKey("ResetLocale", "False", "Main"),
            new IniKey("Banner", "theme\\banner.png", "Main"),
            new IniKey("Localisation", "theme\\locale.ini", "Main"),
            new IniKey("Type", "Multi", "Background"),
            new IniKey("Color", "#1d5c8c", "Background"),
            new IniKey("Image", "theme\\bg.logo.png", "Background"),
            new IniKey("ShowOnMulti", "True", "Background"),
            new IniKey("X", "Auto", "Background"),
            new IniKey("Y", "Auto", "Background"),
            new IniKey("Width", "Auto", "Background"),
            new IniKey("ScaleFactor", "0", "Background"),
            new IniKey("ScaleType", "X", "Background"),
            new IniKey("UseKeySequence", "False", "KeySequence"),
            new IniKey("Icon", "theme\\keyboard.icon.png", "KeySequence"),
            new IniKey("Type", "ListLast", "Login"),
            new IniKey("IsCancelEnabled", "True", "Login"),
            new IniKey("ButtonX", "Enabled", "Login"),
            new IniKey("Image", "theme\\loading.gif", "Loading"),
            new IniKey("FPS", "Auto", "Loading"),
            new IniKey("ShutdownIcon", "theme\\shutdown.icon.png", "Shutdown"),
            new IniKey("Error", "theme\\messagebox.error.ico", "MessageBox"),
            new IniKey("Warning", "theme\\messagebox.warning.ico", "MessageBox"),
            new IniKey("Hint", "TryFirst", "Login"),
            new IniKey("Height", "Auto", "Loading"),
            new IniKey("FgColor", "Default", "Main"),
            new IniKey("BgColor", "Default", "Main"),
            new IniKey("Theme", "Classic", "Main"),
            new IniKey("RestartIcon", "theme\\shutdown.icon.png", "Shutdown"),
            new IniKey("SleepIcon", "theme\\sleep.icon.png", "Shutdown"),
            new IniKey("Banner", "theme\\banner.png", "Loading"),
            new IniKey("Banner", "theme\\banner.png", "Shutdown"),
            new IniKey("ShowOptions", "True", "Main"),
            new IniKey("NetLogon", "DomainDialUp", "Login"),
            new IniKey("HelpText", "True", "KeySequence"),
            new IniKey("NetLogonOptions", "True", "Login"),
            new IniKey("BannerRescale", "True", "Main"),
            new IniKey("CloseOnUpdate", "False", "Main"),
            new IniKey("BannerOnShutdown", "theme\\banner.png", "Shutdown")
        };







        public static IniKey[] Locale { get; } = new IniKey[]
        {
            new IniKey("Version", ProgramVersion, "Program"),
            new IniKey("buttonOK", "OK", "Main"),
            new IniKey("buttonCancel", "Cancel", "Main"),
            new IniKey("Title", "Welcome to Windows", "KeySequence"),
            new IniKey("Text", "Press the Windows key to start.", "KeySequence"),
            new IniKey("Title", "Log On to Windows", "Login"),
            new IniKey("labelPassword", "Password:", "Login"),
            new IniKey("labelUsername", "User name:", "Login"),
            new IniKey("buttonOptions", "Options", "Login"),
            new IniKey("buttonShutdown", "Shutdown...", "Login"),
            new IniKey("Title", "Please Wait...", "Loading"),
            new IniKey("Text", "Applying your personal settings...", "Loading"),
            new IniKey("Title", "Shut down Windows", "Shutdown"),
            new IniKey("Text", "What do you want the computer to do?", "Shutdown"),
            new IniKey("ShutdownText", "Windows is shutting down...", "Loading"),
            new IniKey("Name1", "Shut down", "Shutdown"),
            new IniKey("Name2", "Restart", "Shutdown"),
            new IniKey("Name3", "Stand by", "Shutdown"),
            new IniKey("Desc1", "Ends your session and shuts down Windows so you can safely turn off power.", "Shutdown"),
            new IniKey("Desc2", "Ends your session, shuts down Windows, and starts Windows again.", "Shutdown"),
            new IniKey("Desc3", "Maintains your session, keeping the computer running on low power with data still in memory.", "Shutdown"),
            new IniKey("ErrorTitle", "Logon Message", "Login"),
            new IniKey("ErrorText", "The system could not log you on. Make sure your User name and domain are correct, then type your password again.  Letters in passwords must be typed using the correct case. Make sure that Caps Lock is not accidentally on.", "Login"),
            new IniKey("ErrorTitle", "Error", "Main"),
            new IniKey("labelDomain", "PIZZA PASTA:", "Login"),
            new IniKey("checkboxDialUp", "Log on using dial-up connection", "Login"),
            new IniKey("buttonHelp", "Help", "Main"),
            new IniKey("Help", "Windows helps keep your password secure. Click Help for more information", "KeySequence"),
            new IniKey("capsLockNotice", "Caps Lock is on. It is advisable to disable it.", "Login")
        };
    }
}