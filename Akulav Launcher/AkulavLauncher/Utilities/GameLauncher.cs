using CmlLib.Core;
using CmlLib.Core.Auth;
using System.Windows.Forms;

namespace AkulavLauncher
{
    internal class GameLauncher
    {
        private readonly int ram;
        private readonly string username;
        private string gameVersion;
        private readonly Form mainForm;
        private readonly ProgressBar downloadProgressBar;
        private bool downloadStarted;

        public GameLauncher(int ram, string username, string gameVersion, Form mainForm, ProgressBar downloadProgressBar)
        {
            this.ram = ram;
            this.username = username;
            this.gameVersion = gameVersion;
            this.mainForm = mainForm;
            this.downloadProgressBar = downloadProgressBar;
            LaunchGameAsync();
        }

        private string checkIfDownloaded()
        {
            DataDownloader dataDownloader = new DataDownloader(mainForm, username, ram, gameVersion, downloadProgressBar);

            var modpacks = Utility.modpacks;
            downloadStarted = false;

            foreach (var modpack in modpacks)
            {
                if (gameVersion == modpack.Name)
                {
                    if (dataDownloader.CheckLocal())
                    {
                        dataDownloader.StartDownload();
                        downloadStarted = true;
                        break; // Exit the loop once download starts
                    }

                    return modpack.API;
                }
            }

            return null;
        }

        private async void LaunchGameAsync()
        {
            var downloadBar = Application.OpenForms["MainForm"].Controls.Find("downloadBar", true)[0] as ProgressBar;
            var consoleLabel = Application.OpenForms["MainForm"].Controls.Find("consoleLabel", true)[0] as Label;

            var minecraftPath = new MinecraftPath(Paths.mc + "\\" + gameVersion);
            var launcher = new CMLauncher(minecraftPath);

            launcher.ProgressChanged += (sender, progressArgs) =>
            {
                downloadBar.Value = progressArgs.ProgressPercentage;
            };

            launcher.FileChanged += (fileArgs) =>
            {
                consoleLabel.Text = $"[{fileArgs.FileKind}] {fileArgs.FileName} - {fileArgs.ProgressedFileCount}/{fileArgs.TotalFileCount}";
            };

            var session = MSession.CreateOfflineSession(username);

            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = session,
                JVMArguments = null // Currently bugged, set to null
            };

            gameVersion = checkIfDownloaded();

            if (!downloadStarted)
            {
                var process = await launcher.CreateProcessAsync(gameVersion, launchOption);
                process.Start();

                mainForm.Visible = false;
                mainForm.ShowInTaskbar = false;

                while (!process.WaitForExit(1))
                {

                }

                Application.Restart();
            }
        }
    }
}
