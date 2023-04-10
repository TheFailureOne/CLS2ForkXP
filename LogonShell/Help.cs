using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace LogonShell
{
    public partial class Help : Form
    {
        [DllImport("uxtheme.dll", SetLastError = true)]
        static extern int SetWindowTheme(IntPtr window, string app, string theme);
        public Help()
        {
            InitializeComponent();
            labelVersion.Text = Settings.FullVersion;
            labelCodename.Text = Settings.ProgramVersion;
            labelBuild.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();
            var version = Assembly.GetEntryAssembly().GetName().Version;
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(
            TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
            TimeSpan.TicksPerSecond * 2 * version.Revision));
            labelDate.Text = buildDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss tt", new CultureInfo("en-US"));
            labelSw.Text = Program.sw.Elapsed.TotalMilliseconds.ToString() + " ms";
            string certPK = "3082010A0282010100B0C0516ABAE078E51C9BACEC9034937423EFB46E20120A4A4ABB8A5CC534AE999F132E8CAE533BACB8B8FA54686390CCB73294D731E86F5A99BA00FE851D31E8D48662CA818FE4549BD6896043ECA890C6925F4CA8E1A70782D61AFE167A7916AE82E9393854C6FAFDFB86A212C9D1E1309F86344BA06B795AB0982AA06CF0A4018A1C6356DEC54353CD6AEEC25A20F93BB28E1326BACD140775E1AD97A796D8F8F8FB154FD9A89E26687136CD14A55BF0937D8DBF6B2D59B9E8B33CEA839BDB30A12CBD6BB822D0CE588001C620C7F921812E0CAAFBB9E818AB4E0824EDB068D8EED1D89F9CC40E5588C278C47A504FFCE71CEFA9F9F52529CBB512E32497A10203010001";
             
            try
            {
                var cert = X509Certificate.CreateFromSignedFile(Assembly.GetExecutingAssembly().Location);
                labelSigned.ForeColor = Color.Green;
                labelSigned.Text = "Present";
                if (cert.GetPublicKeyString() == certPK)
                {
                    labelKeyMatch.ForeColor = Color.Green;
                    labelKeyMatch.Text = "Match";
                }
                else
                {
                    labelKeyMatch.ForeColor = Color.Red;
                    labelKeyMatch.Text = "No Match";
                }
            }
            catch (Exception)
            {
                labelSigned.ForeColor = Color.Red;
                labelSigned.Text = "Not present";
                labelKeyMatch.ForeColor = Color.Red;
                labelKeyMatch.Text = "Not present";
            }

        }

        private void Help_Load(object sender, EventArgs e)
        {
            if (Settings.Read(28).ToLower() == "classic")
                SetWindowTheme(Handle, " ", " ");
        }
    }
}
