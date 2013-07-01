using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HomeIntegrationPlatform.Engine
{
    public class Settings
    {
        private static string FileName = "../../../Settings.json";

        public Settings()
        {
            ConfigSettings = new Dictionary<string, string>();
            AssembliesToLoad = new List<string>();
        }

        [JsonProperty]
        public Dictionary<string, string> ConfigSettings { get; private set; }

        [JsonProperty]
        public List<string> AssembliesToLoad { get; private set; }

        public static Settings LoadSettings()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader myReader = new StreamReader(FileName, false))
                {
                    return (Settings)JsonConvert.DeserializeObject<Settings>(myReader.ReadToEnd());
                }
            }

            return new Settings();
        }

        public void SaveSettings()
        {
            using (StreamWriter myWriter = new StreamWriter(FileName, false))
            {
                myWriter.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            }
        }
    }
}
