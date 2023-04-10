using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace LogonShell
{
    public partial class BackgroundForm : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        LogOnForm f1;
		SecondaryBackgrounds[] secondaryBackgrounds;
        int Long;
		public static BackgroundForm form { get; set; }

		public BackgroundForm()
		{
			InitializeComponent();
			Rectangle screen = Screen.PrimaryScreen.Bounds;
			Size = screen.Size;
			BackColor = ColorTranslator.FromHtml(Settings.Read(6));
			if (Settings.Read(7).ToLower() != "none")
			{
				var image = Cache.Images[1];
				if (Int32.Parse(Settings.Read(12))>0)
				{
					if (Settings.Read(13).ToLower() == "x")
                    {
                        pictureBox1.Width = (int)Math.Round((float)screen.Width / 100 * Int32.Parse(Settings.Read(12)));
                        pictureBox1.Height = (int)Math.Round((float)image.Height / image.Width * pictureBox1.Width);
                    }
					else if (Settings.Read(13).ToLower() == "y")
                    {
                        pictureBox1.Height = (int)Math.Round((float)screen.Height / 100 * Int32.Parse(Settings.Read(12)));
                        pictureBox1.Width = (int)Math.Round((float)image.Width / image.Height * pictureBox1.Height);
                    }
					else
                    {
                        pictureBox1.Width = (int)Math.Round((float)screen.Width / 100 * Int32.Parse(Settings.Read(12)));
                        pictureBox1.Height = (int)Math.Round((float)screen.Height / 100 * Int32.Parse(Settings.Read(12)));
                    }
                    Int32 x, y;
                    if (Int32.TryParse(Settings.Read(9), out x)&&Int32.TryParse(Settings.Read(10), out y))
                        pictureBox1.Location = new Point(x, y);
                    else if (Int32.TryParse(Settings.Read(9), out x))
                        pictureBox1.Location = new Point(x, (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
                    else if (Int32.TryParse(Settings.Read(10), out y))
                        pictureBox1.Location = new Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), y);
                    else
                        pictureBox1.Location = new Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
                }
                else
				{
					if (Settings.Read(11).ToLower()=="auto")
                    {
                        pictureBox1.Width = image.Width;
                        pictureBox1.Height = image.Height;
                    }
					else
                    {
                        pictureBox1.Width = Int32.Parse(Settings.Read(11));
                        pictureBox1.Height = (int)Math.Round((float)image.Height / image.Width * pictureBox1.Width);
                    }
                    Int32 x, y;
                    if (Int32.TryParse(Settings.Read(9), out x) && Int32.TryParse(Settings.Read(10), out y))
                        pictureBox1.Location = new Point(x, y);
                    else if (Int32.TryParse(Settings.Read(9), out x))
                        pictureBox1.Location = new Point(x, (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
                    else if (Int32.TryParse(Settings.Read(10), out y))
                        pictureBox1.Location = new Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), y);
                    else
                        pictureBox1.Location = new Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
				pictureBox1.Image = image;
			}
            f1 = new LogOnForm();
			if (Settings.Read(5).ToLower() == "multi")
            {
                Screen[] screens = Screen.AllScreens;
				secondaryBackgrounds = new SecondaryBackgrounds[screens.Length];
				int i = 0;
				foreach (Screen temp in screens)
                {
                    if (!temp.Primary)
                    {
                        secondaryBackgrounds[i] = new SecondaryBackgrounds(f1, temp);
                        secondaryBackgrounds[i].Show();
                    }
                    i++;
                }
            }
            Shown += BackgroundForm_Shown;
			form = this;
            if (WindowsIdentity.GetCurrent().Name == @"NT AUTHORITY\SYSTEM")
                backgroundWorker1.DoWork += backgroundWorker1_DoWork;
        }

		private void BackgroundForm_Shown(object sender, EventArgs e)
		{
			f1.Show();

            backgroundWorker1.RunWorkerAsync();
        }

		private void BackgroundForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
				e.Cancel = true;
		}

		private void BackgroundForm_Activated(object sender, EventArgs e)
		{
			f1.Activate();
		}
        public void MakeTransparent()
        {
            Long = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, Long | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        public void RemoveTransparent()
        {
            SetWindowLong(this.Handle, GWL_EXSTYLE, Long);
        }
        public void StopWorker()
        {
            backgroundWorker1.CancelAsync();
        }
        public void StartWorker()
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                Thread.Sleep(50);
                IntPtr hwnd = GetForegroundWindow();
                if (hwnd == null)
                    continue;
                uint pid;
                GetWindowThreadProcessId(hwnd, out pid);
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.ProcessName == "LockApp" || p.ProcessName == "LockScreen" || p.ProcessName == "LockAppHost" || p.ProcessName == "LockScreenContentServer") 
                        p.Kill();
                    else if (p.Id == pid && p.ProcessName != Process.GetCurrentProcess().ProcessName)
                    {
                        MouseOperations.SetCursorPosition(f1.Location.X + 350, f1.Location.Y + 128);
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                    }
                }
            }
        }

        private void BackgroundForm_Load(object sender, EventArgs e)
        {
        }
    }
}
