using AkulavLauncher.Data;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Net;

namespace AkulavLauncher
{
    public static class Utility
    {
        public static List<ModpackData> modpacks = new List<ModpackData>();
        public static void GetModpacks()
        {
            LoadAll();
            string source = DirectoryLib.GetTextFromJson();
            string json;
            try
            {

                using (WebClient client = new WebClient())
                {
                    if (source != null)
                    {
                        json = client.DownloadString(source);
                    }
                    else
                    {
                        json = client.DownloadString(Paths.modpackList);
                    }

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
            ComputerInfo computerInfo = new ComputerInfo();
            // Convert bytes to kilobytes
            return Convert.ToInt32(computerInfo.TotalPhysicalMemory / 1024 / 1024 / 1024);
        }

        public static int ParseRam(string ram, int maxRam)
        {
            if (int.TryParse(ram, out int parsedRam))
            {
                return parsedRam;
            }
            else
            {
                // Default value if parsing fails
                return maxRam / 2;
            }
        }

        public static void PlaySound()
        {
            try
            {
                using (var player = new SoundPlayer(Properties.Resources.lever))
                {
                    player.Play();
                }
            }
            catch
            {

            }
        }

        public static void SetUserData(string username, string ram, string version)
        {
            UserData ud = new UserData();
            ud.SetUserData(username, ram, version);
        }

        private static void LoadAll()
        {
            string jsonFilePath = Paths.links;
            if (!File.Exists(jsonFilePath))
            {
                if (!Directory.Exists(Paths.mc))
                {
                    Directory.CreateDirectory(Paths.mc);
                }
                File.WriteAllText(jsonFilePath, "");
            }
        }

    }
}