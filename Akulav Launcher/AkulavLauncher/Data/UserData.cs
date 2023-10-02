using Newtonsoft.Json;
using System.IO;

namespace AkulavLauncher
{
    public class UserData
    {
        public string UserName { get; set; }
        public string Ram { get; set; }

        public void SetUserData(string UsernameUI, string RamUI)
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
