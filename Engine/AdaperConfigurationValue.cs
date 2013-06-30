using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZWaveDeviceBridge
{
    class AdaperConfigurationValue
    {
        private string configFileKey;

        public AdaperConfigurationValue(String name, String configFileKey)
        {
            this.Name = name;
            this.configFileKey = configFileKey;
        }

        public string Name { get; private set; }

        public string Value { get; set; }

        public void SetFromSettings(Settings settings)
        {
            Value = settings.ConfigSettings[configFileKey];
        }
    }
}
