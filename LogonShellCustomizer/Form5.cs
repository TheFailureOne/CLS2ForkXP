using System;
using System.IO;
using System.Windows.Forms;

namespace LogonShellCustomizer
{
    public partial class Form5 : Form
    {
        public static IniKey[] Default { get; } = new IniKey[] {
            new IniKey("UseKeySequence", "False", "KeySequence"),
            new IniKey("Type", "ListLast", "Login"),
            new IniKey("IsCancelEnabled", "True", "Login"),
            new IniKey("ButtonX", "None", "Login"),
            new IniKey("Hint", "TryFirst", "Login"),
            new IniKey("ShowOptions", "True", "Main"),
            new IniKey("NetLogon", "False", "Login"),
            new IniKey("HelpText", "True", "KeySequence"),
            new IniKey("NetLogonOptions", "True", "Login"),
            new IniKey("BannerRescale", "True", "Main"),
            new IniKey("CloseOnUpdate", "False", "Main")
        };
        public string file;
        IniFile Settings;
        public Form5(string file)
        {
            InitializeComponent();
            this.file = file;
            if (!File.Exists(file))
                File.Copy("LogonShell.ini", file);
            Settings = new IniFile(Default, file);
            loadSettings();
        }

        private void loadSettings()
        {
            switch(Settings.Read(0).ToLower())
            {
                case "false":
                    checkBox1.Checked = false;
                    comboBox1.SelectedIndex = 0;
                    comboBox1.Enabled = false;
                    checkBox4.Enabled = false;
                    break;
                case "win":
                    checkBox1.Checked = true;
                    comboBox1.SelectedIndex = 0;
                    break;
                case "ctrlaltwin":
                    checkBox1.Checked = true;
                    comboBox1.SelectedIndex = 1;
                    break;
                case "ctrlaltesc":
                    checkBox1.Checked = true;
                    comboBox1.SelectedIndex = 2;
                    break;
            }
            switch (Settings.Read(1).ToLower())
            {
                case "simple":
                    comboBox2.SelectedIndex = 0;
                    break;
                case "last":
                    comboBox2.SelectedIndex = 1;
                    break;
                case "list":
                    comboBox2.SelectedIndex = 2;
                    break;
                case "listlast":
                    comboBox2.SelectedIndex = 3;
                    break;
            }
            bool parse;
            if (Boolean.TryParse(Settings.Read(2), out parse))
                checkBox2.Checked = parse;
            switch (Settings.Read(3).ToLower())
            {
                case "none":
                    comboBox3.SelectedIndex = 0;
                    break;
                case "disabled":
                    comboBox3.SelectedIndex = 1;
                    break;
                case "enabled":
                    comboBox3.SelectedIndex = 2;
                    break;
            }
            switch (Settings.Read(4).ToLower())
            {
                case "never":
                    comboBox4.SelectedIndex = 0;
                    break;
                case "always":
                    comboBox4.SelectedIndex = 1;
                    break;
                case "tryfirst":
                    comboBox4.SelectedIndex = 2;
                    break;
            }
            if (Boolean.TryParse(Settings.Read(5), out parse))
                checkBox3.Checked = parse;
            switch (Settings.Read(6).ToLower())
            {
                case "disabled":
                    comboBox5.SelectedIndex = 0;
                    break;
                case "dialup":
                    comboBox5.SelectedIndex = 1;
                    break;
                case "domain":
                    comboBox5.SelectedIndex = 2;
                    break;
                case "domaindialup":
                    comboBox5.SelectedIndex = 3;
                    break;
            }
            if (Boolean.TryParse(Settings.Read(7), out parse))
                checkBox4.Checked = parse;
            if (Boolean.TryParse(Settings.Read(8), out parse))
                checkBox5.Checked = parse;
            if (Boolean.TryParse(Settings.Read(9), out parse))
                checkBox6.Checked = parse;
            if (Boolean.TryParse(Settings.Read(10), out parse))
                checkBox7.Checked = parse;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox1.Enabled = true;
                checkBox4.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                checkBox4.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                Settings.Write(0, "False");
            else
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        Settings.Write(0, "Win");
                        break;
                    case 1:
                        Settings.Write(0, "CtrlAltWin");
                        break;
                    case 2:
                        Settings.Write(0, "CtrlAltEsc");
                        break;
                }
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    Settings.Write(1, "Simple");
                    break;
                case 1:
                    Settings.Write(1, "Last");
                    break;
                case 2:
                    Settings.Write(1, "List");
                    break;
                case 3:
                    Settings.Write(1, "ListLast");
                    break;
            }
            if (checkBox2.Checked)
                Settings.Write(2, "True");
            else
                Settings.Write(2, "False");
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    Settings.Write(3, "None");
                    break;
                case 1:
                    Settings.Write(3, "Disabled");
                    break;
                case 2:
                    Settings.Write(3, "Enabled");
                    break;
            }
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    Settings.Write(4, "Never");
                    break;
                case 1:
                    Settings.Write(4, "Always");
                    break;
                case 2:
                    Settings.Write(4, "TryFirst");
                    break;
            }
            if (checkBox3.Checked)
                Settings.Write(5, "True");
            else
                Settings.Write(5, "False");
            switch (comboBox5.SelectedIndex)
            {
                case 0:
                    Settings.Write(6, "Disabled");
                    break;
                case 1:
                    Settings.Write(6, "DialUp");
                    break;
                case 2:
                    Settings.Write(6, "Domain");
                    break;
                case 3:
                    Settings.Write(6, "DomainDialUp");
                    break;
            }
            if (checkBox4.Checked)
                Settings.Write(7, "True");
            else
                Settings.Write(7, "False");
            if (checkBox5.Checked)
                Settings.Write(8, "True");
            else
                Settings.Write(8, "False");
            if (checkBox6.Checked)
                Settings.Write(9, "True");
            else
                Settings.Write(9, "False");
            if (checkBox7.Checked)
                Settings.Write(10, "True");
            else
                Settings.Write(10, "False");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Copy("LogonShell.ini", file, true);
            loadSettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(file);
            this.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == 0)
                checkBox5.Enabled = false;
            else
                checkBox5.Enabled = true;
        }
    }
}
