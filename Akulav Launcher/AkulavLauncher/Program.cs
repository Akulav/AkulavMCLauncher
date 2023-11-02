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
            MainForm mf = new MainForm();
            mf.Show();
            Application.Run();
        }
    }
}
