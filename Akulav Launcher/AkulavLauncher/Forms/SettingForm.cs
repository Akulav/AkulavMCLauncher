using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AkulavLauncher.Forms
{

    public partial class SettingForm : Form
    {
        private readonly string jsonFilePath = Paths.links;

        public SettingForm()
        {
            InitializeComponent();
            if (DirectoryLib.GetTextFromJson() != null)
            {
                textBoxNewLink.Text = DirectoryLib.GetTextFromJson();
                textBoxNewLink.Enabled = false;
            }
        }


        private void AddTextToJson(string text)
        {
            var jsonData = JsonConvert.SerializeObject(text);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        private void RemoveTextFromJson()
        {
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }
        }



        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            if (!string.IsNullOrWhiteSpace(textBoxNewLink.Text))
            {
                string textToAdd = textBoxNewLink.Text.Trim();
                AddTextToJson(textToAdd);
                textBoxNewLink.Enabled = false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            RemoveTextFromJson();
            textBoxNewLink.Enabled = true;
            textBoxNewLink.Clear();
        }

        private void resetSettingsButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            if (File.Exists(Paths.settings))
            {
                File.Delete(Paths.settings);
            }
        }

        private void McFolderButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            Process.Start("explorer.exe", Paths.mc);
        }

        private void removeModpacksButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            if (File.Exists(Paths.localMetadata))
            {
                File.Delete(Paths.localMetadata);
            }
        }
    }

}
