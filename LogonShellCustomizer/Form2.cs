using System;
using System.Configuration.Install;
using System.IO;
using System.IO.Compression;
using System.ServiceProcess;
using System.Windows.Forms;

namespace LogonShellCustomizer
{
    public partial class Form2 : Form
    {
        public string customizerDir;
        public bool importedLocale, importedTheme, shouldCheck;
        public Form2(string customizerDir)
        {
            InitializeComponent();
            this.customizerDir = customizerDir;
            shouldCheck = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 2 && e.CurrentValue == CheckState.Checked)
                checkedListBox1.Items.Add("Don\'t ask me anymore");
            if (e.Index == 2 && e.CurrentValue == CheckState.Unchecked)
                checkedListBox1.Items.Remove("Don\'t ask me anymore");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool[] checks = new bool[4];
            foreach (int chk in checkedListBox1.CheckedIndices)
                checks[chk] = true;

            string cdir = @"C:\Program Files\Ellanet\Logon Screen Launcher";

            if (checks[0] && File.Exists(cdir + @"\theme\locale.ini") && !File.Exists(customizerDir + "\\locale\\Imported Locale.ini"))
            {
                File.Copy(cdir + @"\theme\locale.ini", customizerDir + "\\locale\\Imported Locale.ini");
                importedLocale = true;
            }
            if (checks[1] && Directory.Exists(cdir + "\\theme") && File.Exists(cdir + "\\LogonShell.ini"))
            {
                string tempFolder = Environment.CurrentDirectory + "\\Temp";
                Directory.CreateDirectory(tempFolder);
                Directory.Move(cdir + "\\theme", tempFolder + "\\theme");
                File.Copy(cdir + "\\LogonShell.ini", tempFolder + "\\LogonShell.ini");
                ZipFile.CreateFromDirectory(tempFolder, customizerDir + @"\themes\Imported Theme.zip");
                Directory.Move(tempFolder + "\\theme", cdir + "\\theme");
                Directory.Delete(tempFolder, true);
                importedTheme = true;
            }
            if (checks[2])
            {
                try
                {
                    ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
                    InstallContext Context = new InstallContext();
                    ServiceInstallerObj.Context = Context;
                    ServiceInstallerObj.ServiceName = "Logon Screen Launcher";
                    ServiceInstallerObj.Uninstall(null);
                    ServiceInstallerObj.Dispose();
                }
                catch (Exception)
                {

                }
                Directory.Delete("C:\\Program Files\\Ellanet", true);
            }
            if (checks[2] || checks[3])
                shouldCheck = false;
            this.Close();
        }
    }
}
