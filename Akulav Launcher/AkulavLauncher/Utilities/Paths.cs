using System;

namespace AkulavLauncher
{
    class Paths
    {
        public static readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string localMetadata = appdata + @"\.minecraft\modpacks_local.json";
        public static readonly string temp = "C:\\AkulavLauncherCache\\";
        public static readonly string mc = appdata + @"\.minecraft";
        public static readonly string versionUrl = "https://raw.githubusercontent.com/Akulav/AkulavMCLauncher/main/version.txt";
        public static readonly string modpackList = "https://raw.githubusercontent.com/Akulav/AkulavMCLauncher/main/modpacks.json";
        public static readonly string[] deletion_list = {
            @"mods",
            @"config"
        };
        public static readonly string settings = appdata + @"\.minecraft\akulav_launcher_settings.json";
    }
}
