using System;

namespace PasswordManager.Utilities
{
    class Paths
    {
        public static readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string localMetadata = appdata + @"\.minecraft\modpacks_local.json";
        public static readonly string url = "https://raw.githubusercontent.com/Akulav/MinecraftModpackUpdater/main/NewEraUltimateMetadata";
        public static readonly string temp = "C:\\NewEraCache\\";
        public static readonly string mc = appdata + @"\.minecraft";
        public static readonly string skin = appdata + @"\.minecraft\CustomSkinLoader\LocalSkin\skins\";
        public static readonly string versionUrl = "https://raw.githubusercontent.com/Akulav/AkulavMCLauncher/main/version.txt";
        public static readonly string modpackList = "https://raw.githubusercontent.com/Akulav/AkulavMCLauncher/main/modpacks.json";
        public static readonly string[] deletion_list = {
            appdata + @"\.minecraft\mods",
            appdata + @"\.minecraft\config"
        };
        public static readonly string settings = appdata + @"\.minecraft\newerasettings.json";
    }
}
