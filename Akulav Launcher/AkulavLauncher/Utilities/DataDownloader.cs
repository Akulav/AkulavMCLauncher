using AkulavLauncher.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace AkulavLauncher
{
    internal sealed class DataDownloader
    {
        private readonly Form mainForm;
        private readonly List<ModpackData> modpackData;
        private readonly string username;
        private readonly string modpackName;
        private readonly int ram;
        private readonly ProgressBar downloadBar;
        private WebClient client;

        public DataDownloader(Form mainForm, string username, int ram, string modpackName, ProgressBar downloadBar)
        {
            this.mainForm = mainForm;
            this.username = username;
            this.ram = ram;
            this.modpackData = Utility.modpacks;
            this.modpackName = modpackName;
            this.downloadBar = downloadBar;
        }

        public bool CheckLocal()
        {
            if (!File.Exists(Paths.localMetadata))
                return true;

            var localData = JsonConvert.DeserializeObject<List<ModpackData>>(File.ReadAllText(Paths.localMetadata));
            var localModpack = localData?.Find(mp => mp.Name == modpackName);
            var modpack = modpackData.Find(mp => mp.Name == modpackName);

            return localModpack?.Version != modpack?.Version;
        }

        public void StartDownload()
        {
            var modpack = modpackData.Find(mp => mp.Name == modpackName);
            if (modpack == null) return;

            DirectoryLib.CreateFolder(Paths.temp);

            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(modpack.URL), Paths.downloaded);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                double percentage = (double)e.BytesReceived / e.TotalBytesToReceive * 100;
                downloadBar.Value = (int)percentage;
            });
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                DirectoryLib.DeleteFolder(Paths.extracted);
                ExtractInstall();
                DirectoryLib.DeleteFolder(Paths.cache);
            });
        }

        private void ExtractInstall()
        {
            try
            {
                var modpack = modpackData.Find(mp => mp.Name == modpackName);
                if (modpack == null) return;

                foreach (var folder in Paths.deletion_list)
                {
                    DirectoryLib.DeleteFolder(Path.Combine(Paths.mc, modpackName, folder));
                }

                ZipFile.ExtractToDirectory(Paths.downloaded, Paths.extracted);
                DirectoryLib.CopyFilesRecursively(Paths.extracted, Path.Combine(Paths.mc, modpackName));

                UpdateLocalMetadata(modpack);
                LaunchGame();
            }
            catch (IOException) { }
        }

        private void UpdateLocalMetadata(ModpackData modpack)
        {
            var existingData = new List<ModpackData>();
            if (File.Exists(Paths.localMetadata))
            {
                string existingJson = File.ReadAllText(Paths.localMetadata);
                existingData = JsonConvert.DeserializeObject<List<ModpackData>>(existingJson);
            }

            var existingModpack = existingData.Find(mp => mp.Name == modpackName);
            if (existingModpack != null)
            {
                existingModpack.Version = modpack.Version;
            }
            else
            {
                existingData.Add(new ModpackData { Name = modpackName, Version = modpack.Version, API = modpack.API, URL = modpack.URL });
            }

            File.WriteAllText(Paths.localMetadata, JsonConvert.SerializeObject(existingData, Formatting.Indented));
        }

        private void LaunchGame()
        {
            new GameLauncher(ram, username, modpackName, mainForm, downloadBar);
        }
    }
}
