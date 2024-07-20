namespace AkulavLauncher.Forms
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.topPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.topNameLabel = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textBoxNewLink = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.linkLabel = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.resetSettingsButton = new System.Windows.Forms.Button();
            this.removeModpacksButton = new System.Windows.Forms.Button();
            this.McFolderButton = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.topPanel.Controls.Add(this.CloseButton);
            this.topPanel.Controls.Add(this.topNameLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(652, 40);
            this.topPanel.TabIndex = 0;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CloseButton.Location = new System.Drawing.Point(604, 6);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 30);
            this.CloseButton.TabIndex = 17;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // topNameLabel
            // 
            this.topNameLabel.AutoSize = true;
            this.topNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topNameLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.topNameLabel.Location = new System.Drawing.Point(286, 9);
            this.topNameLabel.Name = "topNameLabel";
            this.topNameLabel.Size = new System.Drawing.Size(75, 22);
            this.topNameLabel.TabIndex = 0;
            this.topNameLabel.Text = "Settings";
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAdd.Location = new System.Drawing.Point(480, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 34);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // textBoxNewLink
            // 
            this.textBoxNewLink.Location = new System.Drawing.Point(129, 54);
            this.textBoxNewLink.Name = "textBoxNewLink";
            this.textBoxNewLink.Size = new System.Drawing.Size(345, 20);
            this.textBoxNewLink.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.Location = new System.Drawing.Point(561, 46);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 34);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.linkLabel.Location = new System.Drawing.Point(9, 55);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(114, 17);
            this.linkLabel.TabIndex = 7;
            this.linkLabel.Text = "Modpack Source";
            // 
            // resetSettingsButton
            // 
            this.resetSettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetSettingsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.resetSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetSettingsButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.resetSettingsButton.Location = new System.Drawing.Point(12, 97);
            this.resetSettingsButton.Name = "resetSettingsButton";
            this.resetSettingsButton.Size = new System.Drawing.Size(200, 34);
            this.resetSettingsButton.TabIndex = 8;
            this.resetSettingsButton.Text = "Reset Settings";
            this.resetSettingsButton.UseVisualStyleBackColor = true;
            this.resetSettingsButton.Click += new System.EventHandler(this.resetSettingsButton_Click);
            // 
            // removeModpacksButton
            // 
            this.removeModpacksButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeModpacksButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.removeModpacksButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeModpacksButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.removeModpacksButton.Location = new System.Drawing.Point(436, 97);
            this.removeModpacksButton.Name = "removeModpacksButton";
            this.removeModpacksButton.Size = new System.Drawing.Size(200, 34);
            this.removeModpacksButton.TabIndex = 9;
            this.removeModpacksButton.Text = "Remove Modpacks";
            this.removeModpacksButton.UseVisualStyleBackColor = true;
            this.removeModpacksButton.Click += new System.EventHandler(this.removeModpacksButton_Click);
            // 
            // McFolderButton
            // 
            this.McFolderButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.McFolderButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.McFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.McFolderButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.McFolderButton.Location = new System.Drawing.Point(218, 97);
            this.McFolderButton.Name = "McFolderButton";
            this.McFolderButton.Size = new System.Drawing.Size(212, 34);
            this.McFolderButton.TabIndex = 10;
            this.McFolderButton.Text = "Open Minecraft Folder";
            this.McFolderButton.UseVisualStyleBackColor = true;
            this.McFolderButton.Click += new System.EventHandler(this.McFolderButton_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(652, 143);
            this.ControlBox = false;
            this.Controls.Add(this.McFolderButton);
            this.Controls.Add(this.removeModpacksButton);
            this.Controls.Add(this.resetSettingsButton);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.textBoxNewLink);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SettingForm";
            this.TopMost = true;
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label topNameLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textBoxNewLink;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label linkLabel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button resetSettingsButton;
        private System.Windows.Forms.Button removeModpacksButton;
        private System.Windows.Forms.Button McFolderButton;
    }
}