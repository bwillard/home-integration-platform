using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine
{
    public class AdaperConfigurationValue
    {
        public AdaperConfigurationValue(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public string Value { get; set; }

        public void SetFromSettings(Settings settings)
        {
            Value = settings.ConfigSettings[Name];
        }
    }
}
