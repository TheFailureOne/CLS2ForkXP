using FlaUI.Core.Tools;
using FlaUI.UIA3;
using LogonShell.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LogonShell
{
    public partial class LogOnForm : Form
	{
		#region P/Invoke
		public enum WTS_CONNECTSTATE_CLASS
		{
			WTSActive,
			WTSConnected,
			WTSConnectQuery,
			WTSShadow,
			WTSDisconnected,
			WTSIdle,
			WTSListen,
			WTSReset,
			WTSDown,
			WTSInit
		}

		public enum WTS_INFO_CLASS
		{
			WTSInitialProgram,
			WTSApplicationName,
			WTSWorkingDirectory,
			WTSOEMId,
			WTSSessionId,
			WTSUserName,
			WTSWinStationName,
			WTSDomainName,
			WTSConnectState,
			WTSClientBuildNumber,
			WTSClientName,
			WTSClientDirectory,
			WTSClientProductId,
			WTSClientHardwareId,
			WTSClientAddress,
			WTSClientDisplay,
			WTSClientProtocolType,
			WTSIdleTime,
			WTSLogonTime,
			WTSIncomingBytes,
			WTSOutgoingBytes,
			WTSIncomingFrames,
			WTSOutgoingFrames,
			WTSClientInfo,
			WTSSessionInfo,
			WTSSessionInfoEx,
			WTSConfigInfo,
			WTSValidationInfo,
			WTSSessionAddressV4,
			WTSIsRemoteSession
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct WTS_SESSION_INFO
		{
			public Int32 SessionID;

			[MarshalAs(UnmanagedType.LPStr)]
			public String pWinStationName;

			public WTS_CONNECTSTATE_CLASS State;
		}

		[DllImport("wtsapi32.dll", SetLastError = true)]
		static extern IntPtr WTSOpenServer(string pServerName);
		[DllImport("wtsapi32.dll", SetLastError = true)]
		static extern int WTSEnumerateSessions(IntPtr hServer, int Reserved, int Version, ref IntPtr ppSessionInfo, ref int pCount);
		[DllImport("wtsapi32.dll", SetLastError = true)]
		static extern bool WTSQuerySessionInformationW(IntPtr hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out IntPtr ppBuffer, out int pBytesReturned);
		[DllImport("wtsapi32.dll", SetLastError = true)]
		static extern void WTSFreeMemory(IntPtr value);
		[DllImport("wtsapi32.dll", SetLastError = true)]
		static extern int WTSConnectSession(int LogonId, int TargetLogonId, string pPassword, bool bWait);
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern int WTSGetActiveConsoleSessionId();
		[DllImport("uxtheme.dll", SetLastError = true)]
		static extern int SetWindowTheme(IntPtr window, string app, string theme);
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool ExitWindowsEx(uint uFlags, uint dwReason); [DllImport("advapi32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool LogonUser([MarshalAs(UnmanagedType.LPStr)] string pszUserName, [MarshalAs(UnmanagedType.LPStr)] string pszDomain, [MarshalAs(UnmanagedType.LPStr)] string pszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool PaintDesktop(IntPtr hdc);

		readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
		#endregion

		bool optionsShown = false;
		private GifImage gifImage = null;
        Control UsernameBox;
        SystemMenuManager menuManager;
		List<string> UsernameList = new List<string>();
		List<string> UserSIDList = new List<string>();
        public LogOnForm()
		{
			InitializeComponent();
			Load += Form1_Load;
			LoadUI();
			if (Cache.args.Length > 0 && Cache.args[0] == "-startup" && File.Exists(Environment.CurrentDirectory + "\\updates"))
			{
				File.Delete(Environment.CurrentDirectory + "\\updates");
				Thread.Sleep(250);
				Environment.Exit(-1);
			}
			else if (Cache.args.Length > 0 && Cache.args[0] == "-startup" && AutologinEnabled())
			{
				ShowLoading();
				backgroundWorkerCloseOnLogon.RunWorkerAsync();
			}
			else if (Cache.args.Length > 0 && Cache.args[0] == "-shutdown")
				ShowLoading(true);
			else
			{
				ShowKeySeqDialog();
				if (Settings.Read(24).ToLower() == "always" && (Settings.Read(16).ToLower() == "last" || Settings.Read(16).ToLower() == "listlast"))
					GetUserPasswordHint();
			}
			Program.sw.Stop();
		}

		#region Form Events
		private void Form1_Load(object sender, EventArgs e)
		{
			if (Settings.Read(28).ToLower() == "classic")
				SetWindowTheme(Handle, " ", " ");
		}

		private void Form1_Shown(object sender, EventArgs e)
        {
            Location = new Point(Location.X, 166);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			var keySeqOption = Settings.Read(14).ToLower();
			switch (keySeqOption)
			{
				case "win":
					if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
						HideKeySeq();
					break;
				case "ctrlaltwin":
					if (e.Control && e.Alt && (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin))
						HideKeySeq();
					break;
				case "ctrlaltesc":
					if (e.Control && e.Alt && e.KeyCode == Keys.Escape)
						HideKeySeq();
					break;
				default:
					MessageBox.Show("Configuration Error: unknown key sequence " + keySeqOption);
					break;
			}
		}
		#endregion

		#region Control Events

		private void buttonOK_Click(object sender, EventArgs e)
		{
            IntPtr user = new IntPtr();
            bool succes = LogonUser(UsernameBox.Text, comboBoxDomain.Text, textBoxPassword.Text, 3, 0, ref user);
            if (!succes)
            {
                if (Marshal.GetLastWin32Error() == 1326)
                {
                    new MessageBoxForm(Settings.Read(21, true), Settings.Read(22, true), 1).ShowDialog();
                    if (Settings.Read(24).ToLower() == "tryfirst")
                        GetUserPasswordHint();
					textBoxPassword.Text = "";
					textBoxPassword.Focus();
					return;
				}
                else
				{
					new MessageBoxForm(Settings.Read(23, true), Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()).Message, 0).ShowDialog();
					return;
				}
            }

            try
            {
                Logon.Login(UsernameBox.Text, textBoxPassword.Text, String.Compare(GetLastUserLoggedOn(), UsernameBox.Text, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0);
            }
            catch (Exception ex)
            {
                new MessageBoxForm(Settings.Read(23, true), ex.Message, 0).ShowDialog();
				Application.Exit();
			}
			ShowLoading();
			backgroundWorkerCapsLockDetect.CancelAsync();
			backgroundWorkerCloseOnLogon.RunWorkerAsync();
        }

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void buttonOptions_Click(object sender, EventArgs e)
		{
			optionsShown = !optionsShown;
			buttonShutdown.Visible = optionsShown;
			if (optionsShown)
			{
				buttonOptions.Text = Settings.Read(8, true) + " < <";
				buttonCancel.Location = buttonOK.Location;
				buttonOK.Location = buttonDummy.Location;
				if (Boolean.Parse(Settings.Read(36)))
					LoadDialUpDomain();
			}
			else
			{
				buttonOptions.Text = Settings.Read(8, true) + " > >";
				buttonOK.Location = buttonCancel.Location;
				buttonCancel.Location = buttonShutdown.Location;
				if (Boolean.Parse(Settings.Read(36)))
					HideDialUpDomain();
			}
		}

		private void buttonShutdown_Click(object sender, EventArgs e)
		{
			var ShutdownForm = new ShutDownForm();
			var result = ShutdownForm.ShowDialog();
			if (result == DialogResult.OK)
				ShowLoading(true);
			ShutdownForm.Dispose();
        }

        private void ComboBoxUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUserPasswordHint();
		}
		private void labelHelpLink_Click(object sender, EventArgs e)
		{
			(new Help()).ShowDialog();
		}
		#endregion

		#region Get User Accounts
		private void PopulateUserList(ComboBox userComboBox)
		{
			userComboBox.Items.Clear();
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_UserAccount WHERE Disabled=False");
			ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
			foreach (ManagementObject envVar in searcher.Get())
			{
				UsernameList.Add((string)envVar["Name"]);
				UserSIDList.Add((string)envVar["SID"]);
            }
			userComboBox.Items.AddRange(UsernameList.ToArray());
		}
		private string GetLastUserLoggedOn()
		{
			string location = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI";
			var registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
			using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
			{
				using (var key = hive.OpenSubKey(location))
				{
					var item = key.GetValue("LastLoggedOnUser");
					string itemValue = item == null ? "No Logon Found" : item.ToString().Remove(0, 2); ;
					return itemValue;
				}
			}
		}
		private void GetUserPasswordHint()
		{
			try
			{
				string sid = UserSIDList.ElementAt(UsernameList.IndexOf(UsernameBox.Text));
				if (sid != null)
				{
					var rid = sid.Split('-').Last();
					var hexValue = int.Parse(rid).ToString("X").PadLeft(8, '0');
					RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SAM\SAM\Domains\Account\Users\" + hexValue);
					byte[] KeyValue = (byte[])key.GetValue("UserPasswordHint");
					string hint = System.Text.Encoding.UTF8.GetString(KeyValue, 0, KeyValue.Length).Replace("\0", "");
					if (!string.IsNullOrEmpty(hint) && hint != "null")
					{
						labelHint.Text = hint;
						labelHint.Visible = true;
					}
				}
			}
			catch (Exception)
			{
				// new MessageBoxForm(Settings.Read(23, true), e.Message, 0).ShowDialog(); do nothing
			}
		}
		#endregion

		#region Background Workers

		private void backgroundWorkerCloseOnLogon_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Process p = Process.GetProcessesByName("LogonUI").FirstOrDefault();
			do
			{
				p = Process.GetProcessesByName("LogonUI").FirstOrDefault();
				Thread.Sleep(100);
			} while (p != null);
			Application.Exit();
		}
		private void backgroundWorkerInteractiveUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Thread.Sleep(200);
			Process p = Process.GetProcessesByName("LogonUI").FirstOrDefault();
			if (p != null)
			{
				var app = FlaUI.Core.Application.Attach(p);
				using (var automation = new UIA3Automation())
				{
					var lines = app.GetMainWindow(automation).FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
					while (!p.HasExited && !backgroundWorkerInteractiveUpdate.CancellationPending)
					{
						if (lines.First().Name.Contains("%"))
							this.Invoke((MethodInvoker)delegate { labelShutdown.Text = lines.First().Name; });
						else if (lines.ElementAt(1).Name.Contains("%"))
							this.Invoke((MethodInvoker)delegate { labelShutdown.Text = lines.ElementAt(0).Name + " " + lines.ElementAt(1).Name; });
						else if (lines.ElementAtOrDefault(2) != null)
							this.Invoke((MethodInvoker)delegate { labelShutdown.Text = lines.ElementAt(0).Name + " " + lines.ElementAt(1).Name + " " + lines.ElementAt(2).Name; });
						else if (lines.ElementAtOrDefault(1) != null)
							this.Invoke((MethodInvoker)delegate { labelShutdown.Text = lines.ElementAt(0).Name + " " + lines.ElementAt(1).Name; });
						else
							this.Invoke((MethodInvoker)delegate { labelShutdown.Text = lines.ElementAt(0).Name; });
						if (labelShutdown.Height > 18)
							this.Invoke((MethodInvoker)delegate { this.Height = this.Height + 15; });
						Thread.Sleep(200);
						lines = app.GetMainWindow(automation).FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
					}
				}
			}
		}

		private void backgroundWorkerCapsLockDetect_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			bool isCapsLockOn = false;
			while (!backgroundWorkerCapsLockDetect.CancellationPending)
            {
				Thread.Sleep(100);
				if (Control.IsKeyLocked(Keys.CapsLock))
				{
					this.Invoke((MethodInvoker)delegate { labelCapsLock.Visible = true; pictureBoxCapsLock.Visible = true; });
					isCapsLockOn = true;
				} else if (isCapsLockOn) {
					this.Invoke((MethodInvoker)delegate { labelCapsLock.Visible = false; pictureBoxCapsLock.Visible = false; });
					isCapsLockOn = false;
				}
            }
		}

		#endregion

		#region Miscellaneous

		private void ShowKeySeqDialog()
		{
			var keySeqOption = Settings.Read(14).ToLower();
			if (keySeqOption != "false" && (keySeqOption == "win" || keySeqOption == "ctrlaltwin" || keySeqOption == "ctrlaltesc"))
			{
				HideLogin();
				labelKeySequence.Visible = true;
				pictureBoxKeySequence.Image = Cache.Images[2];
				pictureBoxKeySequence.Visible = true;
				if (Boolean.Parse(Settings.Read(35)))
				{
					this.ClientSize = new Size(409, 165);
					labelHelp.Visible = true;
                    labelHelpLink.Visible = true;
				}
				this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
			}
			else if (keySeqOption.ToLower() == "false")
			{
				if (!Boolean.Parse(Settings.Read(33)))
				{
					buttonShutdown.Location = buttonOptions.Location;
					buttonShutdown.Visible = true;
					buttonOptions.Visible = false;
				}
				LoadUsernameBox();
				if (!Boolean.Parse(Settings.Read(36)))
					LoadDialUpDomain();
				pictureBoxCapsLock.Image = Cache.Images[11];
				backgroundWorkerCapsLockDetect.RunWorkerAsync();
				Normalize();
			}
			else
				MessageBox.Show("Configuration Error: unknown key sequence " + keySeqOption);
		}

		private void HideKeySeq()
		{
			labelKeySequence.Visible = false;
			pictureBoxKeySequence.Visible = false;
			labelHelp.Visible = false;
			labelHelpLink.Visible = false;
			ShowLogin();
			this.KeyDown -= this.Form1_KeyDown;
		}

		private void HideLogin()
		{
			textBoxPassword.Visible = false;
			comboBoxUsername.Visible = false;
			textBoxUsername.Visible = false;
			comboBoxDomain.Visible = false;
			var labels = this.Controls.OfType<Label>();
			foreach (var lbl in labels)
				lbl.Visible = false;
			var buttons = this.Controls.OfType<Button>();
			foreach (var btn in buttons)
				btn.Visible = false;
			this.ClientSize = new Size(409, 132);
		}

		private void ShowLogin()
		{
			this.ClientSize = new Size(409, 205);
			textBoxPassword.Visible = true;
			labelPassword.Visible = true;
			labelUsername.Visible = true;
			buttonCancel.Visible = true;
			buttonOK.Visible = true;
			if (Boolean.Parse(Settings.Read(33)))
				buttonOptions.Visible = true;
			else
			{
				buttonShutdown.Location = buttonOptions.Location;
				buttonShutdown.Visible = true;
				buttonOptions.Visible = false;
			}
			MouseOperations.SetCursorPosition(Location.X + 350, Location.Y + 128);
			MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
			MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
			LoadUsernameBox();
			UsernameBox.Visible = true;
			if (!Boolean.Parse(Settings.Read(36)))
				LoadDialUpDomain();
			pictureBoxCapsLock.Image = Cache.Images[11];
            backgroundWorkerCapsLockDetect.RunWorkerAsync();
			Normalize();
		}

		private void ShowLoading(bool isShutdown = false)
		{
			HideLogin();
			int fps;
			if (Int32.TryParse(Settings.Read(20), out fps))
			{
				timer1.Interval = 1000 / fps;
				timer1.Start();
			}
			if (isShutdown)
			{
				if (File.Exists(Environment.CurrentDirectory + "\\updates"))
				{
					if (Boolean.Parse(Settings.Read(38)))
						Application.Exit();
                    else
						backgroundWorkerInteractiveUpdate.RunWorkerAsync();
				}
				if (!Boolean.Parse(Settings.Read(37)))
				{
					pictureBoxBanner.SizeMode = PictureBoxSizeMode.CenterImage;
				}
				labelShutdown.Visible = false;
                XPlogo.Location = new System.Drawing.Point(System.Windows.SystemParameters.WorkArea.Width.ToInt() / 2 + 39, SelectedUser.Location.Y + 7 - 31);
				ToBegin.Location = new System.Drawing.Point(System.Windows.SystemParameters.WorkArea.Width.ToInt() / 2 - 84, XPlogo.Location.Y + 24 + 86);
				ToBegin.Text = "Windows is shutting down...";

				AddAcc.Visible = false;
				VerticalSeparator.Visible = false;
				SelectedUser.Visible = false;
				PFPAround.Visible = false;
				pictureBox1.Visible = false;
				TypeUrPass.Visible = false;
				UserLabel.Visible = false;
				OKButt.Visible = false;
				textBox1.Visible = false;
				pictureBox5.Visible = false;
				PFP.Visible = false;
				textBoxPassword.Visible = false;
            }
			else
			{
				if (!Boolean.Parse(Settings.Read(37)))
				{
					pictureBoxBanner.SizeMode = PictureBoxSizeMode.CenterImage;
				}
                TypeUrPass.Parent = pictureBox6;
                UserLabel.Parent = pictureBox6;
                PFPAround.Parent = pictureBox6;

                labelLoading.Location = new System.Drawing.Point(TypeUrPass.Location.X + pictureBox6.Location.X - 2, TypeUrPass.Location.Y + pictureBox6.Location.Y);

                labelLoading.Visible = true;
                TypeUrPass.Visible = false;
                pictureBox1.Visible = false;
                textBoxPassword.Visible = false;
                SelectedUser.Visible = false;
				XPlogo.Visible = false;
				ToBegin.Visible = true;
				OKButt.Visible = false;

                ToBegin.Font = new System.Drawing.Font("Tahoma", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ToBegin.Text = "welcome";
                ToBegin.Location = new System.Drawing.Point(VerticalSeparator.Location.X - ToBegin.Width - 40, pictureBox6.Location.Y + PFPAround.Location.Y + 20 - 10);
            }
		}

		private void LoadUI()
		{
			gifImage = new GifImage(Cache.Images[3]);
			int height;
			if (Int32.TryParse(Settings.Read(25), out height))
				pictureBoxLoading.Height = height;
			if (!Boolean.Parse(Settings.Read(37)))
            {
                pictureBoxBanner.SizeMode = PictureBoxSizeMode.CenterImage;
            }
			if (Boolean.Parse(Settings.Read(17)))
				buttonCancel.Enabled = true;
			if (Settings.Read(18).ToLower() == "enabled")
				ControlBox = true;
			else if (Settings.Read(18).ToLower() == "disabled")
			{
				ControlBox = true;
                this.menuManager = new SystemMenuManager(this, SystemMenuManager.MenuItemState.Greyed);
            }
			var labels = this.Controls.OfType<Label>();
			var buttons = this.Controls.OfType<Button>();
			if (Settings.Read(27).ToLower() != "default")
			{
				this.BackColor = ColorTranslator.FromHtml(Settings.Read(27));
				foreach (var lbl in labels)
					lbl.BackColor = ColorTranslator.FromHtml(Settings.Read(27));
				foreach (var btn in buttons)
					btn.BackColor = SystemColors.Control;
			}
			if (Settings.Read(26).ToLower() != "default")
			{
				foreach (var lbl in labels)
					lbl.ForeColor = ColorTranslator.FromHtml(Settings.Read(26));
			}
			buttonOK.Text = "";
			buttonCancel.Text = Settings.Read(2, true);
			buttonOptions.Text = Settings.Read(8, true) + " > >";
			buttonShutdown.Text = Settings.Read(9, true);
			labelPassword.Text = Settings.Read(6, true);
			labelUsername.Text = Settings.Read(7, true);
			labelShutdown.Text = Settings.Read(14, true);
			labelDomain.Text = Settings.Read(24, true);
			checkBoxDialUp.Text = Settings.Read(25, true);
			labelHelpLink.Text = Settings.Read(26, true);
			labelHelp.Text = Settings.Read(27, true);
			labelCapsLock.Text = Settings.Read(28, true);
		}

		private void Normalize()
        {
			if (labelUsername.Width > 70 || labelPassword.Width > 70 || labelDomain.Width > 70)
            {
				int newWidth = (labelUsername.Width > labelPassword.Width)? labelUsername.Width : labelPassword.Width;
				newWidth = (newWidth > labelDomain.Width) ? newWidth : labelDomain.Width;
				newWidth = (newWidth > 142) ? 142 : newWidth;
				UsernameBox.Left = newWidth + 16;
				textBoxPassword.Left = newWidth + 16;
				comboBoxDomain.Left = newWidth + 16;
				checkBoxDialUp.Left = newWidth + 16;
				labelHint.Left = newWidth + 16;
				pictureBoxCapsLock.Left = newWidth + 15;
				labelCapsLock.Left = newWidth + 31;
			}
        }

		private void LoadDialUpDomain()
		{
			if (Settings.Read(34).ToLower() == "domaindialup")
			{
				this.ClientSize = new System.Drawing.Size(409, 250);
				checkBoxDialUp.Location = new Point(checkBoxDialUp.Left, 185);
				comboBoxDomain.Items.Clear();
                try
                {
                    comboBoxDomain.Items.Add(System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().Name);
                }
                catch { }
                comboBoxDomain.Items.Add(Environment.MachineName);
                comboBoxDomain.SelectedIndex = 0;
				labelDomain.Visible = true;
				comboBoxDomain.Visible = true;
				checkBoxDialUp.Visible = true;
			}
			else if (Settings.Read(34).ToLower() == "dialup")
			{
				this.ClientSize = new System.Drawing.Size(409, 225);
				checkBoxDialUp.Visible = true;
			}
			else if (Settings.Read(34).ToLower() == "domain")
			{
				this.ClientSize = new Size(409, 235);
				comboBoxDomain.Items.Clear();
				try
				{
                    comboBoxDomain.Items.Add(System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().Name);
                } catch { }
                comboBoxDomain.Items.Add(Environment.MachineName);
                comboBoxDomain.SelectedIndex = 0;
				labelDomain.Visible = true;
				comboBoxDomain.Visible = true;
			}
		}

		private void HideDialUpDomain()
		{
			labelDomain.Visible = false;
			comboBoxDomain.Visible = false;
			checkBoxDialUp.Visible = false;
			this.ClientSize = new Size(409, 205);
		}

		private void LoadUsernameBox()
		{
			var usernameOption = Settings.Read(16).ToLower();
			switch (usernameOption)
			{
				case "simple":
					textBoxUsername.Visible = true;
					comboBoxUsername.Visible = false;
					this.ActiveControl = textBoxUsername;
					textBoxUsername.Focus();
					UsernameBox = textBoxUsername;
					break;
				case "last":
					textBoxUsername.Visible = true;
					comboBoxUsername.Visible = false;
					textBoxUsername.Text = GetLastUserLoggedOn();
					UsernameBox = textBoxUsername;
					this.ActiveControl = textBoxPassword;
					textBoxPassword.Focus();
					break;
				case "list":
					textBoxUsername.Visible = false;
					//comboBoxUsername.Visible = true;
					PopulateUserList(comboBoxUsername);
					this.ActiveControl = comboBoxUsername;
                    comboBoxUsername.Focus();
					if (Settings.Read(24).ToLower() == "always")
                        comboBoxUsername.SelectedIndexChanged += ComboBoxUsername_SelectedIndexChanged;
					UsernameBox = comboBoxUsername;
					break;
				case "listlast":
					textBoxUsername.Visible = false;
					//comboBoxUsername.Visible = true;
					PopulateUserList(comboBoxUsername);
					comboBoxUsername.Text = GetLastUserLoggedOn();
					if (Settings.Read(24).ToLower() == "always")
						comboBoxUsername.SelectedIndexChanged += ComboBoxUsername_SelectedIndexChanged;
					UsernameBox = comboBoxUsername;
					this.ActiveControl = textBoxPassword;
					textBoxPassword.Focus();
					break;
				default:
					MessageBox.Show("Configuration Error: unknown login type " + usernameOption);
					break;
			}
		}

        private bool AutologinEnabled()
        {
            IntPtr phToken = new IntPtr();

            bool loggedIn = LogonUser(GetLastUserLoggedOn(), Environment.MachineName, "", 3, 0, ref phToken);

			// int error = Marshal.GetLastWin32Error();
			// 1327 = empty password

			bool autologon = false;
			bool displayLastUsername = true;

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
            if (key.GetValue("AutoAdminLogon") != null)
                autologon = (key.GetValue("AutoAdminLogon").ToString()).Contains("1");
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            if (key.GetValue("DontDisplayLastUserName") != null)
                displayLastUsername = (key.GetValue("DontDisplayLastUserName").ToString()).Contains("0");

            if ((loggedIn || autologon) && displayLastUsername)
                return true;
            else
                return false;
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
		}

        #endregion

        private void LogOnForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
			pictureBoxLoading.Width = System.Windows.SystemParameters.WorkArea.Width.ToInt() + 6;
            pictureBox2.Location = new System.Drawing.Point(0, System.Windows.SystemParameters.WorkArea.Height.ToInt() - 81 + 5);
            pictureBox3.Location = new System.Drawing.Point(0, pictureBox2.Location.Y - 3);
            pictureBoxBanner.Width = System.Windows.SystemParameters.WorkArea.Width.ToInt() + 6;
            pictureBox2.Width = System.Windows.SystemParameters.WorkArea.Width.ToInt() + 6;
            pictureBox3.Width = System.Windows.SystemParameters.WorkArea.Width.ToInt() + 6;
			AddAcc.Location = new System.Drawing.Point(System.Windows.SystemParameters.WorkArea.Width.ToInt() + 6 - 334, pictureBox2.Location.Y + 16);
			VerticalSeparator.Height = (pictureBox3.Location.Y - 1) - 90;
            VerticalSeparator.Location = new System.Drawing.Point((System.Windows.SystemParameters.WorkArea.Width.ToInt()/2)-2, 90);
			SelectedUser.Location = new System.Drawing.Point(VerticalSeparator.Location.X+25, System.Windows.SystemParameters.WorkArea.Height.ToInt() / 2 - 26);
			PFPAround.Location = new System.Drawing.Point(10, 7);
			pictureBox1.Location = new System.Drawing.Point(SelectedUser.Location.X + 10 + 58 + 6, SelectedUser.Location.Y + 51);
            TypeUrPass.Location = new System.Drawing.Point(75, 33);
			UserLabel.Location = new System.Drawing.Point(75, 8);
			OKButt.Location = new System.Drawing.Point(pictureBox1.Location.X + 172 + 10, pictureBox1.Location.Y + 1);
			XPlogo.Location = new System.Drawing.Point(VerticalSeparator.Location.X - 137 - 20, SelectedUser.Location.Y + 7 - 31);
			ToBegin.Location = new System.Drawing.Point(XPlogo.Location.X - 145, XPlogo.Location.Y + 24 + 86);
			textBox1.Location = new System.Drawing.Point(26, pictureBox2.Location.Y + 21);
            pictureBox5.Location = new System.Drawing.Point(26, pictureBox2.Location.Y + 21);
			PFP.Location = new System.Drawing.Point(SelectedUser.Location.X + 10 + 5, SelectedUser.Location.Y + 7 + 5);
			textBoxPassword.Location = new System.Drawing.Point(pictureBox1.Location.X + 10, pictureBox1.Location.Y + 3);
            
            pictureBox6.Location = new System.Drawing.Point(SelectedUser.Location.X, SelectedUser.Location.Y);

            TypeUrPass.Parent = SelectedUser;
			UserLabel.Parent = SelectedUser;
			PFPAround.Parent = SelectedUser;
			UserLabel.Text = comboBoxUsername.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            IntPtr user = new IntPtr();
            bool succes = LogonUser(UsernameBox.Text, comboBoxDomain.Text, textBoxPassword.Text, 3, 0, ref user);
            if (!succes)
            {
                if (Marshal.GetLastWin32Error() == 1326)
                {
                    new MessageBoxForm(Settings.Read(21, true), Settings.Read(22, true), 1).ShowDialog();
                    if (Settings.Read(24).ToLower() == "tryfirst")
                        GetUserPasswordHint();
                    textBoxPassword.Text = "";
                    textBoxPassword.Focus();
                    return;
                }
                else
                {
                    new MessageBoxForm(Settings.Read(23, true), Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()).Message, 0).ShowDialog();
                    return;
                }
            }

            try
            {
                Logon.Login(UsernameBox.Text, textBoxPassword.Text, String.Compare(GetLastUserLoggedOn(), UsernameBox.Text, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0);
            }
            catch (Exception ex)
            {
                new MessageBoxForm(Settings.Read(23, true), ex.Message, 0).ShowDialog();
                Application.Exit();
            }
            ShowLoading();
            backgroundWorkerCapsLockDetect.CancelAsync();
            backgroundWorkerCloseOnLogon.RunWorkerAsync();
        }

        private void OKButt_MouseCaptureChanged(object sender, EventArgs e)
        {
			
        }

        private void OKButt_MouseDown(object sender, MouseEventArgs e)
        {
			
        }

        private void buttonDummy_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            var ShutdownForm = new ShutDownForm();
            var result = ShutdownForm.ShowDialog();
            if (result == DialogResult.OK)
                ShowLoading(true);
            ShutdownForm.Dispose();
        }

        private void textBoxPassword_KeyDown(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
			
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            int ascii = (int)e.KeyChar;
            if (ascii == 13)
            {
                e.Handled = true;
                IntPtr user = new IntPtr();
                bool succes = LogonUser(UsernameBox.Text, comboBoxDomain.Text, textBoxPassword.Text, 3, 0, ref user);
                if (!succes)
                {
                    if (Marshal.GetLastWin32Error() == 1326)
                    {
                        new MessageBoxForm(Settings.Read(21, true), Settings.Read(22, true), 1).ShowDialog();
                        if (Settings.Read(24).ToLower() == "tryfirst")
                            GetUserPasswordHint();
                        textBoxPassword.Text = "";
                        textBoxPassword.Focus();
                        return;
                    }
                    else
                    {
                        new MessageBoxForm(Settings.Read(23, true), Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()).Message, 0).ShowDialog();
                        return;
                    }
                }

                try
                {
                    Logon.Login(UsernameBox.Text, textBoxPassword.Text, String.Compare(GetLastUserLoggedOn(), UsernameBox.Text, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0);
                }
                catch (Exception ex)
                {
                    new MessageBoxForm(Settings.Read(23, true), ex.Message, 0).ShowDialog();
                    Application.Exit();
                }
                ShowLoading();
                backgroundWorkerCapsLockDetect.CancelAsync();
                backgroundWorkerCloseOnLogon.RunWorkerAsync();
            }
        }
    }
}
