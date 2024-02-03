using AkulavLauncher.Data;
using CmlLib.Core;
using CmlLib.Core.Auth;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AkulavLauncher
{
    internal class GameLauncher
    {
        private readonly int ram;
        private readonly string username;
        private readonly string game_version;
        private readonly Form mf;

        public GameLauncher(int ram, string username, string game_version, Form mf)
        {
            this.ram = ram;
            this.username = username;
            this.game_version = game_version;
            this.mf = mf;
            LaunchGameAsync();
        }

        private async void LaunchGameAsync()
        {
            IconButton launchButton = Application.OpenForms["MainForm"].Controls.Find("launchButton", true)[0] as IconButton;
            ProgressBar downloadBar = Application.OpenForms["MainForm"].Controls.Find("downloadBar", true)[0] as ProgressBar;
            Label consoleLabel = Application.OpenForms["MainForm"].Controls.Find("consoleLabel", true)[0] as Label;
            MinecraftPath path = new MinecraftPath(Paths.mc + "\\" + game_version);
            CMLauncher launcher = new CMLauncher(path);

            launchButton.Enabled = false;
            launcher.ProgressChanged += (s, p) =>
            {
                downloadBar.Value = p.ProgressPercentage;
            };

            launcher.FileChanged += (e) =>
            {
                consoleLabel.Text = "[" + e.FileKind.ToString() + "] " + e.FileName + " - " + e.ProgressedFileCount + "//" + e.TotalFileCount;
            };

            var session = MSession.GetOfflineSession(username);

            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = session,
                JVMArguments = null //curently bugged, set to null
            };

            string version = game_version;

            DataDownloader data = new DataDownloader(mf);
            List<ModpackData> json = Utility.modpacks;
            bool downloadStarted = false;

            foreach (var modpack in json)
            {
                if (version == modpack.Name)
                {
                    if (data.CheckLocal())
                    {
                        data.StartDownload();
                        downloadStarted = true;
                        break; // Exit the loop once download starts
                    }

                    version = modpack.API;
                }
            }

            if (!downloadStarted)
            {
                var process = await launcher.CreateProcessAsync(version, launchOption);

                process.Start();

                mf.Visible = false;
                mf.ShowInTaskbar = false;

                while (!process.WaitForExit(1000))
                {

                }

                Application.Restart();
            }
        }

    }
}
