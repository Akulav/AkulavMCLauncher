using AkulavLauncher.Data;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace AkulavLauncher
{
    public static class Utility
    {
        public static List<ModpackData> modpacks = new List<ModpackData>();
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

        public static void GetModpacks()
        {
            try
            {
                using (WebClient client = new WebClient())
                {

                    string json = client.DownloadString(Paths.modpackList);
                    modpacks = JsonConvert.DeserializeObject<List<ModpackData>>(json);
                }
            }

            catch
            {
                modpacks = null;
            }
        }

    }
}