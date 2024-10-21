using System;
using System.IO;

namespace AkulavLauncher
{
    class Paths
    {
        // Base Paths
        public static readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string mc = Path.Combine(appdata, ".minecraft");
        public static readonly string temp = @"C:\AkulavLauncherCache";

        // Files and URLs
        public static readonly string localMetadata = Path.Combine(mc, "modpacks_local.json");
        public static readonly string settings = Path.Combine(mc, "akulav_launcher_settings.json");
        public static readonly string links = Path.Combine(mc, "links.json");
        public static readonly string versionUrl = "https://raw.githubusercontent.com/Akulav/AkulavMCLauncher/main/version.txt";
        public static readonly string modpackList = "https://raw.githubusercontent.com/Akulav/AkulavMCLauncher/main/modpacks.json";

        // Cache and Update Paths
        public static readonly string update = Path.Combine(@"C:\AkulavLauncher", "update.exe");
        public static readonly string updateFlag = Path.Combine(@"C:\AkulavLauncher", "update.txt");
        public static readonly string downloaded = Path.Combine(temp, "downloaded.zip");
        public static readonly string extracted = Path.Combine(temp, "extracted");
        public static readonly string cache = temp;  // Retained `cache` as it directly refers to `temp`

        // Directories to be Deleted
        public static readonly string[] deletion_list = {
            "mods",
            "config"
        };
    }
}
