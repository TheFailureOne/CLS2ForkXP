using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LogonShellCustomizer
{
    public partial class Form4 : Form
    {
        string locale;
        public Form4(string locale)
        {
            InitializeComponent();
            this.locale = locale;
            textBox1.Text = File.ReadAllText(Environment.CurrentDirectory + "\\customizer_files\\locale\\" + locale + ".ini");
            textBox1.SelectionStart = 0;
            textBox2.Text = locale;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text)&&!String.IsNullOrEmpty(textBox2.Text))
            {
                string fileName = Environment.CurrentDirectory + "\\customizer_files\\locale\\" + textBox2.Text + ".ini";
                using (StreamWriter sw = new StreamWriter(File.Open(fileName, FileMode.Create), Encoding.Unicode))
                {
                    foreach (string line in textBox1.Lines)
                        sw.WriteLine(line);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\customizer_files\\locale\\" + textBox2.Text + ".ini"))
                label2.Visible = true;
            else
                label2.Visible = false;
        }
    }
}
