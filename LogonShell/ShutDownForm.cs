using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LogonShell
{
	public partial class ShutDownForm : Form
	{
		[DllImport("uxtheme.dll", SetLastError = true)]
		static extern int SetWindowTheme(IntPtr window, string app, string theme);

		string[] actionDescriptions =
		{
			@"Ends your session and shuts down Windows so you can safely turn off power.",
			@"Ends your session, shuts down Windows, and starts Windows again.",
			@"Maintains your session, keeping the computer running on low power with data still in memory."
		};

		int[] actionIcons = new int[3];
		int selectedIndex;

        public ShutDownForm(int shutdownType = 0)
		{
			InitializeComponent();
			this.Text = Settings.Read(12, true);
            if (!Boolean.Parse(Settings.Read(37)))
            {
                pictureBoxBanner.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            int height;
			if (Int32.TryParse(Settings.Read(25).ToLower(), out height))
				pictureBoxLoading.Height = height;
			else
				pictureBoxLoading.Height = Cache.Images[3].Height;
			pictureBoxLoading.Image = new GifImage(Cache.Images[3]).GetNextFrame();
			pictureBoxComputer.Image = Cache.Images[4];
			comboBox1.Items.Clear();
			comboBox1.Items.Add(Settings.Read(15, true));
			comboBox1.Items.Add(Settings.Read(16, true));
			comboBox1.Items.Add(Settings.Read(17, true));
			actionDescriptions[0] = Settings.Read(18, true);
			actionDescriptions[1] = Settings.Read(19, true);
			actionDescriptions[2] = Settings.Read(20, true);
			labelWdyw.Text = Settings.Read(13, true);
			buttonOK.Text = Settings.Read(1, true);
			buttonCancel.Text = Settings.Read(2, true);
			buttonHelp.Text = Settings.Read(26, true);
			actionIcons[0] = 4;
			actionIcons[1] = 7;
			actionIcons[2] = 8;
			selectedIndex = shutdownType;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex != -1)
			{
				labelDescription.Text = actionDescriptions[comboBox1.SelectedIndex];
				pictureBoxComputer.Image = Cache.Images[actionIcons[comboBox1.SelectedIndex]];
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ShutDownForm_Load(object sender, EventArgs e)
		{
			comboBox1.SelectedIndex = selectedIndex;
			if (Settings.Read(28).ToLower() == "classic")
				SetWindowTheme(Handle, " ", " ");
		}
		private void buttonOK_Click(object sender, EventArgs e)
		{
			ProcessStartInfo psi = null;
			var LoadAnim = true;
			switch (comboBox1.SelectedIndex)
			{
				case 0:
					psi = new ProcessStartInfo("shutdown", "/s /t 0");
					break;
				case 1:
					psi = new ProcessStartInfo("shutdown", "/r /t 0");
					break;
				case 2:
					Application.SetSuspendState(PowerState.Suspend, true, true);
					LoadAnim = false;
					break;
				default:
					MessageBox.Show("The selected option isn't available.");
					LoadAnim = false;
					break;
			}
			if (psi != null)
			{
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
				Process.Start(psi);
			}
			if (LoadAnim)
				this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}

        private void buttonHelp_Click(object sender, EventArgs e)
        {
			(new Help()).ShowDialog();
        }
    }
}
