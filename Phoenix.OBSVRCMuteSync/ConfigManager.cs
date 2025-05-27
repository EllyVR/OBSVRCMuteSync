using Newtonsoft.Json;

namespace Phoenix.OBSVRCMuteSync;

public class ConfigManager
{
    public static Config Config;

    public static void Load()
    {
        if (!File.Exists("config.json"))
        {
            Config = new Config();
            File.WriteAllText("Config.json", JsonConvert.SerializeObject(Config, Formatting.Indented));
            Environment.Exit(0);
        }
        else
        {
            Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
        }
    }
}

public class Config
{
    public string OBSWebsocketURL { get; set; } = "ws://localhost:4455";
    public string ObsControlPassword { get; set; } = "password";
    public string OBSMicName = "";
}