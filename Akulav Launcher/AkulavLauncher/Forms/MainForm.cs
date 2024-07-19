using AkulavLauncher.Forms;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Media;
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

        public static readonly string client_version = "7.0.0";
        public static readonly int ram = Utility.GetRAM();

        public MainForm()
        {
            InitializeComponent();
            GetUserData();
            Utility.GetModpacks();
        }

        private void GetUserData()
        {
            try
            {
                UserData ud = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Paths.settings));
                UpdateUserDataUI(ud);
            }
            catch
            {
                SetDefaultValues();
            }
        }

        private void UpdateUserDataUI(UserData userData)
        {
            Username.Text = userData.UserName;
            int maxRam = ram;
            ramSlider.Minimum = 1;
            ramSlider.Maximum = maxRam + 1;
            ramSlider.Value = ParseRam(userData.Ram, maxRam);
            ramLabel.Text = userData.Ram + " GB of RAM";
        }

        private int ParseRam(string ram, int maxRam)
        {
            if (int.TryParse(ram, out int parsedRam))
            {
                return parsedRam;
            }
            else
            {
                // Default value if parsing fails
                return maxRam / 2;
            }
        }

        private void SetDefaultValues()
        {
            ramSlider.Minimum = 1;
            ramSlider.Maximum = ram + 1;
            ramSlider.Value = ram / 2;
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
            Username.Text = "Steve";
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
            data.GetData();
        }
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            PlaySound();
            Utility.SetUserData(Username.Text, ramSlider.Value.ToString(), versionBox.Text);
            GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this);
        }
        private void PlaySound()
        {
            try
            {
                using (var player = new SoundPlayer(Properties.Resources.lever))
                {
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }



        private void RepairButton_Click(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.StartDownload();
            launchButton.Enabled = false;
        }

        private void RamSlider_ValueChanged(object sender, EventArgs e)
        {
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
        }


        private void VersionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.GetData();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingForm sf = new SettingForm(this);
            sf.ShowDialog();
        }
    }
}