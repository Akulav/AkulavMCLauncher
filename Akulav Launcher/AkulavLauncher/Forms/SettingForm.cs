using AkulavLauncher.Utilities;
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
        UIManager ui = new UIManager();
        public SettingForm()
        {
            InitializeComponent();
            var savedLink = DirectoryLib.GetTextFromJson();
            if (savedLink != null)
            {
                textBoxNewLink.Text = savedLink;
                ui.DisableControl(textBoxNewLink);
            }
        }

        private void SaveTextToJson(string text)
        {
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(text));
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            var textToAdd = textBoxNewLink.Text.Trim();
            if (!string.IsNullOrWhiteSpace(textToAdd))
            {
                SaveTextToJson(textToAdd);
                ui.DisableControl(textBoxNewLink);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DirectoryLib.DeleteFileIfExists(jsonFilePath);
            ui.EnableControl(textBoxNewLink);
            ui.ClearControl(textBoxNewLink);
        }

        private void resetSettingsButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DirectoryLib.DeleteFileIfExists(Paths.settings);
        }

        private void McFolderButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            Process.Start("explorer.exe", Paths.mc);
        }

        private void removeModpacksButton_Click(object sender, EventArgs e)
        {
            Utility.PlaySound();
            DirectoryLib.DeleteFileIfExists(Paths.localMetadata);
        }
    }
}
