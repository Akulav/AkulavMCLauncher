using PasswordManager;
using PasswordManager.Utilities;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace AkulavLauncher
{
    public partial class MainForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public static readonly string client_version = "3.1.1";

        //Logic starts here
        public MainForm()
        {
            Utility.EnforceAdminPrivilegesWorkaround();
            InitializeComponent();
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
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
            if (Directory.Exists(Paths.skin))
            {
                string filepath = Paths.skin;
                DirectoryInfo d = new DirectoryInfo(filepath);
                foreach (var file in d.GetFiles("*.png"))
                {

                    if (Path.GetFileNameWithoutExtension(file.FullName) != Username.Text)
                    {
                        Directory.Move(file.FullName, filepath + Username.Text + ".png");
                    }

                }
            }

        }

        private void VersionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(versionBox.SelectedItem.ToString() == "NewEra Ultimate"))
            {
                gameVersion.Text = "Game Version: " + versionBox.SelectedItem.ToString();
                packVersion.Text = "";
                nameLabel.Text = "";
                skinButton.Visible = false;
                launchButton.Size = new System.Drawing.Size(695, 40);
            }

            else
            {
                DataDownloader data = new DataDownloader(this);
                data.SetUIText();
                skinButton.Visible = true;
                launchButton.Size = new System.Drawing.Size(518, 40);
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SkinButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*",
                FilterIndex = 1
            };
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Paths.skin + "\\" + Username.Text + ".png"))
                {
                    File.Delete(Paths.skin + "\\" + Username.Text + ".png");
                }
                File.Copy(choofdlog.FileName, Paths.skin + "\\" + Username.Text + ".png");
            }

        }
    }
}