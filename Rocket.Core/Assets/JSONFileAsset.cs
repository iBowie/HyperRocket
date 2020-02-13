using Rocket.API;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Rocket.Core.Assets
{
    public class JSONFileAsset<T> : Asset<T> where T : class, IDefaultable
    {
        private JsonSerializerSettings settings;
        private Formatting formatting;
        private string file;
        T defaultInstance;
        public JSONFileAsset(string file, Formatting formatting = Formatting.Indented, JsonSerializerSettings serializerSettings = null, T defaultInstance = null)
        {
            this.settings = serializerSettings ?? new JsonSerializerSettings();
            this.formatting = formatting;
            this.file = file;
            this.defaultInstance = defaultInstance;
            Load();
        }
        public JSONFileAsset(string file, Formatting formatting = Formatting.Indented, T defaultInstance = null, params JsonConverter[] converters) : this(file, formatting, new JsonSerializerSettings() { Converters = converters }, defaultInstance)
        {

        }
        public override T Save()
        {
            try
            {
                string directory = Path.GetDirectoryName(file);
                if (string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                using (StreamWriter writer = new StreamWriter(file))
                {
                    if (instance == null)
                    {
                        if (defaultInstance == null)
                        {
                            instance = Activator.CreateInstance<T>();
                            instance.LoadDefaults();
                        }
                        else
                        {
                            instance = defaultInstance;
                        }
                    }
                    string content = JsonConvert.SerializeObject(instance, formatting, settings);
                    writer.Write(content);
                    return instance;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to serialize JSONFileAsset: {file}", ex);
            }
        }
        public override void Load(AssetLoaded<T> callback = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string content = reader.ReadToEnd();
                        instance = JsonConvert.DeserializeObject<T>(content, settings);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to deserialize JSONFileAsset: {file}", ex);
            }
        }
    }
}
