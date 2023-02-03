using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Version;
using CmlLib.Core.VersionMetadata;
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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
        //Logic starts here
        public MainForm()
        {
            Utility.EnforceAdminPrivilegesWorkaround();
            InitializeComponent();
            CheckTheme();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
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
            Colors.ChangeTheme(rightpanel.Controls, this, "dark");
            Colors.ChangeTheme(leftpanel.Controls, this, "darker");
            leftpanel.BackColor = Colors.back_darker;
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            ramSlider.Maximum = Convert.ToInt32(Utility.getRam());
            try
            {
                MinecraftPath path = new MinecraftPath();
                CMLauncher launcher = new CMLauncher(path);
                MVersionCollection versions = await launcher.GetAllVersionsAsync();

                versionBox.Items.Add("NewEra Ultimate");
                // show all versions
                foreach (MVersionMetadata ver in versions)
                {
                    versionBox.Items.Add(ver.Name);
                }

                versionBox.SelectedItem = "NewEra Ultimate";

                DataDownloader data = new DataDownloader();

                nameLabel.Text = data.mod_name;
                packVersion.Text = "Pack Version: " + data.mod_version;
                gameVersion.Text = "Game Version: " + data.game_version;

                if (File.Exists(Paths.localUser))
                {
                    Username.Text = File.ReadAllText(Paths.localUser);
                }

                if (File.Exists(Paths.ramData))
                {
                    ramSlider.Value = Int32.Parse(File.ReadAllText(Paths.ramData).ToString());
                    ramLabel.Text = File.ReadAllText(Paths.ramData) + " GB of RAM";
                }

                if (!File.Exists(Paths.localMetadata) || File.ReadAllText(Paths.localMetadata) != data.mod_version)
                {
                    convertToUpdate();
                }

                data = null;
            }

            catch
            {
                nameLabel.Text = "Could Not Reach Server";
                packVersion.Text = "";
                gameVersion.Text = "";
            }

        }

        private void convertToUpdate()
        {
            repairButton.Text = "Update";
            launchButton.Enabled = false;
        }

        private void convertBackToRepair()
        {
            repairButton.Text = "Repair";
            launchButton.Enabled = true;
        }

        private async void launchButton_Click(object sender, EventArgs e)
        {
            MinecraftPath path = new MinecraftPath(); // use default directory
            CMLauncher launcher = new CMLauncher(path);
            launchButton.Enabled = false;

            File.WriteAllText(Paths.localUser, Username.Text);

            launcher.FileChanged += (p) =>
            {
                Console.WriteLine("[{0}] {1} - {2}/{3}", p.FileKind.ToString(), p.FileName, p.ProgressedFileCount, p.TotalFileCount);
            };
            launcher.ProgressChanged += (s, p) =>
            {
                downloadBar.Value = p.ProgressPercentage;
            };
            var session = MSession.GetOfflineSession(Username.Text);
            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ramSlider.Value * 1024,
                Session = session,
            };

            string version = versionBox.SelectedItem.ToString();
            if (version == "NewEra Ultimate")
            {
                version = "1.19.2-forge-43.2.4";
            }

            var process = await launcher.CreateProcessAsync(version, launchOption);

            this.Visible = false;
            this.ShowInTaskbar = false;

            process.Start();

            while (!process.WaitForExit(100)) ;

            launchButton.Enabled = true;
            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void repairButton_Click(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader();
            DirectoryLib.CreateFolder(Paths.temp);
            StartDownload(data.mod_url);
            data = null;
            Functionality.StartInstall();
        }

        private void StartDownload(string url)
        {
            Thread thread = new Thread(() =>
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(url), @"C:\NewEraCache\downloaded.zip");
            });
            thread.Start();
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                //progressLabel.Text = e.BytesReceived / 1000 / 1000 + "MB" + " " + " of " + e.TotalBytesToReceive / 1000 / 1000 + "MB";
                downloadBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                DataDownloader data = new DataDownloader();
                DirectoryLib.DeleteFolder(@"C:\NewEraCache\extracted");
                Functionality.ExtractInstall(data.mod_version);
                launchButton.Enabled = true;
                DirectoryLib.DeleteFolder(@"C:\NewEraCache");
                convertBackToRepair();
                data = null;
            });
        }

        private void ramSlider_ValueChanged(object sender, EventArgs e)
        {
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
            File.WriteAllText(Paths.ramData, ramSlider.Value.ToString());
        }
    }
}