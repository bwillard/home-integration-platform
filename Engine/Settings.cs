using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ZWaveDeviceBridge
{
    public class Settings
    {
        private static String FileName = "../../../Settings.json";

        [JsonProperty]
        public Dictionary<String, String> ConfigSettings { get; private set; }

        public static Settings LoadSettings()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader myReader = new StreamReader(FileName, false))
                {
                    return (Settings)JsonConvert.DeserializeObject<Settings>(myReader.ReadToEnd());
                }
            }

            Settings s =  new Settings();
            s.ConfigSettings = new Dictionary<string, string>();
            return s;
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
