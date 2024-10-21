using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace AkulavLauncherInstaller
{

    public partial class statusdLbl : Form
    {
        readonly static string location = "C:\\AkulavLauncher\\";
        readonly string fileLocation = "C:\\AkulavLauncher\\file.zip";
        readonly string updateFlag = "C:\\AkulavLauncher\\update.txt";

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public statusdLbl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            string dirpath = Directory.GetCurrentDirectory();
            this.CenterToScreen();
            if (File.Exists(updateFlag))
            {
                installBtn.Text = "Update Launcher";
            }
        }

        private void InstallBtn_Click(object sender, EventArgs e)
        {

            Directory.CreateDirectory(location);

            File.WriteAllBytes(fileLocation, Properties.Resources.file);

            using (ZipArchive source = ZipFile.Open(fileLocation, ZipArchiveMode.Read, null))
            {
                foreach (ZipArchiveEntry entry in source.Entries)
                {
                    string fullPath = Path.GetFullPath(Path.Combine(location, entry.FullName));

                    if (Path.GetFileName(fullPath).Length != 0)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                        // The boolean parameter determines whether an existing file that has the same name as the destination file should be overwritten
                        entry.ExtractToFile(fullPath, true);
                        //File.Delete(fileLocation);
                    }
                }
            }

            IShellLink link = (IShellLink)new ShellLink();

            // setup shortcut information
            link.SetDescription("A MC Launcher");
            link.SetPath(@"C:\\AkulavLauncher\\AkulavLauncher.exe");

            // save it
            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, "AkulavLauncher.lnk"), false);

            var p = new Process();
            p.StartInfo.FileName = @"C:\AkulavLauncher\AkulavLauncher.exe";
            p.Start();

            Application.Exit();
        }

        private void UninstallBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                ForceDeleteDirectory(location);
                Directory.Delete(location, true);
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
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        private void statusdLbl_Load(object sender, EventArgs e)
        {
            string dirpath = Directory.GetCurrentDirectory();
            if (dirpath == "C:\\AkulavLauncher")
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