using AkulavLauncher.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AkulavLauncher
{
    internal sealed class DataDownloader
    {
        private readonly Form mainForm;
        private bool flag = true;

        private readonly TextBox usernameTextBox;
        private readonly TrackBar ramSlider;
        private readonly ComboBox versionBox;
        private readonly Label nameLabel;
        private readonly Label packVersion;
        private readonly ProgressBar downloadBar;
        private readonly List<ModpackData> modpackData;

        public DataDownloader(Form mainForm)
        {
            this.mainForm = mainForm;
            usernameTextBox = mainForm.Controls.Find("Username", true)[0] as TextBox;
            ramSlider = mainForm.Controls.Find("ramSlider", true)[0] as TrackBar;
            versionBox = mainForm.Controls.Find("versionBox", true)[0] as ComboBox;
            nameLabel = mainForm.Controls.Find("nameLabel", true)[0] as Label;
            packVersion = mainForm.Controls.Find("packVersion", true)[0] as Label;
            downloadBar = mainForm.Controls.Find("downloadBar", true)[0] as ProgressBar;
            modpackData = Utility.modpacks;
        }

        private void CheckUpdate()
        {
            try
            {
                var metadata = new List<string>();
                using (WebClient client = new WebClient())
                {
                    string updateData = client.DownloadString(Paths.versionUrl);
                    metadata.AddRange(Regex.Split(updateData, "\r\n|\r|\n"));
                }

                if (MainForm.client_version != metadata[0])
                {
                    var client = new WebClient();
                    client.DownloadProgressChanged += Client_DownloadProgressChangedVersion;
                    client.DownloadFileAsync(new Uri(metadata[1]), Paths.update);
                }
                else
                {
                    File.Delete(Paths.update);
                }
            }
            catch
            {
                nameLabel.Text = "Server could not be reached.";
                packVersion.Text = "";
            }
        }

        private void Client_DownloadProgressChangedVersion(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100 && flag)
            {
                flag = false;
                var p = new Process();
                p.StartInfo.FileName = Paths.update;
                p.Start();
                Application.Exit();
            }
        }

        public void GetData()
        {
            try
            {
                var selectedModpack = modpackData.Find(modpack => modpack.Name == versionBox.Text);
                nameLabel.Text = selectedModpack != null ? "Modpack: " + selectedModpack.Name : "Server could not be reached.";
                packVersion.Text = selectedModpack != null ? "Pack Version: " + selectedModpack.Version : "";
            }
            catch
            {
                nameLabel.Text = "Server could not be reached.";
                packVersion.Text = "";
            }
        }

        public bool CheckLocal()
        {
            if (File.Exists(Paths.localMetadata))
            {
                var local = JsonConvert.DeserializeObject<List<ModpackData>>(File.ReadAllText(Paths.localMetadata));

                var localIndex = GetListIndex(local, versionBox.Text);
                var modpackIndex = GetListIndex(modpackData, versionBox.Text);

                try
                {
                    return local[localIndex].Version != modpackData[modpackIndex].Version;
                }

                catch
                {
                    return true;
                }
            }

            return true;
        }

        private int GetListIndex(List<ModpackData> list, string name) => list.FindIndex(item => item.Name == name);

        public void GetVersions()
        {
            CheckUpdate();
            try
            {
                foreach (var modpack in modpackData)
                {
                    versionBox.Items.Add(modpack.Name);
                }

                versionBox.SelectedIndex = 0;
                GetData();
            }
            catch { }
        }

        public void StartDownload()
        {
            foreach (var modpack in modpackData)
            {
                if (versionBox.Text == modpack.Name)
                {
                    var url = modpack.URL;
                    DirectoryLib.CreateFolder(Paths.temp);

                    var client = new WebClient();
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    client.DownloadFileAsync(new Uri(url), Paths.downloaded);
                }
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                var bytesIn = double.Parse(e.BytesReceived.ToString());
                var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                var percentage = bytesIn / totalBytes * 100;
                downloadBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                DirectoryLib.DeleteFolder(Paths.extracted);
                var name = modpackData.Find(modpack => modpack.Name == versionBox.Text)?.Name;
                ExtractInstall(name);
                DirectoryLib.DeleteFolder(Paths.cache);
            });
        }

        private void ExtractInstall(string name)
        {
            try
            {
                var data = modpackData.Find(modpack => modpack.Name == name);
                foreach (var folder in Paths.deletion_list)
                {
                    DirectoryLib.DeleteFolder(Path.Combine(Paths.mc, name, folder));
                }

                ZipFile.ExtractToDirectory(Paths.downloaded, Paths.extracted);
                DirectoryLib.CopyFilesRecursively(Paths.extracted, Path.Combine(Paths.mc, name));

                // Read existing JSON content
                List<ModpackData> existingData = new List<ModpackData>();
                if (File.Exists(Paths.localMetadata))
                {
                    string existingJson = File.ReadAllText(Paths.localMetadata);
                    existingData = JsonConvert.DeserializeObject<List<ModpackData>>(existingJson);
                }

                // Check if the modpack already exists in local metadata
                var existingModpack = existingData.Find(modpack => modpack.Name == name);
                if (existingModpack != null)
                {
                    // If modpack already exists, update its data
                    existingModpack.Version = data.Version; // You need to implement this method to get the version
                }
                else
                {
                    // If modpack doesn't exist, add it to the list
                    existingData.Add(new ModpackData { Name = name, Version = data.Version, API = data.API, URL = data.URL });
                }

                // Write the updated JSON content back to the file
                string updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);
                File.WriteAllText(Paths.localMetadata, updatedJson);

                // Launch the game
                var gl = new GameLauncher(ramSlider.Value * 1024, usernameTextBox.Text, versionBox.SelectedItem.ToString(), mainForm);
            }
            catch (IOException) { }
        }

    }
}
