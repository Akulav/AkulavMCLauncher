using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AkulavLauncher
{
    class DirectoryLib
    {
        public static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(sourcePath)) return;

            var directories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
            var files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(directories, (dirPath) =>
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            });

            Parallel.ForEach(files, (filePath) =>
            {
                string targetFilePath = filePath.Replace(sourcePath, targetPath);
                File.Copy(filePath, targetFilePath, true);
            });
        }
        public static void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static string GetTextFromJson()
        {
            return File.Exists(Paths.links)
                ? JsonConvert.DeserializeObject<string>(File.ReadAllText(Paths.links))
                : null;
        }

        public static void DeleteFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
