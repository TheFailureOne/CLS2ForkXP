using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogonShell
{
	public partial class SecondaryBackgrounds : Form
	{
		LogOnForm f1;

		public SecondaryBackgrounds(LogOnForm Form1, Screen ShownScreen)
		{
			InitializeComponent();
			Location = ShownScreen.WorkingArea.Location;
            Rectangle screen = ShownScreen.Bounds;
			Size = screen.Size;
			BackColor = ColorTranslator.FromHtml(Settings.Read(6));
			if (Settings.Read(7).ToLower() != "none" && Boolean.Parse(Settings.Read(8)))
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
					if (Int32.TryParse(Settings.Read(9), out x) && Int32.TryParse(Settings.Read(10), out y))
						pictureBox1.Location = new System.Drawing.Point(x, y);
					else if (Int32.TryParse(Settings.Read(9), out x))
						pictureBox1.Location = new System.Drawing.Point(x, (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
					else if (Int32.TryParse(Settings.Read(10), out y))
						pictureBox1.Location = new System.Drawing.Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), y);
					else
						pictureBox1.Location = new System.Drawing.Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
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
						pictureBox1.Location = new System.Drawing.Point(x, y);
					else if (Int32.TryParse(Settings.Read(9), out x))
						pictureBox1.Location = new System.Drawing.Point(x, (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
					else if (Int32.TryParse(Settings.Read(10), out y))
						pictureBox1.Location = new System.Drawing.Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), y);
					else
						pictureBox1.Location = new System.Drawing.Point((int)Math.Round(((float)screen.Width / 2) - ((float)pictureBox1.Width / 2)), (int)Math.Round(((float)screen.Height / 2) - ((float)pictureBox1.Height / 2)));
				}
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
				pictureBox1.Image = image;
			}
			f1 = Form1;
			Shown += BackgroundForm_Shown;
        }

		private void BackgroundForm_Shown(object sender, EventArgs e)
		{
			f1.Show();
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
	}
}
