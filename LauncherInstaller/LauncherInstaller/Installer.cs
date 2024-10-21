using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace AkulavLauncherInstaller
{
    public partial class statusdLbl : Form
    {
        private static readonly string basePath = "C:\\AkulavLauncher\\";
        private static readonly string fileLocation = Path.Combine(basePath, "file.zip");
        private static readonly string updateFlag = Path.Combine(basePath, "update.txt");

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public statusdLbl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            CenterToScreen();

            if (File.Exists(updateFlag))
            {
                installBtn.Text = "Update Launcher";
            }
        }

        private void InstallBtn_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(basePath);
            File.WriteAllBytes(fileLocation, Properties.Resources.file);
            ExtractFilesFromZip(fileLocation, basePath);
            CreateShortcut();
            StartLauncher();
            Application.Exit();
        }

        private void ExtractFilesFromZip(string zipPath, string extractPath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (!string.IsNullOrEmpty(entry.Name))
                    {
                        string destinationPath = Path.Combine(extractPath, entry.FullName);
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        entry.ExtractToFile(destinationPath, true);
                    }
                }
            }
        }

        private void CreateShortcut()
        {
            IShellLink link = (IShellLink)new ShellLink();
            link.SetDescription("A MC Launcher");
            link.SetPath(Path.Combine(basePath, "AkulavLauncher.exe"));
            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, "AkulavLauncher.lnk"), false);
        }

        private void StartLauncher()
        {
            Process.Start(Path.Combine(basePath, "AkulavLauncher.exe"));
        }

        private void UninstallBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                ForceDeleteDirectory(basePath);
                Directory.Delete(basePath, true);
                File.Delete(Path.Combine(desktopPath, "AkulavLauncher.lnk"));
                statusLabel.Text = "Uninstallation done.";
            }
            catch
            {
                statusLabel.Text = "Some files were not found.";
            }
        }

        public static void ForceDeleteDirectory(string path)
        {
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };
            foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                info.Attributes = FileAttributes.Normal;
            }
        }

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink { }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        }

        private void statusdLbl_Load(object sender, EventArgs e)
        {
            if (Directory.GetCurrentDirectory() == basePath)
            {
                installBtn.PerformClick();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}
