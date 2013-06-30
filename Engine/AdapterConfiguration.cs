using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine
{
    public class AdapterConfiguration
    {
        private Dictionary<string, AdaperConfigurationValue> values = new Dictionary<string, AdaperConfigurationValue>();

        public AdapterConfiguration(IEnumerable<AdaperConfigurationValue> configValues) {
            foreach (var configValue in configValues)
            {
                values[configValue.Name] = configValue;
            }
        }

        public String GetValue(String key)
        {
            return values[key].Value;
        }

        internal void Configure(Settings settings)
        {
            foreach (AdaperConfigurationValue value in values.Values)
            {
                value.SetFromSettings(settings);
            }
        }
    }
}
