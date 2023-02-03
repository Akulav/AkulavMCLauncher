using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
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

        List<string> metadata = new List<string>();

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


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string[] result;
                    string update_data = client.DownloadString(Paths.url);
                    result = Regex.Split(update_data, "\r\n|\r|\n");
                    for (int i = 0; i < result.Length; i++)
                    {
                        metadata.Add(result[i]);
                    }
                    metadata.Remove("");
                }

                nameLabel.Text = metadata[0].ToString();
                packVersion.Text = "Pack Version: " + metadata[1].ToString();
                gameVersion.Text = "Game Version: " + metadata[2].ToString();

                if (File.Exists(Paths.localUser))
                {
                    Username.Text = File.ReadAllText(Paths.localUser);
                }
            }

            catch
            {
                nameLabel.Text = "Could Not Reach Server";
                packVersion.Text = "";
                gameVersion.Text = "";
            }

            if (!File.Exists(Paths.localMetadata) || File.ReadAllText(Paths.localMetadata) != metadata[1])
            {
                convertToUpdate();
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
            launchButton.Enabled = false;
            var path = new MinecraftPath(); // use default directory

            var launcher = new CMLauncher(path);

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
                MaximumRamMb = 4096,
                Session = session,
            };

            var process = await launcher.CreateProcessAsync("1.19.2-forge-43.2.4", launchOption);

            this.Visible = false;
            this.ShowInTaskbar = false;

            process.Start();

            while (!process.WaitForExit(100)) ;

            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void repairButton_Click(object sender, EventArgs e)
        {
            directoryLib.CreateFolder(Paths.temp);
            StartDownload(metadata[3]);
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
                //progressLabel.Text = "Downloading";

                directoryLib.DeleteFolder(@"C:\NewEraCache\extracted");
                Functionality.ExtractInstall(metadata[1]);

                launchButton.Enabled = true;
                //progressLabel.Text = "Success";
                directoryLib.DeleteFolder(@"C:\NewEraCache");
                convertBackToRepair();
            });
        }


    }
}