using AkulavLauncher.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace AkulavLauncher
{
    public static class Utility
    {
        public static List<ModpackData> modpacks = new List<ModpackData>();
        public static void GetModpacks()
        {
            try
            {
                using (WebClient client = new WebClient())
                {

                    string json = client.DownloadString(Paths.modpackList);
                    modpacks = JsonConvert.DeserializeObject<List<ModpackData>>(json);
                }
            }

            catch
            {
                modpacks = null;
            }
        }

    }
}