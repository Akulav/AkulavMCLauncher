using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AkulavLauncher.Utilities
{
    public class UIManager
    {
        public UIManager() { }
        public void disableButton(Button button)
        {
            button.BeginInvoke((MethodInvoker)(() => button.Enabled = false));
        }

        public void setRamLabel(Label ramLabel, TrackBar ramSlider)
        {
            setControlText(ramLabel, ramSlider.Value.ToString() + " GB of RAM");
        }

        private void setControlText(Control control, string text)
        {
            control.BeginInvoke((MethodInvoker)(() => control.Text = text));
        }

        private void setRamSliderMinimum(TrackBar slider, int value)
        {
            slider.BeginInvoke((MethodInvoker)(() => slider.Minimum = value));
        }

        private void setRamSliderMaximum(TrackBar slider, int value)
        {
            slider.BeginInvoke((MethodInvoker)(() => slider.Maximum = value));
        }

        private void setRamSliderValue(TrackBar slider, int value)
        {
            slider.BeginInvoke((MethodInvoker)(() => slider.Value = value));
        }

        private void UpdateUserDataUI(UserData userData, TextBox Username, TrackBar ramSlider, Label ramLabel, int ram, ComboBox comboBox)
        {
            setControlText(Username, userData.UserName);
            setRamSliderMinimum(ramSlider, 1);
            setRamSliderMaximum(ramSlider, ram + 1);
            ramSlider.Value = Utility.ParseRam(userData.Ram, ram);
            setControlText(ramLabel, userData.Ram + " GB of RAM");

            // Call SelectItemByText on the UI thread
            comboBox.BeginInvoke((MethodInvoker)(() => SelectItemByText(comboBox, userData.SelectedModpack)));
        }

        private void SelectItemByText(ComboBox comboBox, string targetText)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == targetText)
                {
                    SetComboBoxIndex(comboBox, i);
                    return;
                }
            }
        }

        private void SetComboBoxIndex(ComboBox versionbox, int index)
        {
            versionbox.BeginInvoke((MethodInvoker)(() => versionbox.SelectedIndex = index));
        }

        public void SetProgressBarValue(ProgressBar bar, double value)
        {
            bar.BeginInvoke((MethodInvoker)(() => bar.Value = int.Parse(Math.Truncate(value).ToString())));

        }


        private void SetDefaultValues(TrackBar ramSlider, Label ramLabel, int ram, TextBox username, ComboBox versionBox)
        {
            setRamSliderMinimum(ramSlider, 1);
            setRamSliderMaximum(ramSlider, ram + 1);
            setRamSliderValue(ramSlider, (ram + 1) / 2);
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
            setControlText(ramSlider, ramSlider.Value.ToString() + " GB of RAM");
            setControlText(username, "Steve");
            SetComboBoxIndex(versionBox, 0);
        }

        public void GetUserData(TextBox Username, TrackBar ramSlider, Label ramLabel, int ram, ComboBox comboBox)
        {
            try
            {
                UserData ud = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Paths.settings));
                UpdateUserDataUI(ud, Username, ramSlider, ramLabel, ram, comboBox);
            }
            catch
            {
                SetDefaultValues(ramSlider, ramLabel, ram, Username, comboBox);
            }
        }

        public void SetDataOnModpackSelect(ComboBox versionBox, Label nameLabel, Label packVersion)
        {
            try
            {
                var selectedModpack = Utility.modpacks.Find(modpack => modpack.Name == versionBox.Text);
                if (selectedModpack != null)
                {
                    setControlText(nameLabel, "Modpack: " + selectedModpack.Name);
                    setControlText(packVersion, "Pack Version: " + selectedModpack.Version);
                }
                else
                {
                    setControlText(nameLabel, "Server could not be reached.");
                    setControlText(packVersion, "");
                }

            }
            catch
            {
                setControlText(nameLabel, "Server could not be reached.");
                setControlText(packVersion, "");
            }
        }

        private void AddComboBoxItems(ComboBox versionbox, string value)
        {
            versionbox.BeginInvoke((MethodInvoker)(() => versionbox.Items.Add(value)));
        }

        public void GetVersions(ComboBox versionBox, Label nameLabel, Label packVersion)
        {
            try
            {
                foreach (var modpack in Utility.modpacks)
                {
                    AddComboBoxItems(versionBox, modpack.Name);
                }

                string userdata = File.ReadAllText(Paths.settings);
                UserData ud = JsonConvert.DeserializeObject<UserData>(userdata);

                bool found = false;

                for (int i = 0; i < versionBox.Items.Count; i++)
                {
                    MessageBox.Show(versionBox.Items[i].ToString());

                    if (versionBox.Items[i].ToString() == ud.SelectedModpack)
                    {
                        SetComboBoxIndex(versionBox, i);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    SetComboBoxIndex(versionBox, 0);
                }
                UIManager ui = new UIManager();
                ui.SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
            }
            catch { }
        }

        public void CheckUpdate(string version, Label nameLabel, Label packVersion)
        {
            try
            {
                var metadata = new List<string>();
                using (WebClient client = new WebClient())
                {
                    string updateData = client.DownloadString(Paths.versionUrl);
                    metadata.AddRange(Regex.Split(updateData, "\r\n|\r|\n"));
                }

                if (version != metadata[0])
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
                setControlText(nameLabel, "Server could not be reached.");
                setControlText(packVersion, "");
            }
        }

        private void Client_DownloadProgressChangedVersion(object sender, DownloadProgressChangedEventArgs e)
        {
            bool flag = true;
            if (e.ProgressPercentage == 100 && flag)
            {
                flag = false;
                var p = new Process();
                p.StartInfo.FileName = Paths.update;
                p.Start();
                Application.Exit();
            }
        }

    }

}