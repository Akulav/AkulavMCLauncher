using System;

namespace PasswordManager.Utilities
{
    class Paths
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string localMetadata = appdata + @"\.minecraft\neweraversion";
        public static string localUser = appdata + @"\.minecraft\username";
        public static string url = "https://raw.githubusercontent.com/Akulav/MinecraftModpackUpdater/main/NewEraUltimateMetadata";
        public static string[] deletion_list = {
            //appdata + @"\.minecraft\libraries",
            //appdata + @"\.minecraft\webcache2",
            appdata + @"\.minecraft\mods",
            //appdata + @"\.minecraft\versions",
            appdata + @"\.minecraft\config",
            appdata + @"\.minecraft\Flan"
        };
        public static string temp = "C:\\NewEraCache\\";
    }
}
