using AkulavLauncher.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        // Public methods first

        public void DisableControl(Control control) => control.BeginInvoke((MethodInvoker)(() => control.Enabled = false));

        public void EnableControl(Control control) => control.BeginInvoke((MethodInvoker)(() => control.Enabled = true));

        public void ClearControl(TextBox text) => text.BeginInvoke((MethodInvoker)(() => text.Clear()));

        public void SetRamLabel(Label ramLabel, TrackBar ramSlider) => SetControlText(ramLabel, $"{ramSlider.Value} GB of RAM");

        public void ShowSettingsForm(Panel settingsPanel)
        {
            var sf = new SettingForm { TopLevel = false, TopMost = true };
            settingsPanel.Controls.Add(sf);
            sf.Show();
        }

        public void HideSettingsForm(Panel settingsPanel) => settingsPanel.BeginInvoke((MethodInvoker)(() => settingsPanel.Controls.Clear()));

        public void GetUserData(TextBox username, TrackBar ramSlider, Label ramLabel, int ram, ComboBox comboBox)
        {
            try
            {
                var userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Paths.settings));
                UpdateUserDataUI(userData, username, ramSlider, ramLabel, ram, comboBox);
            }
            catch
            {
                SetDefaultValues(ramSlider, ramLabel, ram, username, comboBox);
            }
        }

        public void GetVersions(ComboBox versionBox, Label nameLabel, Label packVersion)
        {
            try
            {
                versionBox.Items.Clear();
                foreach (var modpack in Utility.modpacks)
                    AddComboBoxItems(versionBox, modpack.Name);

                var userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Paths.settings));
                SelectModpackInComboBox(versionBox, userData.SelectedModpack);
                SetDataOnModpackSelect(versionBox, nameLabel, packVersion);
            }
            catch { }
        }

        public void CheckUpdate(string version, Label nameLabel, Label packVersion)
        {
            try
            {
                var metadata = DownloadUpdateMetadata();
                if (version != metadata[0])
                {
                    var client = new WebClient();
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    client.DownloadFileAsync(new Uri(metadata[1]), Paths.update);
                }
                else
                {
                    File.Delete(Paths.update);
                }
            }
            catch
            {
                SetControlText(nameLabel, "Server could not be reached.");
                SetControlText(packVersion, "");
            }
        }

        public void SetDataOnModpackSelect(ComboBox versionBox, Label nameLabel, Label packVersion)
        {
            try
            {
                var selectedModpack = Utility.modpacks.Find(modpack => modpack.Name == versionBox.Text);


                if (selectedModpack != null)
                {
                    SetControlText(nameLabel, $"Modpack: {selectedModpack.Name}");
                    SetControlText(packVersion, $"Pack Version: {selectedModpack.Version}");
                }
                else
                {
                    SetControlText(nameLabel, "Server could not be reached.");
                    SetControlText(packVersion, "");
                }
            }

            catch
            {

            }
        }

        // Private methods below

        private void UpdateUserDataUI(UserData userData, TextBox username, TrackBar ramSlider, Label ramLabel, int ram, ComboBox comboBox)
        {
            SetControlText(username, userData.UserName);
            SetRamSliderProperties(ramSlider, 1, ram + 1, Utility.ParseRam(userData.Ram, ram));
            SetControlText(ramLabel, $"{userData.Ram} GB of RAM");
            comboBox.BeginInvoke((MethodInvoker)(() => SelectItemByText(comboBox, userData.SelectedModpack)));
        }

        private void SetDefaultValues(TrackBar ramSlider, Label ramLabel, int ram, TextBox username, ComboBox versionBox)
        {
            SetRamSliderProperties(ramSlider, 1, ram + 1, (ram + 1) / 2);
            SetControlText(ramLabel, $"{ramSlider.Value} GB of RAM");
            SetControlText(username, "Steve");
            SetComboBoxIndex(versionBox, 0);
        }

        private void SetRamSliderProperties(TrackBar slider, int min, int max, int value)
        {
            slider.BeginInvoke((MethodInvoker)(() =>
            {
                slider.Minimum = min;
                slider.Maximum = max;
                slider.Value = value;
            }));
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

        private void SetComboBoxIndex(ComboBox comboBox, int index) => comboBox.BeginInvoke((MethodInvoker)(() => comboBox.SelectedIndex = index));

        private void SelectModpackInComboBox(ComboBox versionBox, string selectedModpack)
        {
            bool found = false;
            for (int i = 0; i < versionBox.Items.Count; i++)
            {
                if (versionBox.Items[i].ToString() == selectedModpack)
                {
                    SetComboBoxIndex(versionBox, i);
                    found = true;
                    break;
                }
            }
            if (!found)
                SetComboBoxIndex(versionBox, 0);
        }

        private void AddComboBoxItems(ComboBox versionBox, string value) => versionBox.BeginInvoke((MethodInvoker)(() => versionBox.Items.Add(value)));

        private void SetControlText(Control control, string text) => control.BeginInvoke((MethodInvoker)(() => control.Text = text));

        private List<string> DownloadUpdateMetadata()
        {
            using (var client = new WebClient())
            {
                var updateData = client.DownloadString(Paths.versionUrl);
                return new List<string>(Regex.Split(updateData, "\r\n|\r|\n"));
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            File.WriteAllText(Paths.updateFlag, "INCOMING_UPDATE");
            var process = new Process { StartInfo = { FileName = Paths.update } };
            process.Start();
            Application.Exit();
        }
    }
}
