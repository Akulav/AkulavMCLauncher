using Newtonsoft.Json;
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
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        public static readonly string client_version = "6.0.0";

        //Logic starts here
        public MainForm()
        {
            Utility.EnforceAdminPrivilegesWorkaround();
            InitializeComponent();
            GetUserData();
            Utility.GetModpacks();
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
            data.GetData();
        }
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            UserData ud = new UserData();
            ud.SetUserData(Username.Text, ramSlider.Value.ToString());
            GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this);
        }

        //needs improvement
        private void RepairButton_Click(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.StartDownload();
        }

        private void RamSlider_ValueChanged(object sender, EventArgs e)
        {
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
        }

        //needs improvement
        private void VersionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.GetData();
        }

        //Optimized
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}