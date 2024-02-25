using AkulavLauncher.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AkulavLauncher
{
    public static class Utility
    {
        public static List<ModpackData> modpacks = new List<ModpackData>();
        public static void GetModpacks()
        {
            LoadAll();
            string source = GetTextFromJson();
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

        public static string GetTextFromJson()
        {
            if (File.Exists(Paths.links))
            {
                string jsonData = File.ReadAllText(Paths.links);
                return JsonConvert.DeserializeObject<string>(jsonData);
            }
            return null;
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