using System.IO;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using FontAwesome.Sharp;

namespace PasswordManager.Utilities
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
            CheckBox optimizationBox = Application.OpenForms["MainForm"].Controls.Find("optimizationBox", true)[0] as CheckBox;
            MinecraftPath path = new MinecraftPath();
            CMLauncher launcher = new CMLauncher(path);

            File.WriteAllText(Paths.localUser, username);

            launchButton.Enabled = false;
            launcher.ProgressChanged += (s, p) =>
            {
                downloadBar.Value = p.ProgressPercentage;
            };

            launcher.FileChanged += (e) =>
            {
                //Console.WriteLine("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount);
                consoleLabel.Text = "[" + e.FileKind.ToString() + "] " + e.FileName + " - " + e.ProgressedFileCount + "//" + e.TotalFileCount;
            };

            var session = MSession.GetOfflineSession(username);

            string[] optimizations = new string[] { };

            if (optimizationBox.Checked)
            {
                optimizations = new string[] { "-XX:+UnlockExperimentalVMOptions", "-XX:G1NewSizePercent=20", "-XX:+UseG1GC", "-XX:MaxGCPauseMillis=25", "-XX:G1HeapRegionSize=32M" };
            }

            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = session,
                JVMArguments = optimizations
            };

            string version = game_version;
            if (version == "NewEra Ultimate")
            {
                DataDownloader data = new DataDownloader(mf);
                if (!data.CheckLocal())
                {
                    data.StartDownload();
                    data.StartInstall();
                    goto end;
                }
                version = "1.19.2-forge-43.2.8";
            }

            var process = await launcher.CreateProcessAsync(version, launchOption);


            process.Start();

            mf.Visible = false;
            mf.ShowInTaskbar = false;

            while (!process.WaitForExit(1000))
            {

            }

            Application.Restart();
        end:;


        }
    }
}
