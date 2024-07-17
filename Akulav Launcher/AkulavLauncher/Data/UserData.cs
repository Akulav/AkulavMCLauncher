using Newtonsoft.Json;
using System.IO;

namespace AkulavLauncher
{
    public class UserData
    {
        public string UserName { get; set; }
        public string Ram { get; set; }
        public string SelectedModpack { get; set; }

        public void SetUserData(string UsernameUI, string RamUI, string SelectedModpackUI)
        {
            var ud = new UserData
            {
                UserName = UsernameUI,
                Ram = RamUI,
                SelectedModpack = SelectedModpackUI
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
