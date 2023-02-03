using System;
using System.Windows.Forms;

namespace AkulavLauncher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm lg = new MainForm();
            lg.Show();
            Application.Run();
        }
    }
}
