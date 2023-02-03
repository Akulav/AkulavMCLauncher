using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using AkulavLauncher;

namespace PasswordManager.Utilities
{
    class Functionality
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static void StartInstall()
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < Paths.deletion_list.Length; i++)
                {
                    directoryLib.DeleteFolder(Paths.deletion_list[i]);
                }

            });
            thread.Start();
        }

        public static void ExtractInstall(string version)
        {
            try
            {
                ZipFile.ExtractToDirectory(@"C:\NewEraCache\downloaded.zip", @"C:\NewEraCache\extracted\");
                directoryLib.CopyFilesRecursively(@"C:\NewEraCache\extracted\", appdata + @"\.minecraft\");
                File.WriteAllText(Paths.localMetadata, version);
            }
            catch (IOException)
            {

            }
        }
    }
}
