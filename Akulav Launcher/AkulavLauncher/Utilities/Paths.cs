using System;

namespace PasswordManager.Utilities
{
    class Paths
    {
        public static readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string localMetadata = appdata + @"\.minecraft\neweraversion";
        public static readonly string ramData = appdata + @"\.minecraft\ramsetting";
        public static readonly string localUser = appdata + @"\.minecraft\username";
        public static readonly string url = "https://raw.githubusercontent.com/Akulav/MinecraftModpackUpdater/main/NewEraUltimateMetadata";
        public static readonly string[] deletion_list = {
            //appdata + @"\.minecraft\libraries",
            //appdata + @"\.minecraft\webcache2",
            appdata + @"\.minecraft\mods",
            //appdata + @"\.minecraft\versions",
            appdata + @"\.minecraft\config",
            appdata + @"\.minecraft\Flan"
        };
        public static readonly string temp = "C:\\NewEraCache\\";
    }
}
