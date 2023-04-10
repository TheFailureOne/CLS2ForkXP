using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace LogonShellCustomizer
{
    public partial class Form3 : Form
    {
        private static String HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        string themeFile, theme, customizerDir, settingsDir, settingsFile, tempFolder;
        bool newFile;
        IniFile Settings;
        public Form3(string themeFile)
        {
            InitializeComponent();
            this.themeFile = themeFile;
            this.theme = Path.GetFileNameWithoutExtension(themeFile);
            tempFolder = Environment.CurrentDirectory + "\\Temp";
            customizerDir = Environment.CurrentDirectory + "\\customizer_files";
            settingsDir = customizerDir + "\\user_settings";
            settingsFile = settingsDir + "\\" + theme + ".ini";
            label4.Text = theme;
            loadSettings();
        }

        private void loadSettings()
        {
            string settingsDir = customizerDir + "\\user_settings";
            string settingsFile = settingsDir + "\\" + theme + ".ini";
            if (!Directory.Exists(settingsDir))
                Directory.CreateDirectory(settingsDir);
            if (!File.Exists(settingsFile))
            {
                ZipArchive archive = ZipFile.Open(themeFile, ZipArchiveMode.Read);
                archive.ExtractToDirectory(tempFolder);
                archive.Dispose();
                File.Copy(tempFolder + "\\LogonShell.ini", settingsFile);
                newFile = true;
                Directory.Delete(tempFolder, true);
            }
            Settings = new IniFile(MainWindow.Default, settingsFile);
            if (Settings.Read(16).ToLower() == "default")
            {
                checkBox7.Checked = true;
                pictureBox1.BackColor = SystemColors.ControlText;
            }
            else
            {
                checkBox7.Checked = false;
                pictureBox1.BackColor = ColorTranslator.FromHtml(Settings.Read(16));
            }
            if (Settings.Read(17).ToLower() == "default")
            {
                checkBox8.Checked = true;
                pictureBox2.BackColor = SystemColors.Control;
            }
            else
            {
                checkBox8.Checked = false;
                pictureBox2.BackColor = ColorTranslator.FromHtml(Settings.Read(17));
            }
            if (Settings.Read(2).ToLower() == "multi")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
            pictureBox3.BackColor = ColorTranslator.FromHtml(Settings.Read(3));
            if (Boolean.Parse(Settings.Read(5)))
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;
            if (Settings.Read(7).ToLower() == "auto")
                checkBox3.Checked = true;
            else
            {
                checkBox3.Checked = false;
                numericUpDown1.Value = Int32.Parse(Settings.Read(7));
            }
            if (Settings.Read(6).ToLower() == "auto")
                checkBox4.Checked = true;
            else
            {
                checkBox4.Checked = false;
                numericUpDown2.Value = Int32.Parse(Settings.Read(6));
            }
            if (Settings.Read(8).ToLower() == "auto")
                checkBox6.Checked = true;
            else
            {
                numericUpDown3.Value = Int32.Parse(Settings.Read(8));
                checkBox6.Checked = false;
            }
            if (Settings.Read(10).ToLower() == "x")
                comboBox1.SelectedIndex = 0;
            else if (Settings.Read(10).ToLower() == "y")
                comboBox1.SelectedIndex = 1;
            else
                comboBox1.SelectedIndex = 2;
            if (Int32.Parse(Settings.Read(9)) == 0)
                checkBox5.Checked = true;
            else
            {
                numericUpDown4.Value = Int32.Parse(Settings.Read(9));
                checkBox5.Checked = false;
            }
            int i, x;
            if (Int32.TryParse(Settings.Read(21), out i))
            {
                numericUpDown5.Value = i;
                checkBox9.Checked = false;
            }
            else
                checkBox9.Checked = true;
            if (Int32.TryParse(Settings.Read(22), out x))
            {
                numericUpDown6.Value = x;
                checkBox10.Checked = false;
            }
            else
                checkBox10.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
                Settings.Write(16);
            else
                Settings.Write(16, HexConverter(pictureBox1.BackColor));
            if (checkBox8.Checked)
                Settings.Write(17);
            else
                Settings.Write(17, HexConverter(pictureBox2.BackColor));
            if (checkBox1.Checked)
                Settings.Write(2);
            else
                Settings.Write(2, "Single");
            Settings.Write(3, HexConverter(pictureBox3.BackColor));
            if (checkBox2.Checked)
                Settings.Write(5);
            else
                Settings.Write(5, "False");
            if (checkBox3.Checked)
                Settings.Write(7, "Auto");
            else
                Settings.Write(7, numericUpDown1.Value.ToString());
            if (checkBox4.Checked)
                Settings.Write(6, "Auto");
            else
                Settings.Write(6, numericUpDown2.Value.ToString());
            if (checkBox5.Checked)
            {
                Settings.Write(9, "0");
                if (checkBox6.Checked)
                    Settings.Write(8, "Auto");
                else
                    Settings.Write(8, numericUpDown3.Value.ToString());
            }
            else
            {
                Settings.Write(9, numericUpDown4.Value.ToString());
                if (comboBox1.SelectedIndex == 0)
                    Settings.Write(10, "X");
                else if (comboBox1.SelectedIndex == 1)
                    Settings.Write(10, "Y");
                else
                    Settings.Write(10, "XY");

            }
            if (checkBox9.Checked)
                Settings.Write(21, "Auto");
            else
                Settings.Write(21, numericUpDown5.Value.ToString());
            if (checkBox10.Checked)
                Settings.Write(22, "Auto");
            else
                Settings.Write(22, numericUpDown6.Value.ToString());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ColorPicker(pictureBox3);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                numericUpDown1.Enabled = false;
            else
                numericUpDown1.Enabled = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                numericUpDown2.Enabled = false;
            else
                numericUpDown2.Enabled = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                numericUpDown3.Enabled = false;
            else
                numericUpDown3.Enabled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                if (!checkBox6.Checked)
                    numericUpDown3.Enabled = true;
                checkBox6.Enabled = true;
                numericUpDown4.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                numericUpDown3.Enabled = false;
                checkBox6.Enabled = false;
                numericUpDown4.Enabled = true;
                comboBox1.Enabled = true;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                pictureBox1.Enabled = false;
                pictureBox1.Cursor = Cursors.Default;
            }
            else
            {
                pictureBox1.Enabled = true;
                pictureBox1.Cursor = Cursors.Hand;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                pictureBox2.Enabled = false;
                pictureBox2.Cursor = Cursors.Default;
            }
            else
            {
                pictureBox2.Enabled = true;
                pictureBox2.Cursor = Cursors.Hand;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
                numericUpDown5.Enabled = false;
            else
                numericUpDown5.Enabled = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
                numericUpDown6.Enabled = false;
            else
                numericUpDown6.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorPicker(pictureBox1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorPicker(pictureBox2);
        }

        private void ColorPicker(PictureBox pictureBox)
        {
            colorDialog1.Reset();
            colorDialog1.CustomColors = new int[] {
                                        ColorTranslator.ToOle(pictureBox.BackColor)
                                      };
            colorDialog1.Color = pictureBox.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox.BackColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (newFile)
                File.Delete(settingsFile);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Delete(settingsFile);
            loadSettings();
        }
    }
}
