using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace PasswordManager.Utilities
{
    internal class DataDownloader
    {
        public string game_version;
        public string mod_version;
        public string mod_name;
        public string mod_url;
        public DataDownloader() {
            getData();
        }

        private void getData()
        {
            List<string> metadata = new List<string>();
            using (WebClient client = new WebClient())
            {
                string[] result;
                string update_data = client.DownloadString(Paths.url);
                result = Regex.Split(update_data, "\r\n|\r|\n");
                foreach (string s in result)
                {
                    metadata.Add(s);
                }
            }
            mod_name = metadata[0];
            mod_version = metadata[1];
            game_version = metadata[2];
            mod_url = metadata[3];
        }
    }
}
