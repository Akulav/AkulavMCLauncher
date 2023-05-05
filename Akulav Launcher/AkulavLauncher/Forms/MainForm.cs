using AkulavLauncher.Data;
using AkulavLauncher.Utilities;
using Newtonsoft.Json;
using PasswordManager;
using PasswordManager.Utilities;
using System;
using System.Collections.Generic;
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
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        public static readonly string client_version = "3.1.1";

        //Logic starts here
        public MainForm()
        {
            Utility.EnforceAdminPrivilegesWorkaround();
            InitializeComponent();
            GetUserData();
        }


        //Sets the user data to the json settings file
        private void SetUserData()
        {
            var ud = new UserData
            {
                UserName = Username.Text,
                Ram = ramSlider.Value.ToString()
            };
            string jsonString = JsonConvert.SerializeObject(ud, Formatting.Indented);
            File.WriteAllText(Paths.settings, jsonString);

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

        //Sets the UI using the json settings file, if doesnt exists sets default
        private void GetUserData()
        {
            try
            {
                string userdata = File.ReadAllText(Paths.settings);
                UserData ud = JsonConvert.DeserializeObject<UserData>(userdata);
                Username.Text = ud.UserName;
                GetPhysicallyInstalledSystemMemory(out long memKb);
                ramSlider.Minimum = 1;
                ramSlider.Maximum = Convert.ToInt32(memKb / 1024 / 1024);
                ramSlider.Value = Int32.Parse(ud.Ram);
                ramLabel.Text = ud.Ram + " GB of RAM";
            }
            catch (IOException)
            {
                GetPhysicallyInstalledSystemMemory(out long memKb);
                ramSlider.Minimum = 1;
                ramSlider.Maximum = Convert.ToInt32(memKb / 1024 / 1024);
                ramSlider.Value = ramSlider.Maximum / 2;
                ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
            }
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

        //needs improvement
        private void MainForm_Load(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.GetVersions();
            data.SetUIText();
        }
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            SetUserData();
            GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this);
        }

        //needs improvement
        private void RepairButton_Click(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.StartDownload();
            data.StartInstall();
        }

        private void RamSlider_ValueChanged(object sender, EventArgs e)
        {
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
        }

        //needs improvement
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

        //Optimized
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //Optimized
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