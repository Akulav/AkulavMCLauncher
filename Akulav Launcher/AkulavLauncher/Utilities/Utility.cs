using AkulavLauncher.Data;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                using (var client = new WebClient())
                {
                    modpacks = JsonConvert.DeserializeObject<List<ModpackData>>(client.DownloadString(source));
                }
            }
            catch
            {
                modpacks = null;
            }
        }

        public static int GetRAM() => (int)(new ComputerInfo().TotalPhysicalMemory / (1024 * 1024 * 1024));

        public static int ParseRam(string ram, int maxRam) => int.TryParse(ram, out int parsedRam) ? parsedRam : maxRam / 2;

        public static void PlaySound()
        {
            using (var player = new SoundPlayer(Properties.Resources.lever))
            {
                player.Play();
            }
        }

        public static void SetUserData(string username, string ram, string version) => new UserData().SetUserData(username, ram, version); // Compacting method call

        private static void LoadAll()
        {
            if (!File.Exists(Paths.links))
            {
                Directory.CreateDirectory(Paths.mc);
                Directory.CreateDirectory(Paths.akulav_launcher_config);
                File.WriteAllText(Paths.links, string.Empty);
            }
        }

        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 16 || !IsAlphanumericWithUnderscore(username))
            {
                ShowWarning("Invalid username. It should be 3-16 alphanumeric characters or underscores only.");
                return false;
            }
            return true;
        }

        private static bool IsAlphanumericWithUnderscore(string username) => username.All(c => char.IsLetterOrDigit(c) || c == '_'); // Optimizing loop into LINQ

        private static void ShowWarning(string message) => MessageBox.Show(message, "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
