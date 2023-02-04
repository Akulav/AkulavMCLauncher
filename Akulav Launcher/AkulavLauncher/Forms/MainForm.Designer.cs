
namespace AkulavLauncher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Username = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.statusText = new System.Windows.Forms.Label();
            this.leftpanel = new System.Windows.Forms.Panel();
            this.leftTopPanel = new System.Windows.Forms.Panel();
            this.devLabel = new System.Windows.Forms.Label();
            this.leftlabel = new System.Windows.Forms.Label();
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.rightpanel = new System.Windows.Forms.Panel();
            this.versionBox = new System.Windows.Forms.ComboBox();
            this.ramLabel = new System.Windows.Forms.Label();
            this.ramSlider = new System.Windows.Forms.TrackBar();
            this.downloadBar = new System.Windows.Forms.ProgressBar();
            this.gameVersion = new System.Windows.Forms.Label();
            this.packVersion = new System.Windows.Forms.Label();
            this.repairButton = new FontAwesome.Sharp.IconButton();
            this.topPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.launchButton = new FontAwesome.Sharp.IconButton();
            this.leftpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            this.rightpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramSlider)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(109, 300);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(290, 20);
            this.Username.TabIndex = 6;
            this.Username.TextChanged += new System.EventHandler(this.Username_TextChanged);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.userLabel.Location = new System.Drawing.Point(20, 300);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(83, 20);
            this.userLabel.TabIndex = 8;
            this.userLabel.Text = "Username";
            // 
            // statusText
            // 
            this.statusText.AutoEllipsis = true;
            this.statusText.AutoSize = true;
            this.statusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.statusText.Location = new System.Drawing.Point(21, 357);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(0, 13);
            this.statusText.TabIndex = 11;
            // 
            // leftpanel
            // 
            this.leftpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.leftpanel.Controls.Add(this.leftTopPanel);
            this.leftpanel.Controls.Add(this.devLabel);
            this.leftpanel.Controls.Add(this.leftlabel);
            this.leftpanel.Controls.Add(this.leftPictureBox);
            this.leftpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftpanel.Location = new System.Drawing.Point(0, 0);
            this.leftpanel.Name = "leftpanel";
            this.leftpanel.Size = new System.Drawing.Size(262, 398);
            this.leftpanel.TabIndex = 16;
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.BackColor = System.Drawing.Color.Transparent;
            this.leftTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftTopPanel.Location = new System.Drawing.Point(0, 0);
            this.leftTopPanel.Name = "leftTopPanel";
            this.leftTopPanel.Size = new System.Drawing.Size(262, 42);
            this.leftTopPanel.TabIndex = 20;
            this.leftTopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftTopPanel_MouseDown);
            // 
            // devLabel
            // 
            this.devLabel.AutoSize = true;
            this.devLabel.Font = new System.Drawing.Font("Yu Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.devLabel.ForeColor = System.Drawing.Color.White;
            this.devLabel.Location = new System.Drawing.Point(147, 375);
            this.devLabel.Name = "devLabel";
            this.devLabel.Size = new System.Drawing.Size(109, 14);
            this.devLabel.TabIndex = 17;
            this.devLabel.Text = "Developed by Akulav";
            // 
            // leftlabel
            // 
            this.leftlabel.AutoSize = true;
            this.leftlabel.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftlabel.ForeColor = System.Drawing.Color.White;
            this.leftlabel.Location = new System.Drawing.Point(12, 184);
            this.leftlabel.Name = "leftlabel";
            this.leftlabel.Size = new System.Drawing.Size(241, 27);
            this.leftlabel.TabIndex = 16;
            this.leftlabel.Text = "Akulav Launcher V1.0.1";
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("leftPictureBox.Image")));
            this.leftPictureBox.Location = new System.Drawing.Point(72, 48);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(119, 133);
            this.leftPictureBox.TabIndex = 0;
            this.leftPictureBox.TabStop = false;
            // 
            // rightpanel
            // 
            this.rightpanel.BackColor = System.Drawing.SystemColors.Control;
            this.rightpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightpanel.Controls.Add(this.versionBox);
            this.rightpanel.Controls.Add(this.ramLabel);
            this.rightpanel.Controls.Add(this.ramSlider);
            this.rightpanel.Controls.Add(this.downloadBar);
            this.rightpanel.Controls.Add(this.gameVersion);
            this.rightpanel.Controls.Add(this.packVersion);
            this.rightpanel.Controls.Add(this.repairButton);
            this.rightpanel.Controls.Add(this.topPanel);
            this.rightpanel.Controls.Add(this.nameLabel);
            this.rightpanel.Controls.Add(this.statusText);
            this.rightpanel.Controls.Add(this.launchButton);
            this.rightpanel.Controls.Add(this.Username);
            this.rightpanel.Controls.Add(this.userLabel);
            this.rightpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightpanel.Location = new System.Drawing.Point(262, 0);
            this.rightpanel.Name = "rightpanel";
            this.rightpanel.Size = new System.Drawing.Size(427, 398);
            this.rightpanel.TabIndex = 17;
            // 
            // versionBox
            // 
            this.versionBox.FormattingEnabled = true;
            this.versionBox.Location = new System.Drawing.Point(278, 263);
            this.versionBox.Name = "versionBox";
            this.versionBox.Size = new System.Drawing.Size(121, 21);
            this.versionBox.TabIndex = 27;
            // 
            // ramLabel
            // 
            this.ramLabel.AutoSize = true;
            this.ramLabel.Font = new System.Drawing.Font("Yu Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.ramLabel.Location = new System.Drawing.Point(21, 263);
            this.ramLabel.Name = "ramLabel";
            this.ramLabel.Size = new System.Drawing.Size(47, 18);
            this.ramLabel.TabIndex = 26;
            this.ramLabel.Text = "RAM: ";
            // 
            // ramSlider
            // 
            this.ramSlider.Location = new System.Drawing.Point(109, 249);
            this.ramSlider.Name = "ramSlider";
            this.ramSlider.Size = new System.Drawing.Size(165, 45);
            this.ramSlider.TabIndex = 25;
            this.ramSlider.ValueChanged += new System.EventHandler(this.RamSlider_ValueChanged);
            // 
            // downloadBar
            // 
            this.downloadBar.Location = new System.Drawing.Point(24, 370);
            this.downloadBar.Name = "downloadBar";
            this.downloadBar.Size = new System.Drawing.Size(375, 23);
            this.downloadBar.TabIndex = 24;
            // 
            // gameVersion
            // 
            this.gameVersion.AutoSize = true;
            this.gameVersion.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.gameVersion.Location = new System.Drawing.Point(19, 101);
            this.gameVersion.Name = "gameVersion";
            this.gameVersion.Size = new System.Drawing.Size(194, 27);
            this.gameVersion.TabIndex = 22;
            this.gameVersion.Text = "NewEra - Ultimate";
            // 
            // packVersion
            // 
            this.packVersion.AutoSize = true;
            this.packVersion.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.packVersion.Location = new System.Drawing.Point(19, 74);
            this.packVersion.Name = "packVersion";
            this.packVersion.Size = new System.Drawing.Size(194, 27);
            this.packVersion.TabIndex = 21;
            this.packVersion.Text = "NewEra - Ultimate";
            // 
            // repairButton
            // 
            this.repairButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.repairButton.Font = new System.Drawing.Font("Yu Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repairButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.repairButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.repairButton.IconColor = System.Drawing.Color.Black;
            this.repairButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.repairButton.Location = new System.Drawing.Point(280, 326);
            this.repairButton.Name = "repairButton";
            this.repairButton.Size = new System.Drawing.Size(119, 40);
            this.repairButton.TabIndex = 20;
            this.repairButton.Text = "Repair";
            this.repairButton.UseVisualStyleBackColor = false;
            this.repairButton.Click += new System.EventHandler(this.RepairButton_Click);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.Transparent;
            this.topPanel.Controls.Add(this.CloseButton);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(425, 42);
            this.topPanel.TabIndex = 19;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.CloseButton.Location = new System.Drawing.Point(387, -1);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(40, 40);
            this.CloseButton.TabIndex = 16;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.nameLabel.Location = new System.Drawing.Point(19, 47);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(194, 27);
            this.nameLabel.TabIndex = 18;
            this.nameLabel.Text = "NewEra - Ultimate";
            // 
            // launchButton
            // 
            this.launchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.launchButton.Font = new System.Drawing.Font("Yu Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.launchButton.IconChar = FontAwesome.Sharp.IconChar.None;
            this.launchButton.IconColor = System.Drawing.Color.Black;
            this.launchButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.launchButton.Location = new System.Drawing.Point(24, 326);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(250, 40);
            this.launchButton.TabIndex = 15;
            this.launchButton.Text = "Launch";
            this.launchButton.UseVisualStyleBackColor = false;
            this.launchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(689, 398);
            this.ControlBox = false;
            this.Controls.Add(this.rightpanel);
            this.Controls.Add(this.leftpanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.leftpanel.ResumeLayout(false);
            this.leftpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            this.rightpanel.ResumeLayout(false);
            this.rightpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramSlider)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label statusText;
        private FontAwesome.Sharp.IconButton launchButton;
        private System.Windows.Forms.Panel leftpanel;
        private System.Windows.Forms.PictureBox leftPictureBox;
        private System.Windows.Forms.Panel rightpanel;
        private System.Windows.Forms.Label devLabel;
        private System.Windows.Forms.Label leftlabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel leftTopPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button CloseButton;
        private FontAwesome.Sharp.IconButton repairButton;
        private System.Windows.Forms.Label gameVersion;
        private System.Windows.Forms.Label packVersion;
        private System.Windows.Forms.TrackBar ramSlider;
        private System.Windows.Forms.Label ramLabel;
        private System.Windows.Forms.ComboBox versionBox;
        private System.Windows.Forms.ProgressBar downloadBar;
    }
}