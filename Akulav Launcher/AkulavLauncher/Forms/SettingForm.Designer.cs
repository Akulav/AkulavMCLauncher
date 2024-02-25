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
            this.listBoxLinks = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEnable = new System.Windows.Forms.Button();
            this.disableBtn = new System.Windows.Forms.Button();
            this.linkLabel = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.enableLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.serparator = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.topPanel.Controls.Add(this.CloseButton);
            this.topPanel.Controls.Add(this.topNameLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(641, 40);
            this.topPanel.TabIndex = 0;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
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
            this.btnAdd.Location = new System.Drawing.Point(554, 61);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 20);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxNewLink
            // 
            this.textBoxNewLink.Location = new System.Drawing.Point(129, 61);
            this.textBoxNewLink.Name = "textBoxNewLink";
            this.textBoxNewLink.Size = new System.Drawing.Size(419, 20);
            this.textBoxNewLink.TabIndex = 2;
            // 
            // listBoxLinks
            // 
            this.listBoxLinks.FormattingEnabled = true;
            this.listBoxLinks.Location = new System.Drawing.Point(12, 87);
            this.listBoxLinks.Name = "listBoxLinks";
            this.listBoxLinks.Size = new System.Drawing.Size(536, 82);
            this.listBoxLinks.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(554, 87);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(554, 116);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(75, 23);
            this.btnEnable.TabIndex = 5;
            this.btnEnable.Text = "Enable";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // disableBtn
            // 
            this.disableBtn.Location = new System.Drawing.Point(554, 145);
            this.disableBtn.Name = "disableBtn";
            this.disableBtn.Size = new System.Drawing.Size(75, 23);
            this.disableBtn.TabIndex = 6;
            this.disableBtn.Text = "Disable";
            this.disableBtn.UseVisualStyleBackColor = true;
            this.disableBtn.Click += new System.EventHandler(this.disableBtn_Click);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.linkLabel.Location = new System.Drawing.Point(9, 62);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(114, 17);
            this.linkLabel.TabIndex = 7;
            this.linkLabel.Text = "Modpack Source";
            // 
            // enableLabel
            // 
            this.enableLabel.AutoSize = true;
            this.enableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enableLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.enableLabel.Location = new System.Drawing.Point(12, 172);
            this.enableLabel.Name = "enableLabel";
            this.enableLabel.Size = new System.Drawing.Size(68, 17);
            this.enableLabel.TabIndex = 8;
            this.enableLabel.Text = "Enabled: ";
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.sourceLabel.Location = new System.Drawing.Point(86, 172);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(0, 17);
            this.sourceLabel.TabIndex = 9;
            // 
            // serparator
            // 
            this.serparator.Enabled = false;
            this.serparator.Location = new System.Drawing.Point(12, 192);
            this.serparator.Name = "serparator";
            this.serparator.Size = new System.Drawing.Size(617, 10);
            this.serparator.TabIndex = 10;
            this.serparator.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Disable";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(641, 376);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.serparator);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.enableLabel);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.disableBtn);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.listBoxLinks);
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
        private System.Windows.Forms.ListBox listBoxLinks;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button disableBtn;
        private System.Windows.Forms.Label linkLabel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label enableLabel;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Button serparator;
        private System.Windows.Forms.Button button1;
    }
}