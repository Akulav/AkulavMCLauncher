using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using System;
using System.Windows.Forms;

namespace AkulavLauncher
{
    internal class GameLauncher
    {
        private readonly int ram;
        private readonly string username;
        private string gameVersion;
        private readonly Form mainForm;
        private bool downloadStarted;

        public GameLauncher(int ram, string username, string gameVersion, Form mainForm)
        {
            this.ram = ram;
            this.username = username;
            this.gameVersion = gameVersion;
            this.mainForm = mainForm;
            LaunchGameAsync();
        }

        private string checkIfDownloaded()
        {
            var consoleLabel = Application.OpenForms["MainForm"].Controls.Find("consoleLabel", true)[0] as Label;
            DataDownloader dataDownloader = new DataDownloader(mainForm, username, ram, gameVersion, consoleLabel);

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

            var consoleLabel = Application.OpenForms["MainForm"].Controls.Find("consoleLabel", true)[0] as Label;

            var minecraftPath = new MinecraftPath(Paths.mc + "\\" + gameVersion);
            var launcher = new MinecraftLauncher(minecraftPath);



            launcher.ByteProgressChanged += (sender, args) =>
            {
                if (args.TotalBytes > 0)
                {
                    int progressPercentage = (int)((args.ProgressedBytes * 100) / args.TotalBytes);
                    consoleLabel.Text = $"Game is launching: {progressPercentage}%"; // Show only the percentage
                }
            };
            
            var session = MSession.CreateOfflineSession(username);

            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = session
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
