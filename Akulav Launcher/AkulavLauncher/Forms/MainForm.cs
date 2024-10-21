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

        private static readonly string client_version = "8.1.0";
        private static readonly int ram = Utility.GetRAM();
        private UIManager ui = new UIManager();
        private bool settingsFlag = false;

        public MainForm()
        {
            InitializeComponent();
            Utility.GetModpacks();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            if (Utility.IsValidUsername(Username.Text))
            {
                SaveUserData();
                DisableAllControls();
                var gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this, downloadBar);
            }
        }

        private void RepairButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            StartRepairProcess();
        }

        private void RamSlider_ValueChanged(object sender, EventArgs e)
        {
            UpdateUserData();
            ui.SetRamLabel(ramLabel, ramSlider);
        }

        private void VersionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserData();
            ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            ToggleSettingsForm();
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            UpdateUserData();
        }

        // Private helper methods

        private void InitializeUI()
        {
            ui.GetVersions(versionBox, nameLabel, packVersion);
            ramSlider.Minimum = 1;
            ramSlider.Maximum = ram + 1;
            ui.GetUserData(Username, ramSlider, ramLabel, ram, versionBox);
            ui.CheckUpdate(client_version, nameLabel, packVersion);
            ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
        }

        private void UpdateUserData()
        {
            Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
        }

        private void SaveUserData()
        {
            Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
        }

        private void DisableAllControls()
        {
            ui.DisableControl(launchButton);
            ui.DisableControl(repairButton);
            ui.DisableControl(versionBox);
            ui.DisableControl(Username);
        }

        private void EnableAllControls()
        {
            ui.EnableControl(launchButton);
            ui.EnableControl(repairButton);
            ui.EnableControl(versionBox);
            ui.EnableControl(Username);
        }

        private void StartRepairProcess()
        {
            var data = new DataDownloader(this, Username.Text, ramSlider.Value * 1024, versionBox.Text, downloadBar);
            data.StartDownload();
            DisableAllControls();
        }

        private void ToggleSettingsForm()
        {
            if (!settingsFlag)
            {
                DisableAllControls();
                ui.ShowSettingsForm(settingsPanel);
            }
            else
            {
                ui.HideSettingsForm(settingsPanel);
                RefreshUIAfterSettings();
                EnableAllControls();
            }
            settingsFlag = !settingsFlag;
        }

        private void RefreshUIAfterSettings()
        {
            Utility.GetModpacks();
            ui.GetVersions(versionBox, nameLabel, packVersion);
            ui.GetUserData(Username, ramSlider, ramLabel, ram, versionBox);
            ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
        }
    }
}
