using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PasswordManager;
using PasswordManager.Utilities;


namespace AkulavLauncher
{
    public partial class MainForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );

        //Logic starts here
        public MainForm()
        {
            Utility.EnforceAdminPrivilegesWorkaround();
            InitializeComponent();
            CheckTheme();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void LeftTopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }


        private void CheckTheme()
        {
            Colors.ChangeTheme(Controls, this, "dark");
            Colors.ChangeTheme(rightpanel.Controls, this, "dark");
            Colors.ChangeTheme(leftpanel.Controls, this, "darker");
            leftpanel.BackColor = Colors.back_darker;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            Utility.setRam();
            DataDownloader data = new DataDownloader(this);
            data.getVersions();
            data.setUIText();
            data.setMetadata();

            if (!File.Exists(Paths.localMetadata) || File.ReadAllText(Paths.localMetadata) != data.mod_version)
            {
                convertToUpdate();
            }

        }

        private void convertToUpdate()
        {
            repairButton.Text = "Update";
            launchButton.Enabled = false;
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            GameLauncher gl = new GameLauncher(ramSlider.Value * 1024, Username.Text, versionBox.SelectedItem.ToString(), this);
        }

        private void repairButton_Click(object sender, EventArgs e)
        {
            DataDownloader data = new DataDownloader(this);
            data.StartDownload();
            data.StartInstall();
        }

        private void ramSlider_ValueChanged(object sender, EventArgs e)
        {
            ramLabel.Text = ramSlider.Value.ToString() + " GB of RAM";
            File.WriteAllText(Paths.ramData, ramSlider.Value.ToString());
        }
    }
}