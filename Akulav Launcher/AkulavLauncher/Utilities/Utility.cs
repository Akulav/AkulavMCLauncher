using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PasswordManager
{
    class Utility
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        public static void EnforceAdminPrivilegesWorkaround()
        {
            RegistryKey rk;
            string registryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\";

            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                }
                else
                {
                    rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                }

                rk = rk.OpenSubKey(registryPath, true);
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("Please run as administrator");
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static void SetRam()
        {
            GetPhysicallyInstalledSystemMemory(out long memKb);
            TrackBar ramSlider = Application.OpenForms["MainForm"].Controls.Find("ramSlider", true)[0] as TrackBar;
            ramSlider.Maximum = Convert.ToInt32(memKb / 1024 / 1024);
        }
    }
}
