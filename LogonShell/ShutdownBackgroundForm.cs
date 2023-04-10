using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LogonShell
{
    public partial class ShutdownBackgroundForm : Form
    {
        ShutDownForm f1;

		public ShutdownBackgroundForm()
		{
			InitializeComponent();
			Rectangle r = new Rectangle();
			foreach (Screen s in Screen.AllScreens)
			{
				r = Rectangle.Union(r, s.Bounds);
			}
			Top = r.Top;
			Left = r.Left;
			Width = r.Width;
			Height = r.Height;
			Size = r.Size;
			int st;
			if (Cache.args.Length > 1 && Int32.TryParse(Cache.args[1], out st))
				f1 = new ShutDownForm(st);
			else
				f1 = new ShutDownForm();
			f1.StartPosition = FormStartPosition.CenterScreen;
            Shown += ShutdownBackgroundForm_Shown;
			Program.sw.Stop();
		}

        private void ShutdownBackgroundForm_Shown(object sender, EventArgs e)
		{
			backgroundWorker1.RunWorkerAsync();
		}

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
			while (Opacity < 0.6)
            {
				Thread.Sleep(20);
				Opacity = Opacity + 0.01;
			}
			if (f1.ShowDialog() != DialogResult.Retry)
				Application.Exit();
		}

		private void ShutdownBackgroundForm_Activated(object sender, System.EventArgs e)
		{
			f1.Activate();
		}

	}
}
