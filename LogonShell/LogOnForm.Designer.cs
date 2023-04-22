
using FlaUI.Core.Tools;

namespace LogonShell
{
	partial class LogOnForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogOnForm));
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonShutdown = new System.Windows.Forms.Button();
            this.buttonDummy = new System.Windows.Forms.Button();
            this.labelLoading = new System.Windows.Forms.Label();
            this.comboBoxUsername = new System.Windows.Forms.ComboBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelKeySequence = new System.Windows.Forms.Label();
            this.labelShutdown = new System.Windows.Forms.Label();
            this.labelHint = new System.Windows.Forms.Label();
            this.backgroundWorkerCloseOnLogon = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBoxDomain = new System.Windows.Forms.ComboBox();
            this.checkBoxDialUp = new System.Windows.Forms.CheckBox();
            this.labelDomain = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.labelHelpLink = new System.Windows.Forms.Label();
            this.backgroundWorkerInteractiveUpdate = new System.ComponentModel.BackgroundWorker();
            this.labelCapsLock = new System.Windows.Forms.Label();
            this.backgroundWorkerCapsLockDetect = new System.ComponentModel.BackgroundWorker();
            this.NameLabel = new System.Windows.Forms.Label();
            this.AddAcc = new System.Windows.Forms.TextBox();
            this.TypeUrPass = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.ToBegin = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PFP = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.XPlogo = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.OKButt = new System.Windows.Forms.PictureBox();
            this.PFPAround = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SelectedUser = new System.Windows.Forms.PictureBox();
            this.VerticalSeparator = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxCapsLock = new System.Windows.Forms.PictureBox();
            this.pictureBoxKeySequence = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PFP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPlogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFPAround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapsLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKeySequence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.AcceptsReturn = true;
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxPassword.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(974, 351);
            this.textBoxPassword.Multiline = true;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '•';
            this.textBoxPassword.Size = new System.Drawing.Size(149, 20);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPassword_KeyPress);
            // 
            // buttonOptions
            // 
            this.buttonOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOptions.Location = new System.Drawing.Point(1283, 580);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 23);
            this.buttonOptions.TabIndex = 5;
            this.buttonOptions.Text = "Options > >";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Visible = false;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(1202, 580);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(272, 212);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(61, 13);
            this.labelUsername.TabIndex = 6;
            this.labelUsername.Text = "User name:";
            this.labelUsername.Visible = false;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(272, 239);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 7;
            this.labelPassword.Text = "Password:";
            this.labelPassword.Visible = false;
            // 
            // buttonShutdown
            // 
            this.buttonShutdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShutdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.buttonShutdown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShutdown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.buttonShutdown.FlatAppearance.BorderSize = 0;
            this.buttonShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShutdown.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShutdown.ForeColor = System.Drawing.Color.White;
            this.buttonShutdown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonShutdown.Location = new System.Drawing.Point(895, 653);
            this.buttonShutdown.Name = "buttonShutdown";
            this.buttonShutdown.Size = new System.Drawing.Size(228, 33);
            this.buttonShutdown.TabIndex = 9;
            this.buttonShutdown.Text = "Turn off computer";
            this.buttonShutdown.UseVisualStyleBackColor = false;
            this.buttonShutdown.Visible = false;
            this.buttonShutdown.Click += new System.EventHandler(this.buttonShutdown_Click);
            // 
            // buttonDummy
            // 
            this.buttonDummy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDummy.Enabled = false;
            this.buttonDummy.Location = new System.Drawing.Point(938, 709);
            this.buttonDummy.Name = "buttonDummy";
            this.buttonDummy.Size = new System.Drawing.Size(75, 23);
            this.buttonDummy.TabIndex = 10;
            this.buttonDummy.Text = "Dummy";
            this.buttonDummy.UseVisualStyleBackColor = true;
            this.buttonDummy.Visible = false;
            this.buttonDummy.Click += new System.EventHandler(this.buttonDummy_Click);
            // 
            // labelLoading
            // 
            this.labelLoading.AutoSize = true;
            this.labelLoading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.labelLoading.Location = new System.Drawing.Point(286, 189);
            this.labelLoading.Name = "labelLoading";
            this.labelLoading.Size = new System.Drawing.Size(190, 13);
            this.labelLoading.TabIndex = 11;
            this.labelLoading.Text = "Loading your personal settings...";
            this.labelLoading.Visible = false;
            // 
            // comboBoxUsername
            // 
            this.comboBoxUsername.FormattingEnabled = true;
            this.comboBoxUsername.Location = new System.Drawing.Point(344, 236);
            this.comboBoxUsername.Name = "comboBoxUsername";
            this.comboBoxUsername.Size = new System.Drawing.Size(243, 21);
            this.comboBoxUsername.TabIndex = 13;
            this.comboBoxUsername.Visible = false;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(453, 186);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(243, 20);
            this.textBoxUsername.TabIndex = 14;
            this.textBoxUsername.Visible = false;
            // 
            // labelKeySequence
            // 
            this.labelKeySequence.AutoSize = true;
            this.labelKeySequence.Location = new System.Drawing.Point(129, 251);
            this.labelKeySequence.Name = "labelKeySequence";
            this.labelKeySequence.Size = new System.Drawing.Size(156, 13);
            this.labelKeySequence.TabIndex = 15;
            this.labelKeySequence.Text = "Press the Windows key to start.";
            this.labelKeySequence.Visible = false;
            // 
            // labelShutdown
            // 
            this.labelShutdown.AutoSize = true;
            this.labelShutdown.Location = new System.Drawing.Point(53, 348);
            this.labelShutdown.MaximumSize = new System.Drawing.Size(385, 0);
            this.labelShutdown.Name = "labelShutdown";
            this.labelShutdown.Size = new System.Drawing.Size(84, 13);
            this.labelShutdown.TabIndex = 17;
            this.labelShutdown.Text = "Shutting down...";
            this.labelShutdown.Visible = false;
            // 
            // labelHint
            // 
            this.labelHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelHint.AutoSize = true;
            this.labelHint.BackColor = System.Drawing.SystemColors.Control;
            this.labelHint.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHint.Location = new System.Drawing.Point(81, 699);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new System.Drawing.Size(26, 13);
            this.labelHint.TabIndex = 18;
            this.labelHint.Text = "Hint";
            this.labelHint.Visible = false;
            // 
            // backgroundWorkerCloseOnLogon
            // 
            this.backgroundWorkerCloseOnLogon.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCloseOnLogon_DoWork);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBoxDomain
            // 
            this.comboBoxDomain.FormattingEnabled = true;
            this.comboBoxDomain.Location = new System.Drawing.Point(344, 262);
            this.comboBoxDomain.Name = "comboBoxDomain";
            this.comboBoxDomain.Size = new System.Drawing.Size(243, 21);
            this.comboBoxDomain.TabIndex = 19;
            this.comboBoxDomain.Visible = false;
            // 
            // checkBoxDialUp
            // 
            this.checkBoxDialUp.AutoSize = true;
            this.checkBoxDialUp.Location = new System.Drawing.Point(344, 262);
            this.checkBoxDialUp.Name = "checkBoxDialUp";
            this.checkBoxDialUp.Size = new System.Drawing.Size(80, 17);
            this.checkBoxDialUp.TabIndex = 20;
            this.checkBoxDialUp.Text = "checkBox1";
            this.checkBoxDialUp.UseVisualStyleBackColor = true;
            this.checkBoxDialUp.Visible = false;
            // 
            // labelDomain
            // 
            this.labelDomain.AutoSize = true;
            this.labelDomain.Location = new System.Drawing.Point(272, 266);
            this.labelDomain.Name = "labelDomain";
            this.labelDomain.Size = new System.Drawing.Size(79, 13);
            this.labelDomain.TabIndex = 21;
            this.labelDomain.Text = "PIZZA PASTA:";
            this.labelDomain.Visible = false;
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Location = new System.Drawing.Point(226, 304);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(361, 13);
            this.labelHelp.TabIndex = 22;
            this.labelHelp.Text = "Windows helps keep your password secure. Click Help for more information";
            this.labelHelp.Visible = false;
            // 
            // labelHelpLink
            // 
            this.labelHelpLink.AutoSize = true;
            this.labelHelpLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHelpLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpLink.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelHelpLink.Location = new System.Drawing.Point(628, 245);
            this.labelHelpLink.Name = "labelHelpLink";
            this.labelHelpLink.Size = new System.Drawing.Size(29, 13);
            this.labelHelpLink.TabIndex = 23;
            this.labelHelpLink.Text = "Help";
            this.labelHelpLink.Visible = false;
            this.labelHelpLink.Click += new System.EventHandler(this.labelHelpLink_Click);
            // 
            // backgroundWorkerInteractiveUpdate
            // 
            this.backgroundWorkerInteractiveUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerInteractiveUpdate_DoWork);
            // 
            // labelCapsLock
            // 
            this.labelCapsLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCapsLock.AutoSize = true;
            this.labelCapsLock.BackColor = System.Drawing.SystemColors.Control;
            this.labelCapsLock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCapsLock.Location = new System.Drawing.Point(105, 699);
            this.labelCapsLock.Name = "labelCapsLock";
            this.labelCapsLock.Size = new System.Drawing.Size(86, 13);
            this.labelCapsLock.TabIndex = 24;
            this.labelCapsLock.Text = "Caps Lock is on.";
            this.labelCapsLock.Visible = false;
            // 
            // backgroundWorkerCapsLockDetect
            // 
            this.backgroundWorkerCapsLockDetect.WorkerSupportsCancellation = true;
            this.backgroundWorkerCapsLockDetect.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCapsLockDetect_DoWork);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.BackColor = System.Drawing.Color.Transparent;
            this.NameLabel.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.ForeColor = System.Drawing.Color.White;
            this.NameLabel.Location = new System.Drawing.Point(962, 262);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(0, 32);
            this.NameLabel.TabIndex = 27;
            this.NameLabel.Text = this.textBoxUsername.Text;
            // 
            // AddAcc
            // 
            this.AddAcc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.AddAcc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AddAcc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.AddAcc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddAcc.ForeColor = System.Drawing.Color.White;
            this.AddAcc.Location = new System.Drawing.Point(1038, 709);
            this.AddAcc.Multiline = true;
            this.AddAcc.Name = "AddAcc";
            this.AddAcc.ReadOnly = true;
            this.AddAcc.Size = new System.Drawing.Size(306, 49);
            this.AddAcc.TabIndex = 30;
            this.AddAcc.TabStop = false;
            this.AddAcc.Text = "After you log on, you can add or change accounts.\r\nJust go to Control Panel and c" +
    "lick User Accounts.";
            this.AddAcc.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TypeUrPass
            // 
            this.TypeUrPass.AutoSize = true;
            this.TypeUrPass.BackColor = System.Drawing.Color.Transparent;
            this.TypeUrPass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeUrPass.ForeColor = System.Drawing.Color.White;
            this.TypeUrPass.Location = new System.Drawing.Point(795, 281);
            this.TypeUrPass.Name = "TypeUrPass";
            this.TypeUrPass.Size = new System.Drawing.Size(105, 13);
            this.TypeUrPass.TabIndex = 34;
            this.TypeUrPass.Text = "Type your password\r\n";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.BackColor = System.Drawing.Color.Transparent;
            this.UserLabel.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.ForeColor = System.Drawing.Color.White;
            this.UserLabel.Location = new System.Drawing.Point(795, 251);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(189, 24);
            this.UserLabel.TabIndex = 35;
            this.UserLabel.Text = "Type your password\r\n";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(1121, 580);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(32, 30);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Visible = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ToBegin
            // 
            this.ToBegin.AutoSize = true;
            this.ToBegin.BackColor = System.Drawing.Color.Transparent;
            this.ToBegin.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToBegin.ForeColor = System.Drawing.Color.White;
            this.ToBegin.Location = new System.Drawing.Point(412, 354);
            this.ToBegin.Name = "ToBegin";
            this.ToBegin.Size = new System.Drawing.Size(269, 23);
            this.ToBegin.TabIndex = 39;
            this.ToBegin.Text = "To begin, click your user name";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(95, 706);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox1.Size = new System.Drawing.Size(184, 23);
            this.textBox1.TabIndex = 40;
            this.textBox1.Text = "Turn off computer";
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.MouseEnter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_Leave);
            // 
            // PFP
            // 
            this.PFP.Image = global::LogonShell.Properties.Resources.PFP;
            this.PFP.Location = new System.Drawing.Point(741, 258);
            this.PFP.Name = "PFP";
            this.PFP.Size = new System.Drawing.Size(48, 48);
            this.PFP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PFP.TabIndex = 42;
            this.PFP.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox5.Image = global::LogonShell.Properties.Resources.Shutdown;
            this.pictureBox5.Location = new System.Drawing.Point(95, 706);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(26, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 41;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.textBox1_Click);
            this.pictureBox5.MouseEnter += new System.EventHandler(this.textBox1_Enter);
            this.pictureBox5.MouseLeave += new System.EventHandler(this.textBox1_Leave);
            // 
            // XPlogo
            // 
            this.XPlogo.Image = ((System.Drawing.Image)(resources.GetObject("XPlogo.Image")));
            this.XPlogo.Location = new System.Drawing.Point(612, 220);
            this.XPlogo.Name = "XPlogo";
            this.XPlogo.Size = new System.Drawing.Size(137, 86);
            this.XPlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.XPlogo.TabIndex = 38;
            this.XPlogo.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::LogonShell.Properties.Resources.Shade;
            this.pictureBox4.Location = new System.Drawing.Point(0, 83);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(219, 207);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 37;
            this.pictureBox4.TabStop = false;
            // 
            // OKButt
            // 
            this.OKButt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OKButt.Image = global::LogonShell.Properties.Resources.GoIcon;
            this.OKButt.Location = new System.Drawing.Point(919, 430);
            this.OKButt.Name = "OKButt";
            this.OKButt.Size = new System.Drawing.Size(26, 26);
            this.OKButt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.OKButt.TabIndex = 36;
            this.OKButt.TabStop = false;
            this.OKButt.Click += new System.EventHandler(this.pictureBox4_Click);
            this.OKButt.MouseCaptureChanged += new System.EventHandler(this.OKButt_MouseCaptureChanged);
            this.OKButt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OKButt_MouseDown);
            // 
            // PFPAround
            // 
            this.PFPAround.BackColor = System.Drawing.Color.Transparent;
            this.PFPAround.Image = ((System.Drawing.Image)(resources.GetObject("PFPAround.Image")));
            this.PFPAround.Location = new System.Drawing.Point(731, 251);
            this.PFPAround.Name = "PFPAround";
            this.PFPAround.Size = new System.Drawing.Size(58, 58);
            this.PFPAround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PFPAround.TabIndex = 33;
            this.PFPAround.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LogonShell.Properties.Resources.InputArea;
            this.pictureBox1.Location = new System.Drawing.Point(964, 348);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 29);
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // SelectedUser
            // 
            this.SelectedUser.Image = global::LogonShell.Properties.Resources.SelectedUserBackground;
            this.SelectedUser.Location = new System.Drawing.Point(720, 245);
            this.SelectedUser.Name = "SelectedUser";
            this.SelectedUser.Size = new System.Drawing.Size(308, 72);
            this.SelectedUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SelectedUser.TabIndex = 32;
            this.SelectedUser.TabStop = false;
            // 
            // VerticalSeparator
            // 
            this.VerticalSeparator.Image = global::LogonShell.Properties.Resources.DividerXPLogoAndLoginScreen;
            this.VerticalSeparator.Location = new System.Drawing.Point(702, 116);
            this.VerticalSeparator.Name = "VerticalSeparator";
            this.VerticalSeparator.Size = new System.Drawing.Size(1, 340);
            this.VerticalSeparator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VerticalSeparator.TabIndex = 31;
            this.VerticalSeparator.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::LogonShell.Properties.Resources.DividerBottom;
            this.pictureBox3.Location = new System.Drawing.Point(0, 689);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1366, 3);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 29;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.pictureBox2.Location = new System.Drawing.Point(0, 602);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1493, 81);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBoxCapsLock
            // 
            this.pictureBoxCapsLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxCapsLock.Location = new System.Drawing.Point(83, 696);
            this.pictureBoxCapsLock.Name = "pictureBoxCapsLock";
            this.pictureBoxCapsLock.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxCapsLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCapsLock.TabIndex = 25;
            this.pictureBoxCapsLock.TabStop = false;
            this.pictureBoxCapsLock.Visible = false;
            // 
            // pictureBoxKeySequence
            // 
            this.pictureBoxKeySequence.Location = new System.Drawing.Point(621, 236);
            this.pictureBoxKeySequence.Name = "pictureBoxKeySequence";
            this.pictureBoxKeySequence.Size = new System.Drawing.Size(48, 32);
            this.pictureBoxKeySequence.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxKeySequence.TabIndex = 16;
            this.pictureBoxKeySequence.TabStop = false;
            this.pictureBoxKeySequence.Visible = false;
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = global::LogonShell.Properties.Resources.DividerTop;
            this.pictureBoxLoading.Location = new System.Drawing.Point(0, 81);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(1366, 2);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLoading.TabIndex = 12;
            this.pictureBoxLoading.TabStop = false;
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(156)))));
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(1493, 81);
            this.pictureBoxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBanner.TabIndex = 0;
            this.pictureBoxBanner.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Location = new System.Drawing.Point(612, 323);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(308, 72);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox6.TabIndex = 43;
            this.pictureBox6.TabStop = false;
            // 
            // LogOnForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(126)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1370, 746);
            this.ControlBox = false;
            this.Controls.Add(this.PFP);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonShutdown);
            this.Controls.Add(this.ToBegin);
            this.Controls.Add(this.XPlogo);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.OKButt);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.TypeUrPass);
            this.Controls.Add(this.PFPAround);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SelectedUser);
            this.Controls.Add(this.VerticalSeparator);
            this.Controls.Add(this.AddAcc);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.pictureBoxCapsLock);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.labelCapsLock);
            this.Controls.Add(this.labelHelpLink);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelDomain);
            this.Controls.Add(this.checkBoxDialUp);
            this.Controls.Add(this.comboBoxDomain);
            this.Controls.Add(this.labelShutdown);
            this.Controls.Add(this.pictureBoxKeySequence);
            this.Controls.Add(this.labelKeySequence);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.comboBoxUsername);
            this.Controls.Add(this.pictureBoxLoading);
            this.Controls.Add(this.labelLoading);
            this.Controls.Add(this.buttonDummy);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.labelHint);
            this.Controls.Add(this.pictureBox6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogOnForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ur mom";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.LogOnForm_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PFP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPlogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OKButt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFPAround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapsLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKeySequence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxBanner;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Button buttonOptions;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelUsername;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Button buttonShutdown;
		private System.Windows.Forms.Button buttonDummy;
        private System.Windows.Forms.Label labelLoading;
        private System.Windows.Forms.ComboBox comboBoxUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.PictureBox pictureBoxKeySequence;
        private System.Windows.Forms.Label labelKeySequence;
        private System.Windows.Forms.Label labelShutdown;
        private System.Windows.Forms.Label labelHint;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCloseOnLogon;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBoxDomain;
        private System.Windows.Forms.CheckBox checkBoxDialUp;
        private System.Windows.Forms.Label labelDomain;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Label labelHelpLink;
        private System.ComponentModel.BackgroundWorker backgroundWorkerInteractiveUpdate;
        private System.Windows.Forms.Label labelCapsLock;
        private System.Windows.Forms.PictureBox pictureBoxCapsLock;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCapsLockDetect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox AddAcc;
        private System.Windows.Forms.PictureBox VerticalSeparator;
        private System.Windows.Forms.PictureBox SelectedUser;
        private System.Windows.Forms.PictureBox PFPAround;
        private System.Windows.Forms.Label TypeUrPass;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.PictureBox OKButt;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox XPlogo;
        private System.Windows.Forms.Label ToBegin;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox PFP;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}

