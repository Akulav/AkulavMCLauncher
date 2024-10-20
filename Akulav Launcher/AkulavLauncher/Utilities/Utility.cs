using AkulavLauncher.Data;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Net;
using System.Windows.Forms;

namespace AkulavLauncher
{
    public static class Utility
    {
        public static List<ModpackData> modpacks = new List<ModpackData>();

        public static void GetModpacks()
        {
            LoadAll();
            string source = DirectoryLib.GetTextFromJson() ?? Paths.modpackList;

            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(source);
                    modpacks = JsonConvert.DeserializeObject<List<ModpackData>>(json);
                }
            }
            catch
            {
                modpacks = null;
            }
        }

        public static int GetRAM()
        {
            var computerInfo = new ComputerInfo();
            // Return RAM in GB
            return (int)(computerInfo.TotalPhysicalMemory / (1024 * 1024 * 1024));
        }

        public static int ParseRam(string ram, int maxRam)
        {
            return int.TryParse(ram, out int parsedRam) ? parsedRam : maxRam / 2;
        }

        public static void PlaySound()
        {
            using (var player = new SoundPlayer(Properties.Resources.lever))
            {
                player.Play();
            }
        }

        public static void SetUserData(string username, string ram, string version)
        {
            var userData = new UserData();
            userData.SetUserData(username, ram, version);
        }

        private static void LoadAll()
        {
            string jsonFilePath = Paths.links;

            if (!File.Exists(jsonFilePath))
            {
                Directory.CreateDirectory(Paths.mc);
                File.WriteAllText(jsonFilePath, string.Empty);
            }
        }

        public static bool IsValidUsername(TextBox usernameTextBox)
        {
            string username = usernameTextBox.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowWarning("Username can't contain empty spaces or be empty.");
                return false;
            }

            if (username.Length < 3 || username.Length > 16)
            {
                ShowWarning("Username must be between 3 and 16 characters.");
                return false;
            }

            if (!IsAlphanumericWithUnderscore(username))
            {
                ShowWarning("Username needs to contain only alphanumeric characters.");
                return false;
            }

            return true;
        }

        private static bool IsAlphanumericWithUnderscore(string username)
        {
            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c) && c != '_') return false;
            }
            return true;
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
