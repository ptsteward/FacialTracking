namespace AutoTurret
{
    partial class TargetingDisplay
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
            this.tabsSettingsAndStats = new System.Windows.Forms.TabControl();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.lblAcquireFPS_out = new System.Windows.Forms.Label();
            this.lblAcquireFPS = new System.Windows.Forms.Label();
            this.lblTrackFPS_out = new System.Windows.Forms.Label();
            this.lblCamFPS_out = new System.Windows.Forms.Label();
            this.lblFPStracking = new System.Windows.Forms.Label();
            this.lblFPScamera = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tiltTXT = new System.Windows.Forms.TextBox();
            this.panTXT = new System.Windows.Forms.TextBox();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.btnStartTargeting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cameraSelectionBox = new System.Windows.Forms.ComboBox();
            this.trackingMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turretToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgDisplay = new Emgu.CV.UI.ImageBox();
            this.tmrServoUpdate = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabsSettingsAndStats.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.trackingMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // tabsSettingsAndStats
            // 
            this.tabsSettingsAndStats.Controls.Add(this.tabStats);
            this.tabsSettingsAndStats.Controls.Add(this.tabSettings);
            this.tabsSettingsAndStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabsSettingsAndStats.Location = new System.Drawing.Point(644, 27);
            this.tabsSettingsAndStats.Name = "tabsSettingsAndStats";
            this.tabsSettingsAndStats.SelectedIndex = 0;
            this.tabsSettingsAndStats.Size = new System.Drawing.Size(372, 480);
            this.tabsSettingsAndStats.TabIndex = 6;
            // 
            // tabStats
            // 
            this.tabStats.BackColor = System.Drawing.Color.DarkGray;
            this.tabStats.Controls.Add(this.lblAcquireFPS_out);
            this.tabStats.Controls.Add(this.lblAcquireFPS);
            this.tabStats.Controls.Add(this.lblTrackFPS_out);
            this.tabStats.Controls.Add(this.lblCamFPS_out);
            this.tabStats.Controls.Add(this.lblFPStracking);
            this.tabStats.Controls.Add(this.lblFPScamera);
            this.tabStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabStats.Location = new System.Drawing.Point(4, 25);
            this.tabStats.Name = "tabStats";
            this.tabStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabStats.Size = new System.Drawing.Size(364, 451);
            this.tabStats.TabIndex = 0;
            this.tabStats.Text = "Statistics";
            // 
            // lblAcquireFPS_out
            // 
            this.lblAcquireFPS_out.AutoSize = true;
            this.lblAcquireFPS_out.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcquireFPS_out.Location = new System.Drawing.Point(127, 43);
            this.lblAcquireFPS_out.Name = "lblAcquireFPS_out";
            this.lblAcquireFPS_out.Size = new System.Drawing.Size(19, 20);
            this.lblAcquireFPS_out.TabIndex = 5;
            this.lblAcquireFPS_out.Text = "0";
            // 
            // lblAcquireFPS
            // 
            this.lblAcquireFPS.AutoSize = true;
            this.lblAcquireFPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcquireFPS.Location = new System.Drawing.Point(6, 43);
            this.lblAcquireFPS.Name = "lblAcquireFPS";
            this.lblAcquireFPS.Size = new System.Drawing.Size(114, 20);
            this.lblAcquireFPS.TabIndex = 4;
            this.lblAcquireFPS.Text = "Acquire FPS:";
            this.lblAcquireFPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTrackFPS_out
            // 
            this.lblTrackFPS_out.AutoSize = true;
            this.lblTrackFPS_out.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackFPS_out.Location = new System.Drawing.Point(127, 23);
            this.lblTrackFPS_out.Name = "lblTrackFPS_out";
            this.lblTrackFPS_out.Size = new System.Drawing.Size(19, 20);
            this.lblTrackFPS_out.TabIndex = 3;
            this.lblTrackFPS_out.Text = "0";
            // 
            // lblCamFPS_out
            // 
            this.lblCamFPS_out.AutoSize = true;
            this.lblCamFPS_out.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamFPS_out.Location = new System.Drawing.Point(127, 3);
            this.lblCamFPS_out.Name = "lblCamFPS_out";
            this.lblCamFPS_out.Size = new System.Drawing.Size(19, 20);
            this.lblCamFPS_out.TabIndex = 2;
            this.lblCamFPS_out.Text = "0";
            // 
            // lblFPStracking
            // 
            this.lblFPStracking.AutoSize = true;
            this.lblFPStracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFPStracking.Location = new System.Drawing.Point(6, 23);
            this.lblFPStracking.Name = "lblFPStracking";
            this.lblFPStracking.Size = new System.Drawing.Size(121, 20);
            this.lblFPStracking.TabIndex = 1;
            this.lblFPStracking.Text = "Tracking FPS:";
            this.lblFPStracking.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFPScamera
            // 
            this.lblFPScamera.AutoSize = true;
            this.lblFPScamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFPScamera.Location = new System.Drawing.Point(6, 3);
            this.lblFPScamera.Name = "lblFPScamera";
            this.lblFPScamera.Size = new System.Drawing.Size(115, 20);
            this.lblFPScamera.TabIndex = 0;
            this.lblFPScamera.Text = "Camera FPS:";
            this.lblFPScamera.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.Color.DarkGray;
            this.tabSettings.Controls.Add(this.textBox1);
            this.tabSettings.Controls.Add(this.tiltTXT);
            this.tabSettings.Controls.Add(this.panTXT);
            this.tabSettings.Controls.Add(this.imageBox3);
            this.tabSettings.Controls.Add(this.imageBox2);
            this.tabSettings.Controls.Add(this.imageBox1);
            this.tabSettings.Controls.Add(this.btnStartTargeting);
            this.tabSettings.Controls.Add(this.label1);
            this.tabSettings.Controls.Add(this.cameraSelectionBox);
            this.tabSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSettings.Location = new System.Drawing.Point(4, 25);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(364, 451);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DarkGray;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Wide Latin", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.DarkGray;
            this.textBox1.Location = new System.Drawing.Point(18, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(261, 46);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "BANG!!!!";
            // 
            // tiltTXT
            // 
            this.tiltTXT.Location = new System.Drawing.Point(10, 142);
            this.tiltTXT.Name = "tiltTXT";
            this.tiltTXT.Size = new System.Drawing.Size(101, 20);
            this.tiltTXT.TabIndex = 6;
            this.tiltTXT.Text = "1590";
            // 
            // panTXT
            // 
            this.panTXT.Location = new System.Drawing.Point(10, 116);
            this.panTXT.Name = "panTXT";
            this.panTXT.Size = new System.Drawing.Size(100, 20);
            this.panTXT.TabIndex = 5;
            this.panTXT.Text = "1552";
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(6, 252);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(295, 214);
            this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox3.TabIndex = 4;
            this.imageBox3.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(10, 271);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(295, 174);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox2.TabIndex = 3;
            this.imageBox2.TabStop = false;
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(258, 6);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(100, 100);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // btnStartTargeting
            // 
            this.btnStartTargeting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTargeting.Location = new System.Drawing.Point(59, 75);
            this.btnStartTargeting.Name = "btnStartTargeting";
            this.btnStartTargeting.Size = new System.Drawing.Size(143, 35);
            this.btnStartTargeting.TabIndex = 2;
            this.btnStartTargeting.Text = "Start Targeting";
            this.btnStartTargeting.UseVisualStyleBackColor = true;
            this.btnStartTargeting.Click += new System.EventHandler(this.btnStartTargeting_Click);
            this.btnStartTargeting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnStartTargeting_KeyPress);
            this.btnStartTargeting.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnStartTargeting_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera:";
            // 
            // cameraSelectionBox
            // 
            this.cameraSelectionBox.FormattingEnabled = true;
            this.cameraSelectionBox.Location = new System.Drawing.Point(6, 29);
            this.cameraSelectionBox.Name = "cameraSelectionBox";
            this.cameraSelectionBox.Size = new System.Drawing.Size(244, 21);
            this.cameraSelectionBox.TabIndex = 0;
            this.cameraSelectionBox.SelectedIndexChanged += new System.EventHandler(this.cameraSelectionBox_SelectedIndexChanged);
            // 
            // trackingMenu
            // 
            this.trackingMenu.BackColor = System.Drawing.Color.DimGray;
            this.trackingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.turretToolStripMenuItem});
            this.trackingMenu.Location = new System.Drawing.Point(0, 0);
            this.trackingMenu.Name = "trackingMenu";
            this.trackingMenu.Size = new System.Drawing.Size(919, 24);
            this.trackingMenu.TabIndex = 5;
            this.trackingMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Engravers MT", 9F, System.Drawing.FontStyle.Bold);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // turretToolStripMenuItem
            // 
            this.turretToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem1,
            this.setupToolStripMenuItem});
            this.turretToolStripMenuItem.Font = new System.Drawing.Font("Engravers MT", 9F, System.Drawing.FontStyle.Bold);
            this.turretToolStripMenuItem.Name = "turretToolStripMenuItem";
            this.turretToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.turretToolStripMenuItem.Text = "Turret";
            // 
            // connectToolStripMenuItem1
            // 
            this.connectToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.connectToolStripMenuItem1.Name = "connectToolStripMenuItem1";
            this.connectToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.connectToolStripMenuItem1.Text = "Connect";
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // imgDisplay
            // 
            this.imgDisplay.Location = new System.Drawing.Point(0, 27);
            this.imgDisplay.Name = "imgDisplay";
            this.imgDisplay.Size = new System.Drawing.Size(640, 480);
            this.imgDisplay.TabIndex = 2;
            this.imgDisplay.TabStop = false;
            // 
            // tmrServoUpdate
            // 
            this.tmrServoUpdate.Interval = 10;
            this.tmrServoUpdate.Tick += new System.EventHandler(this.tmrServoUpdate_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TargetingDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(919, 473);
            this.Controls.Add(this.imgDisplay);
            this.Controls.Add(this.tabsSettingsAndStats);
            this.Controls.Add(this.trackingMenu);
            this.Name = "TargetingDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tabsSettingsAndStats.ResumeLayout(false);
            this.tabStats.ResumeLayout(false);
            this.tabStats.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.trackingMenu.ResumeLayout(false);
            this.trackingMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabsSettingsAndStats;
        private System.Windows.Forms.TabPage tabStats;
        private System.Windows.Forms.Label lblTrackFPS_out;
        private System.Windows.Forms.Label lblCamFPS_out;
        private System.Windows.Forms.Label lblFPStracking;
        private System.Windows.Forms.Label lblFPScamera;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.MenuStrip trackingMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turretToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private Emgu.CV.UI.ImageBox imgDisplay;
        private System.Windows.Forms.Label lblAcquireFPS_out;
        private System.Windows.Forms.Label lblAcquireFPS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cameraSelectionBox;
        private System.Windows.Forms.Button btnStartTargeting;
        private Emgu.CV.UI.ImageBox imageBox2;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Timer tmrServoUpdate;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.TextBox tiltTXT;
        private System.Windows.Forms.TextBox panTXT;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

