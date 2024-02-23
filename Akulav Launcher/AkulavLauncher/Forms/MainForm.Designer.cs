
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
            this.lowerpanel = new System.Windows.Forms.Panel();
            this.repairButton = new System.Windows.Forms.Button();
            this.launchButton = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.versionBox = new System.Windows.Forms.ComboBox();
            this.consoleLabel = new System.Windows.Forms.Label();
            this.ramLabel = new System.Windows.Forms.Label();
            this.ramSlider = new System.Windows.Forms.TrackBar();
            this.downloadBar = new System.Windows.Forms.ProgressBar();
            this.packVersion = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.leftlabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.lowerpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramSlider)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(10, 23);
            this.Username.Multiline = true;
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(290, 21);
            this.Username.TabIndex = 6;
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.BackColor = System.Drawing.Color.Transparent;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.userLabel.Location = new System.Drawing.Point(103, -1);
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
            // lowerpanel
            // 
            this.lowerpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.lowerpanel.BackgroundImage = global::AkulavLauncher.Properties.Resources.minecraft_dirt_wallpaper_by_thedevartist_d3l1qoj_3511026514;
            this.lowerpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lowerpanel.Controls.Add(this.repairButton);
            this.lowerpanel.Controls.Add(this.launchButton);
            this.lowerpanel.Controls.Add(this.versionLabel);
            this.lowerpanel.Controls.Add(this.statusText);
            this.lowerpanel.Controls.Add(this.Username);
            this.lowerpanel.Controls.Add(this.userLabel);
            this.lowerpanel.Controls.Add(this.versionBox);
            this.lowerpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lowerpanel.Location = new System.Drawing.Point(0, 574);
            this.lowerpanel.Name = "lowerpanel";
            this.lowerpanel.Size = new System.Drawing.Size(1241, 55);
            this.lowerpanel.TabIndex = 17;
            // 
            // repairButton
            // 
            this.repairButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.repairButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.repairButton.Location = new System.Drawing.Point(1060, 10);
            this.repairButton.Name = "repairButton";
            this.repairButton.Size = new System.Drawing.Size(171, 40);
            this.repairButton.TabIndex = 31;
            this.repairButton.Text = "Repair  and  Launch";
            this.repairButton.UseVisualStyleBackColor = false;
            this.repairButton.Click += new System.EventHandler(this.RepairButton_Click);
            // 
            // launchButton
            // 
            this.launchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.launchButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.launchButton.Location = new System.Drawing.Point(533, 10);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(518, 40);
            this.launchButton.TabIndex = 30;
            this.launchButton.Text = "Launch";
            this.launchButton.UseVisualStyleBackColor = false;
            this.launchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.versionLabel.Location = new System.Drawing.Point(353, -1);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(111, 20);
            this.versionLabel.TabIndex = 28;
            this.versionLabel.Text = "Game Version";
            // 
            // versionBox
            // 
            this.versionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionBox.FormattingEnabled = true;
            this.versionBox.Location = new System.Drawing.Point(306, 23);
            this.versionBox.Name = "versionBox";
            this.versionBox.Size = new System.Drawing.Size(221, 21);
            this.versionBox.TabIndex = 27;
            this.versionBox.SelectedIndexChanged += new System.EventHandler(this.VersionBox_SelectedIndexChanged);
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Font = new System.Drawing.Font("Yu Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.consoleLabel.Location = new System.Drawing.Point(890, 204);
            this.consoleLabel.Name = "consoleLabel";
            this.consoleLabel.Size = new System.Drawing.Size(0, 18);
            this.consoleLabel.TabIndex = 28;
            // 
            // ramLabel
            // 
            this.ramLabel.AutoSize = true;
            this.ramLabel.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.ramLabel.Location = new System.Drawing.Point(888, 72);
            this.ramLabel.Name = "ramLabel";
            this.ramLabel.Size = new System.Drawing.Size(71, 27);
            this.ramLabel.TabIndex = 26;
            this.ramLabel.Text = "RAM: ";
            // 
            // ramSlider
            // 
            this.ramSlider.Location = new System.Drawing.Point(888, 102);
            this.ramSlider.Name = "ramSlider";
            this.ramSlider.Size = new System.Drawing.Size(348, 45);
            this.ramSlider.TabIndex = 25;
            this.ramSlider.ValueChanged += new System.EventHandler(this.RamSlider_ValueChanged);
            // 
            // downloadBar
            // 
            this.downloadBar.Location = new System.Drawing.Point(1, 570);
            this.downloadBar.Name = "downloadBar";
            this.downloadBar.Size = new System.Drawing.Size(1240, 10);
            this.downloadBar.TabIndex = 24;
            // 
            // packVersion
            // 
            this.packVersion.AutoSize = true;
            this.packVersion.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.packVersion.Location = new System.Drawing.Point(888, 150);
            this.packVersion.Name = "packVersion";
            this.packVersion.Size = new System.Drawing.Size(194, 27);
            this.packVersion.TabIndex = 21;
            this.packVersion.Text = "NewEra - Ultimate";
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.topPanel.Controls.Add(this.minimizeButton);
            this.topPanel.Controls.Add(this.CloseButton);
            this.topPanel.Controls.Add(this.leftlabel);
            this.topPanel.Location = new System.Drawing.Point(1, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1240, 51);
            this.topPanel.TabIndex = 19;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.minimizeButton.Location = new System.Drawing.Point(1151, 5);
            this.minimizeButton.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(40, 40);
            this.minimizeButton.TabIndex = 17;
            this.minimizeButton.Text = "_";
            this.minimizeButton.UseVisualStyleBackColor = true;
            this.minimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.CloseButton.Location = new System.Drawing.Point(1191, 5);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(40, 40);
            this.CloseButton.TabIndex = 16;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // leftlabel
            // 
            this.leftlabel.AutoSize = true;
            this.leftlabel.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftlabel.ForeColor = System.Drawing.Color.White;
            this.leftlabel.Location = new System.Drawing.Point(411, 13);
            this.leftlabel.Name = "leftlabel";
            this.leftlabel.Size = new System.Drawing.Size(398, 27);
            this.leftlabel.TabIndex = 16;
            this.leftlabel.Text = "Akulav Launcher V6.9.2 - End Of An Era";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nameLabel.Location = new System.Drawing.Point(888, 177);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(194, 27);
            this.nameLabel.TabIndex = 18;
            this.nameLabel.Text = "NewEra - Ultimate";
            // 
            // centerPanel
            // 
            this.centerPanel.BackgroundImage = global::AkulavLauncher.Properties.Resources.minecraft_great_cave_walls_forest_hd_mm9b3bqe6qc6s96f;
            this.centerPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.centerPanel.Location = new System.Drawing.Point(1, 51);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(881, 519);
            this.centerPanel.TabIndex = 29;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1241, 629);
            this.ControlBox = false;
            this.Controls.Add(this.consoleLabel);
            this.Controls.Add(this.ramLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.ramSlider);
            this.Controls.Add(this.packVersion);
            this.Controls.Add(this.centerPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.lowerpanel);
            this.Controls.Add(this.downloadBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.lowerpanel.ResumeLayout(false);
            this.lowerpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ramSlider)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.Panel lowerpanel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label packVersion;
        private System.Windows.Forms.TrackBar ramSlider;
        private System.Windows.Forms.Label ramLabel;
        private System.Windows.Forms.ComboBox versionBox;
        private System.Windows.Forms.ProgressBar downloadBar;
        private System.Windows.Forms.Label consoleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label leftlabel;
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Button repairButton;
    }
}