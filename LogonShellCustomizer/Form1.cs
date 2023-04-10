using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.ServiceProcess;
using System.Windows.Forms;

namespace LogonShellCustomizer
{
    public partial class MainWindow : Form
    {
        public static IniKey[] SettingKeys { get; } = new IniKey[] {
            new IniKey("Locale"),
            new IniKey("Theme"),
            new IniKey("LocaleFile"),
            new IniKey("ThemeFile"),
            new IniKey("OldInstall", "false"),
            new IniKey("Dummy", "x")
        };
        public static IniKey[] Default { get; } = new IniKey[] {
            new IniKey("Version", "R1_1P", "Program"),
            new IniKey("Banner", "theme\\banner.png", "Main"),
            new IniKey("Type", "Multi", "Background"),
            new IniKey("Color", "#1d5c8c", "Background"),
            new IniKey("Image", "theme\\bg.logo.png", "Background"),
            new IniKey("ShowOnMulti", "True", "Background"),
            new IniKey("X", "Auto", "Background"),
            new IniKey("Y", "Auto", "Background"),
            new IniKey("Width", "Auto", "Background"),
            new IniKey("ScaleFactor", "0", "Background"),
            new IniKey("ScaleType", "X", "Background"),
            new IniKey("Icon", "theme\\keyboard.icon.png", "KeySequence"),
            new IniKey("Image", "theme\\loading.gif", "Loading"),
            new IniKey("ShutdownIcon", "theme\\shutdown.icon.png", "Shutdown"),
            new IniKey("Error", "theme\\messagebox.error.ico", "MessageBox"),
            new IniKey("Warning", "theme\\messagebox.warning.ico", "MessageBox"),
            new IniKey("FgColor", "Default", "Main"),
            new IniKey("BgColor", "Default", "Main"),
            new IniKey("Theme", "Classic", "Main"),
            new IniKey("RestartIcon", "theme\\shutdown.icon.png", "Shutdown"),
            new IniKey("SleepIcon", "theme\\sleep.icon.png", "Shutdown"),
            new IniKey("FPS", "Auto", "Loading"),
            new IniKey("Height", "Auto", "Loading"),
            new IniKey("Banner", "theme\\banner.png", "Loading"),
            new IniKey("Banner", "theme\\banner.png", "Shutdown"),
            new IniKey("BannerOnShutdown", "theme\\banner.png", "Shutdown")
        };
        public static IniKey[] Locale { get; } = new IniKey[] {
            new IniKey("Localisation", "theme\\locale.ini", "Main")
        };
        IniFile Settings = new IniFile(SettingKeys);
        IniFile ThemeSettings = new IniFile(Default, "LogonShell.ini");
        IniFile LocaleSetting = new IniFile(Locale, "LogonShell.ini");
        public string[] locales, themes;
        public string customizerDir, tempFolder, modifiedSettings;
        public bool importedLocale, importedTheme, changedLocale;
        public MainWindow()
        {
            InitializeComponent();
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            customizerDir = Environment.CurrentDirectory + "\\customizer_files";
            tempFolder = Environment.CurrentDirectory + "\\Temp";
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            if (!File.Exists(Settings.Path))
            {
                Settings.Write(5);
                Settings.convertFile();
                Settings.Delete(5);
            }
        }

        private void loadFiles()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            locales = Directory.GetFiles(customizerDir + "\\locale\\", "*.ini", SearchOption.TopDirectoryOnly);
            themes = Directory.GetFiles(customizerDir + "\\themes\\", "*.zip", SearchOption.TopDirectoryOnly);
            foreach (string path in locales)
                comboBox1.Items.Add(Path.GetFileNameWithoutExtension(path));
            foreach (string path in themes)
                comboBox2.Items.Add(Path.GetFileNameWithoutExtension(path));
            if (Settings.KeyExists(0))
                comboBox1.SelectedIndex = Array.FindIndex(locales, path => Path.GetFileNameWithoutExtension(path) == Settings.Read(0));
            if (Settings.KeyExists(1))
                comboBox2.SelectedIndex = Array.FindIndex(themes, path => Path.GetFileNameWithoutExtension(path) == Settings.Read(1));
            if (importedLocale)
                comboBox1.SelectedIndex = Array.FindIndex(locales, path => Path.GetFileNameWithoutExtension(path) == "Imported Locale");
            if (importedTheme)
                comboBox2.SelectedIndex = Array.FindIndex(themes, path => Path.GetFileNameWithoutExtension(path) == "Imported Theme");
        }

        private void MainWindow_Show(object sender, EventArgs e)
        {
            if (!Settings.KeyExists(4) && Directory.Exists("C:\\Program Files\\Ellanet"))
            {
                Form2 frm = new Form2(customizerDir);
                DialogResult dr = frm.ShowDialog();
                if (!frm.shouldCheck)
                    Settings.Write(4);
                importedLocale = frm.importedLocale;
                importedTheme = frm.importedTheme;
            }
            else if (!Settings.KeyExists(4) && !Directory.Exists("C:\\Program Files\\Ellanet"))
                Settings.Write(4);
            loadFiles();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "\"" + Environment.CurrentDirectory + "\\Manual.pdf" + "\"");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = new OpenFileDialog();
            file.Filter = "Settings Files (.ini)|*.ini|All Files (*.*)|*.*";
            file.FilterIndex = 1;
            if (file.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(file.FileName) && Path.GetExtension(file.FileName)==".ini")
                {
                    File.Copy(file.FileName, customizerDir + "\\locale\\" + Path.GetFileName(file.FileName));
                    loadFiles();
                }
                else
                    MessageBox.Show("There was a  problem with the file", "Error", MessageBoxButtons.OK);
            }
            file.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var file = new OpenFileDialog();
            file.Filter = "Zip Files (.zip)|*.zip|All Files (*.*)|*.*";
            file.FilterIndex = 1;
            if (file.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(file.FileName) && Path.GetExtension(file.FileName) == ".zip")
                {
                    File.Copy(file.FileName, Environment.CurrentDirectory + "\\customizer_files\\themes\\" + Path.GetFileName(file.FileName));
                    loadFiles();
                }
                else
                    MessageBox.Show("There was a  problem with the file", "Error", MessageBoxButtons.OK);
            }
            file.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "\"" + Environment.CurrentDirectory + "\"");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Apply();
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
                InstallContext Context = new InstallContext();
                ServiceInstallerObj.Context = Context;
                ServiceInstallerObj.ServiceName = "Classic Logon Shell Launcher Service";
                ServiceInstallerObj.Uninstall(null);
                ServiceInstallerObj.Dispose();
            }
            catch (Exception)
            {

            }
            foreach (var process in Process.GetProcessesByName("LogonShell"))
            {
                process.Kill();
            }
            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 5 & rmdir /s /q \"" + Environment.CurrentDirectory + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                Form4 frm = new Form4(comboBox1.SelectedItem.ToString());
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadFiles();
                    changedLocale = true;
                }
                frm.Dispose();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                Form3 frm = new Form3(themes[comboBox2.SelectedIndex]);
                if (frm.ShowDialog() == DialogResult.OK)
                    modifiedSettings = comboBox2.SelectedItem.ToString();
                frm.Dispose();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);
            Form5 frm = new Form5(tempFolder + "\\LogonShell.ini");
            if (frm.ShowDialog() == DialogResult.OK)
                modifiedSettings = comboBox2.SelectedItem.ToString();
            frm.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void Apply()
        {
            this.Cursor = Cursors.WaitCursor;
            bool convert = false;
            if (!Settings.KeyExists(0))
                convert = true;
            if (File.Exists(tempFolder + "\\LogonShell.ini"))
            {
                File.Copy(tempFolder + "\\LogonShell.ini", "LogonShell.ini", true);
                File.Delete(tempFolder + "\\LogonShell.ini");
            }
            if ((comboBox1.SelectedIndex != -1 && Settings.Read(0) != comboBox1.SelectedItem.ToString()) || changedLocale)
            {
                if (LocaleSetting.KeyExists(0) && File.Exists(Settings.Read(2)))
                    File.Delete(Settings.Read(2));
                string newLocale = Environment.CurrentDirectory + "\\" + comboBox1.SelectedItem.ToString() + ".ini";
                File.Copy(locales[comboBox1.SelectedIndex], newLocale);
                LocaleSetting.Write(0, newLocale);
                Settings.Write(0, comboBox1.SelectedItem.ToString());
                Settings.Write(2, newLocale);
            }
            if (comboBox2.SelectedIndex != -1 && Settings.Read(1) != comboBox2.SelectedItem.ToString())
            {
                ZipArchive archive = ZipFile.Open(themes[comboBox2.SelectedIndex], ZipArchiveMode.Read);
                archive.ExtractToDirectory(tempFolder);
                archive.Dispose();
                if (File.Exists(customizerDir + "\\user_settings\\" + comboBox2.SelectedItem.ToString() + ".ini"))
                {
                    IniFile newConfig = new IniFile(Default, customizerDir + "\\user_settings\\" + comboBox2.SelectedItem.ToString() + ".ini");
                    foreach (IniKey k in Default)
                        if (newConfig.KeyExists(k))
                            ThemeSettings.Write(k, newConfig.Read(k));
                }
                else if (File.Exists(tempFolder + "\\LogonShell.ini"))
                {
                    IniFile newConfig = new IniFile(Default, tempFolder + "\\LogonShell.ini");
                    foreach (IniKey k in Default)
                        if (newConfig.KeyExists(k))
                            ThemeSettings.Write(k, newConfig.Read(k));
                }
                if (Directory.Exists(Environment.CurrentDirectory + "\\theme"))
                    Directory.Delete(Environment.CurrentDirectory + "\\theme", true);
                Directory.Move(tempFolder + "\\theme", Environment.CurrentDirectory + "\\theme");
                Settings.Write(1, comboBox2.SelectedItem.ToString());
                Settings.Write(3, themes[comboBox2.SelectedIndex]);
            }
            else if (modifiedSettings != null && modifiedSettings == comboBox2.SelectedItem.ToString())
            {
                IniFile newConfig = new IniFile(Default, customizerDir + "\\user_settings\\" + comboBox2.SelectedItem.ToString() + ".ini");
                foreach (IniKey k in Default)
                    if (newConfig.KeyExists(k))
                        ThemeSettings.Write(k, newConfig.Read(k));
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            if (convert)
                Settings.convertFile();
            if (File.Exists(Environment.CurrentDirectory + "\\cache.bin"))
                File.Delete(Environment.CurrentDirectory + "\\cache.bin");
            Process.Start(Environment.CurrentDirectory + "\\LogonShell.exe", "/quiet");
            this.Cursor = Cursors.Default;
        }
    }
}
