using AkulavLauncher.Utilities;
using System;
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

        private static readonly string client_version = "8.0.1";
        private static readonly int ram = Utility.GetRAM();
        UIManager ui = new UIManager();
        private bool settingsFlag = false;

        public MainForm()
        {
            InitializeComponent();
            Utility.GetModpacks();
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
            ui.GetVersions(versionBox, nameLabel, packVersion);
            ramSlider.Maximum = ram + 1;
            ramSlider.Minimum = 1;
            ui.GetUserData(Username, ramSlider, ramLabel, ram, versionBox);
            ui.CheckUpdate(client_version, nameLabel, packVersion);
            ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
        }
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            if (Utility.IsValidUsername(Username))
            {
                Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
                ui.disableControl(launchButton);
                ui.disableControl(repairButton);
                ui.disableControl(settingsButton);
                ui.disableControl(versionBox);
                GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this, downloadBar);
            }
        }


        private void RepairButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DataDownloader data = new DataDownloader(this, Username.Text, ramSlider.Value * 1024, versionBox.Text, downloadBar);
            data.StartDownload();
            ui.disableControl(launchButton);
            ui.disableControl(settingsButton);
            ui.disableControl(repairButton);
        }

        private void RamSlider_ValueChanged(object sender, EventArgs e)
        {
            Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
            ui.setRamLabel(ramLabel, ramSlider);
        }


        private void VersionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
            ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            if (!settingsFlag)
            {
                ui.disableControl(launchButton);
                ui.disableControl(repairButton);
                ui.disableControl(versionBox);
                ui.disableControl(Username);
                ui.showSettingsForm(settingsPanel);
                settingsFlag = true;
            }
            else
            {
                ui.hideSettingsForm(settingsPanel);
                Utility.GetModpacks();
                ui.GetVersions(versionBox, nameLabel, packVersion);
                ui.GetUserData(Username, ramSlider, ramLabel, ram, versionBox);
                ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
                ui.enableControl(launchButton);
                ui.enableControl(repairButton);
                ui.enableControl(versionBox);
                ui.enableControl(Username);
                settingsFlag = false;

            }
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
        }
    }
}