using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ZWaveDeviceBridge
{
    [Serializable]
    public class Settings
    {
        private static String FileName = "../../../Settings.xml";

        public string TwilioAcccountSid { get; set; }
        public string TwilioAuthToken { get; set; }
        public string TwilioFromNumber { get; set; }


        public static Settings LoadSettings()
        {
            using (StreamReader myReader = new StreamReader(FileName, false))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(Settings));
                return (Settings)mySerializer.Deserialize(myReader);
            }
        }

        public void SaveSettings()
        {
            using (StreamWriter myWriter = new StreamWriter(FileName, false))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(Settings));
                mySerializer.Serialize(myWriter, this);
            }
        }
    }
}
