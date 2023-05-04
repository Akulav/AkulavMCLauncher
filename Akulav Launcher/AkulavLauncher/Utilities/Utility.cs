using AkulavLauncher.Utilities;
using Microsoft.Win32;
using Newtonsoft.Json;
using PasswordManager.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace PasswordManager
{
    class Utility
    {
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

        public UserData GetUserJson()
        {
            string userdata = File.ReadAllText(Paths.settings);
            UserData ud = JsonConvert.DeserializeObject<UserData>(userdata);
            return ud;
        }
    }
}
