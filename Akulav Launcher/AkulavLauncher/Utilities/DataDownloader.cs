using AkulavLauncher;
using CmlLib.Core;
using CmlLib.Core.Version;
using CmlLib.Core.VersionMetadata;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PasswordManager.Utilities
{
    internal sealed class DataDownloader
    {
        string game_version;
        string mod_version;
        string mod_name;
        string mod_url;

        readonly private Form mf;
        private readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        bool flag = true;

        //References to controls from mainform
        readonly TextBox Username = Application.OpenForms["MainForm"].Controls.Find("Username", true)[0] as TextBox;
        readonly TrackBar ramSlider = Application.OpenForms["MainForm"].Controls.Find("ramSlider", true)[0] as TrackBar;
        readonly Label ramLabel = Application.OpenForms["MainForm"].Controls.Find("ramLabel", true)[0] as Label;
        readonly ComboBox versionBox = Application.OpenForms["MainForm"].Controls.Find("versionBox", true)[0] as ComboBox;
        readonly IconButton repairButton = Application.OpenForms["MainForm"].Controls.Find("repairButton", true)[0] as IconButton;
        readonly IconButton launchButton = Application.OpenForms["MainForm"].Controls.Find("launchButton", true)[0] as IconButton;
        readonly Label nameLabel = Application.OpenForms["MainForm"].Controls.Find("nameLabel", true)[0] as Label;
        readonly Label packVersion = Application.OpenForms["MainForm"].Controls.Find("packVersion", true)[0] as Label;
        readonly Label gameVersion = Application.OpenForms["MainForm"].Controls.Find("gameVersion", true)[0] as Label;
        readonly ProgressBar downloadBar = Application.OpenForms["MainForm"].Controls.Find("downloadBar", true)[0] as ProgressBar;
        public DataDownloader(Form mainform)
        {
            GetData();
            this.mf = mainform;
        }

        private void checkUpdate()
        {
            try
            {
                List<string> metadata = new List<string>();
                using (WebClient client = new WebClient())
                {
                    string[] result;
                    string update_data = client.DownloadString(Paths.versionUrl);
                    result = Regex.Split(update_data, "\r\n|\r|\n");
                    foreach (string s in result)
                    {
                        metadata.Add(s);
                    }
                }
                if (MainForm.client_version != metadata[0])
                {
                    Thread thread = new Thread(() =>
                    {
                        WebClient client = new WebClient();
                        client.DownloadProgressChanged += client_DownloadProgressChanged;
                        client.DownloadFileAsync(new Uri(metadata[1]), @"C:\AkulavLauncher\update.exe");
                    });
                    thread.Start();

                }

                else
                {
                    File.Delete(@"C:\AkulavLauncher\update.exe");
                }
            }

            catch
            {
                mod_name = "SERVER COULD NOT BE REACHED";
                mod_version = "";
                game_version = "";
                mod_url = "";
            }
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100 && flag == true)
            {
                flag = false;
                var p = new Process();
                p.StartInfo.FileName = @"C:\AkulavLauncher\update.exe";
                p.Start();
                Application.Exit();
            }
        }


        private void GetData()
        {
            checkUpdate();
            try
            {
                List<string> metadata = new List<string>();
                using (WebClient client = new WebClient())
                {
                    string[] result;
                    string update_data = client.DownloadString(Paths.url);
                    result = Regex.Split(update_data, "\r\n|\r|\n");
                    foreach (string s in result)
                    {
                        metadata.Add(s);
                    }
                }
                mod_name = metadata[0];
                mod_version = metadata[1];
                game_version = metadata[2];
                mod_url = metadata[3];
            }

            catch
            {
                mod_name = "SERVER COULD NOT BE REACHED";
                mod_version = "";
                game_version = "";
                mod_url = "";
            }
        }

        public bool CheckLocal()
        {
            if (!File.Exists(Paths.localMetadata) || File.ReadAllText(Paths.localMetadata) != mod_version)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public void SetMetadata()
        {
            if (File.Exists(Paths.localUser))
            {
                Username.Text = File.ReadAllText(Paths.localUser);
            }


            if (File.Exists(Paths.ramData))
            {
                ramSlider.Value = Int32.Parse(File.ReadAllText(Paths.ramData).ToString());
                ramLabel.Text = File.ReadAllText(Paths.ramData) + " GB of RAM";
            }
        }

        public async void GetVersions()
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
            launchButton.Size = new System.Drawing.Size(518, 40);
        }

        public void SetUIText()
        {
            nameLabel.Text = mod_name;
            packVersion.Text = "Pack Version: " + mod_version;
            gameVersion.Text = "Game Version: " + game_version;
        }

        public void StartDownload()
        {
            DirectoryLib.CreateFolder(Paths.temp);
            Thread thread = new Thread(() =>
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(mod_url), @"C:\NewEraCache\downloaded.zip");
            });
            thread.Start();
            launchButton.Enabled = false;
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            mf.BeginInvoke((MethodInvoker)delegate
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
            mf.BeginInvoke((MethodInvoker)delegate
            {
                DirectoryLib.DeleteFolder(@"C:\NewEraCache\extracted");
                ExtractInstall(mod_version);
                DirectoryLib.DeleteFolder(@"C:\NewEraCache");
                GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), mf);
            });
        }

        public void StartInstall()
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < Paths.deletion_list.Length; i++)
                {
                    DirectoryLib.DeleteFolder(Paths.deletion_list[i]);
                }

            });
            thread.Start();
        }

        public void ExtractInstall(string version)
        {
            try
            {
                ZipFile.ExtractToDirectory(@"C:\NewEraCache\downloaded.zip", @"C:\NewEraCache\extracted\");
                DirectoryLib.CopyFilesRecursively(@"C:\NewEraCache\extracted\", appdata + @"\.minecraft\");
                File.WriteAllText(Paths.localMetadata, version);
            }
            catch (IOException)
            {

            }
        }
    }
}
