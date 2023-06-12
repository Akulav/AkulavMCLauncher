﻿using Newtonsoft.Json;
using System.IO;
namespace AkulavLauncher
{
    public static class UserSettings
    {
        public static void SetUserData(string UsernameUI, string RamUI)
        {
            var ud = new UserData
            {
                UserName = UsernameUI,
                Ram = RamUI
            };
            string jsonString = JsonConvert.SerializeObject(ud, Formatting.Indented);
            if (!Directory.Exists(Paths.mc))
            {
                Directory.CreateDirectory(Paths.mc);
            }
            File.WriteAllText(Paths.settings, jsonString);
        }
    }
}