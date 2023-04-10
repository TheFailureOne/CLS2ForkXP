using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LogonShell
{
    public partial class MessageBoxForm : Form
	{

        #region DLL Imports
        [DllImport("uxtheme.dll", SetLastError = true)]
		static extern int SetWindowTheme(IntPtr window, string app, string theme);

        #endregion
		// icon 0 = error, 1 = warning
        public MessageBoxForm(string Title, string Message, int icon)
		{
			InitializeComponent();
			this.Text = Title;
			labelMessage.MaximumSize = new Size(400, 56);
			labelMessage.Text = Message;
			if (labelMessage.Height < 18)
				labelMessage.Location = new Point(55, 21);
			this.Size = new Size(83 + labelMessage.Width, (labelMessage.Height + 89 > 118)? labelMessage.Height + 89: 118 );
			buttonOK.Location = new Point((this.Width - 75) / 2, this.Height - 70);
			buttonOK.Text = Settings.Read(1, true);

			if (icon == 0)
                pictureBoxIcon.Image = Cache.Images[5];
            else
                pictureBoxIcon.Image = Cache.Images[6];
        }
        private void ShutDownForm_Load(object sender, EventArgs e)
		{
			if (Settings.Read(28).ToLower() == "classic")
				SetWindowTheme(Handle, " ", " ");
			SystemSounds.Hand.Play();
        }
        private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
