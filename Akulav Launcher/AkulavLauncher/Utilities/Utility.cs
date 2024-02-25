using AkulavLauncher.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace AkulavLauncher
{
    public static class Utility
    {
        public static List<ModpackData> modpacks = new List<ModpackData>();
        public static List<Link> links = new List<Link>();
        public static void GetModpacks()
        {
            LoadAll();
            LoadLinks();
            string source = GetEnabledLink();
            try
            {

                if (source == "")
                {
                    using (WebClient client = new WebClient())
                    {
                        string json = client.DownloadString(Paths.modpackList);
                        modpacks = JsonConvert.DeserializeObject<List<ModpackData>>(json);
                    }
                }

                else
                {
                    using (WebClient client = new WebClient())
                    {
                        string json = client.DownloadString(source);
                        modpacks = JsonConvert.DeserializeObject<List<ModpackData>>(json);
                    }
                }


            }
            catch
            {
                modpacks = null;
            }
        }

        private static void LoadLinks()
        {
            string jsonFilePath = Paths.links;
            string json = File.ReadAllText(jsonFilePath);
            links = JsonConvert.DeserializeObject<List<Link>>(json) ?? new List<Link>();
        }

        public static string GetEnabledLink()
        {
            var enabledLink = links.FirstOrDefault(link => link.Enabled);
            return enabledLink?.Url ?? "";
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