using AkulavLauncher.Data;
using AkulavLauncher.Utilities;
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
        private ProgressBar downloadBar;

        public DataDownloader(Form mainForm, string username, int ram, string modpackName, ProgressBar downloadBar)
        {
            this.ram = ram;
            this.username = username;
            modpackData = Utility.modpacks;
            this.modpackName = modpackName;
            this.mainForm = mainForm;
            this.downloadBar = downloadBar;
        }

        public bool CheckLocal()
        {

            if (File.Exists(Paths.localMetadata))
            {

                var local = JsonConvert.DeserializeObject<List<ModpackData>>(File.ReadAllText(Paths.localMetadata));

                var localIndex = GetListIndex(local, modpackName);

                var modpackIndex = GetListIndex(modpackData, modpackName);

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

        public void StartDownload()
        {
            foreach (var modpack in modpackData)
            {
                if (modpackName == modpack.Name)
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
            UIManager ui = new UIManager();
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                ui.SetProgressBarValue(downloadBar, percentage);
            });
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                DirectoryLib.DeleteFolder(Paths.extracted);
                var name = modpackData.Find(modpack => modpack.Name == modpackName)?.Name;
                ExtractInstall();
                DirectoryLib.DeleteFolder(Paths.cache);
            });
        }

        private void ExtractInstall()
        {
            try
            {
                var data = modpackData.Find(modpack => modpack.Name == modpackName);
                foreach (var folder in Paths.deletion_list)
                {
                    DirectoryLib.DeleteFolder(Path.Combine(Paths.mc, modpackName, folder));
                }

                ZipFile.ExtractToDirectory(Paths.downloaded, Paths.extracted);
                DirectoryLib.CopyFilesRecursively(Paths.extracted, Path.Combine(Paths.mc, modpackName));

                // Read existing JSON content
                List<ModpackData> existingData = new List<ModpackData>();
                if (File.Exists(Paths.localMetadata))
                {
                    string existingJson = File.ReadAllText(Paths.localMetadata);
                    existingData = JsonConvert.DeserializeObject<List<ModpackData>>(existingJson);
                }

                // Check if the modpack already exists in local metadata
                var existingModpack = existingData.Find(modpack => modpack.Name == modpackName);
                if (existingModpack != null)
                {
                    // If modpack already exists, update its data
                    existingModpack.Version = data.Version; // You need to implement this method to get the version
                }
                else
                {
                    // If modpack doesn't exist, add it to the list
                    existingData.Add(new ModpackData { Name = modpackName, Version = data.Version, API = data.API, URL = data.URL });
                }

                // Write the updated JSON content back to the file
                string updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);
                File.WriteAllText(Paths.localMetadata, updatedJson);

                // Launch the game
                var gl = new GameLauncher(ram, username, modpackName, mainForm, downloadBar);
            }
            catch (IOException) { }
        }

    }
}
