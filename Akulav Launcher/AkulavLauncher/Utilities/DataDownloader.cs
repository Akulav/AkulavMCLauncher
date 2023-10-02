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
using System.Threading;
using System.Windows.Forms;

namespace AkulavLauncher
{
    internal sealed class DataDownloader
    {
        readonly private Form mf;
        bool flag = true;

        //References to controls from mainform
        readonly TextBox Username = Application.OpenForms["MainForm"].Controls.Find("Username", true)[0] as TextBox;
        readonly TrackBar ramSlider = Application.OpenForms["MainForm"].Controls.Find("ramSlider", true)[0] as TrackBar;
        readonly ComboBox versionBox = Application.OpenForms["MainForm"].Controls.Find("versionBox", true)[0] as ComboBox;
        readonly Label nameLabel = Application.OpenForms["MainForm"].Controls.Find("nameLabel", true)[0] as Label;
        readonly Label packVersion = Application.OpenForms["MainForm"].Controls.Find("packVersion", true)[0] as Label;
        readonly ProgressBar downloadBar = Application.OpenForms["MainForm"].Controls.Find("downloadBar", true)[0] as ProgressBar;
        List<ModpackData> modpackData = Utility.modpacks;
        public DataDownloader(Form mainform)
        {
            mf = mainform;
        }

        private void CheckUpdate()
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
                        client.DownloadProgressChanged += Client_DownloadProgressChangedVersion;
                        client.DownloadFileAsync(new Uri(metadata[1]), Paths.update);
                    });
                    thread.Start();

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


        void Client_DownloadProgressChangedVersion(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100 && flag == true)
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
                foreach (ModpackData modpack in modpackData)
                {
                    if (versionBox.Text == modpack.Name)
                    {
                        nameLabel.Text = "Modpack: " + modpack.Name;
                        packVersion.Text = "Pack Version: " + modpack.Version;
                    }
                }
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

                List<ModpackData> local = JsonConvert.DeserializeObject<List<ModpackData>>(File.ReadAllText(Paths.localMetadata));



                if (local[GetListIndex(local, versionBox.Text)].Version != modpackData[GetListIndex(modpackData, versionBox.Text)].Version)
                {
                    return true;
                }

                return false;

            }

            return true;

        }

        private int GetListIndex(List<ModpackData> list, string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == name)
                    return i;
            }

            return 999;
        }

        public void GetVersions()
        {
            CheckUpdate();

            try
            {
                using (WebClient client = new WebClient())
                {


                    foreach (var s in modpackData)
                    {
                        versionBox.Items.Add(s.Name);
                    }
                    versionBox.SelectedIndex = 0;
                    GetData();

                }
            }

            catch
            {

            }
        }

        /// <summary>
        /// DOWNLOAD BLOCK FOR MODPACKS
        /// </summary>

        public void StartDownload()
        {
            string url;
            foreach (var s in modpackData)
            {
                if (versionBox.Text == s.Name)
                {
                    url = s.URL;
                    DirectoryLib.CreateFolder(Paths.temp);
                    Thread thread = new Thread(() =>
                    {
                        WebClient client = new WebClient();
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                        client.DownloadFileAsync(new Uri(url), Paths.downloaded);
                    });
                    thread.Start();
                }
            }
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            mf.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                downloadBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            mf.BeginInvoke((MethodInvoker)delegate
            {
                DirectoryLib.DeleteFolder(Paths.extracted);
                string name = "";
                foreach (ModpackData modpack in modpackData)
                {
                    if (modpack.Name == versionBox.Text)
                    {
                        name = modpack.Name;
                    }
                }
                ExtractInstall(name);
                DirectoryLib.DeleteFolder(Paths.cache);
            });
        }

        /// <summary>
        /// END OF DOWNLOAD BLOCK FOR MODPACKS
        /// </summary>

        ///
        ///START OF INSTALLATION BLOCK FOR MODPACKS
        ///

        public void ExtractInstall(string name)
        {
            try
            {

                for (int i = 0; i < Paths.deletion_list.Length; i++)
                {
                    DirectoryLib.DeleteFolder(Paths.mc + "\\" + name + "\\" + Paths.deletion_list[i]);
                }

                ZipFile.ExtractToDirectory(Paths.downloaded, Paths.extracted);
                DirectoryLib.CopyFilesRecursively(Paths.extracted, Paths.mc + "\\" + name + "\\");

                string json = JsonConvert.SerializeObject(modpackData, Formatting.Indented);
                File.WriteAllText(Paths.localMetadata, json);
                GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), mf);
            }
            catch (IOException)
            {

            }
        }

        ///
        ///END OF INSTALL BLOCK
        /// 

    }
}