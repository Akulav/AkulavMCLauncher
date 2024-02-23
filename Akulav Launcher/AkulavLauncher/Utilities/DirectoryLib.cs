using System.IO;
using System.Threading.Tasks;

namespace AkulavLauncher
{
    class DirectoryLib
    {
        public static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            Parallel.ForEach(Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories), (dirPath) =>
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            });

            Parallel.ForEach(Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories), (newPath) =>
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            });

        }


        public static void DeleteFolder(string Path)
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
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
