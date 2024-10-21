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
            var savedLink = DirectoryLib.GetTextFromJson();
            if (savedLink != null)
            {
                textBoxNewLink.Text = savedLink;
                textBoxNewLink.Enabled = false;
            }
        }

        private void SaveTextToJson(string text)
        {
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(text));
        }

        private void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            var textToAdd = textBoxNewLink.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textToAdd))
            {
                SaveTextToJson(textToAdd);
                textBoxNewLink.Enabled = false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DeleteFileIfExists(jsonFilePath);
            textBoxNewLink.Enabled = true;
            textBoxNewLink.Clear();
        }

        private void resetSettingsButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DeleteFileIfExists(Paths.settings);
        }

        private void McFolderButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            Process.Start("explorer.exe", Paths.mc);
        }

        private void removeModpacksButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DeleteFileIfExists(Paths.localMetadata);
        }
    }
}
