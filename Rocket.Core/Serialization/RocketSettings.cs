using Rocket.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rocket.Core.Serialization
{
    public enum CommandPriority { Low = -1, Normal = 0, High = 1 };

    public sealed class RemoteConsole
    {
        [JsonProperty("enabled")]
        public bool Enabled = false;
        [JsonProperty("port")]
        public ushort Port = 27115;
        [JsonProperty("password")]
        public string Password = "changeme";
        [JsonProperty("enableMaxGlobalConnections")]
        public bool EnableMaxGlobalConnections = true;
        [JsonProperty("maxGlobalConnections")]
        public ushort MaxGlobalConnections = 10;
        [JsonProperty("enableMaxLocalConnections")]
        public bool EnableMaxLocalConnections = true;
        [JsonProperty("maxLocalConnections")]
        public ushort MaxLocalConnections = 3;
    }

    public sealed class AutomaticShutdown
    {
        [JsonProperty("enabled")]
        public bool Enabled = false;
        [JsonProperty("interval")]
        public int Interval = 86400;
    }

    public sealed class WebPermissions
    {
        [JsonProperty("enabled")]
        public bool Enabled = false;
        [JsonProperty("url")]
        public string Url = "";
        [JsonProperty("interval")]
        public int Interval = 180;
    }

    public sealed class WebConfigurations
    {
        [JsonProperty("enabled")]
        public bool Enabled = false;
        [JsonProperty("url")]
        public string Url = "";
    }

    public sealed class CommandMapping
    {
        [JsonProperty("name")]
        public string Name = "";

        [JsonProperty("enabled")]
        public bool Enabled = true;

        [JsonProperty("priority")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CommandPriority Priority = CommandPriority.Normal;

        [JsonProperty("class")]
        public string Class = "";
        public CommandMapping()
        {

        }

        public CommandMapping(string name,string @class, bool enabled = true, CommandPriority priority = CommandPriority.Normal)
        {
            Name = name;
            Enabled = enabled;
            Class = @class;
            Priority = priority;
        }
    }

    public sealed class RocketSettings : IDefaultable
    {
        [JsonProperty("rcon")]
        public RemoteConsole RCON = new RemoteConsole();

        [JsonProperty("automaticShutdown")]
        public AutomaticShutdown AutomaticShutdown = new AutomaticShutdown();

        [JsonProperty("webConfigurations")]
        public WebConfigurations WebConfigurations = new WebConfigurations();

        [JsonProperty("webPermissions")]
        public WebPermissions WebPermissions = new WebPermissions();

        [JsonProperty("languageCode")]
        public string LanguageCode = "en";

        [JsonProperty("maxFrames")]
        public int MaxFrames = 60;

        [JsonProperty("forceJSON")]
        public bool ForceJSON { get; set; }
        
        public void LoadDefaults()
        {
            RCON = new RemoteConsole();
            AutomaticShutdown = new AutomaticShutdown();
            WebConfigurations = new WebConfigurations();
            WebPermissions = new WebPermissions();
            LanguageCode = "en";
            MaxFrames = 60;
            ForceJSON = true;
        }
    }
}