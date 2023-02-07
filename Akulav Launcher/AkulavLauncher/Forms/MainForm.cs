using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PasswordManager;
using PasswordManager.Utilities;


namespace AkulavLauncher
{
    public partial class MainForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        //Logic starts here
        public MainForm()
        {
            Utility.EnforceAdminPrivilegesWorkaround();
            InitializeComponent();
            CheckTheme();
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void LeftTopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }


        private void CheckTheme()
        {
            Colors.ChangeTheme(Controls, this, "dark");
            Colors.ChangeTheme(lowerpanel.Controls, this, "darker");
            Colors.ChangeTheme(topPanel.Controls, this, "darker");
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.GetVersions();
            data.SetUIText();
            Utility.SetRam();
            data.SetMetadata();
        }
        private void LaunchButton_Click(object sender, EventArgs e)
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }

        private void RepairButton_Click(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.StartDownload();
            data.StartInstall();
        }

        private void RamSlider_ValueChanged(object sender, EventArgs e)
        {
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
            File.WriteAllText(Paths.ramData, ramSlider.Value.ToString());
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Paths.localUser, Username.Text);
        }

        private void versionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(versionBox.SelectedItem.ToString() == "NewEra Ultimate"))
            {
                gameVersion.Text = "Game Version: " + versionBox.SelectedItem.ToString();
                packVersion.Text = "";
                nameLabel.Text = "";
                launchButton.Size = new System.Drawing.Size(695, 40);
            }

            else
            {
                DataDownloader data = new DataDownloader(this);
                data.SetUIText();
                launchButton.Size = new System.Drawing.Size(518, 40);
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}