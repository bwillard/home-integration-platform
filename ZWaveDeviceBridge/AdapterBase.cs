using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZWaveDeviceBridge
{
    abstract class AdapterBase
    {
        private Settings settings;
        protected AdapterBase(AdapterConfiguration configuration, Settings settings)
        {
            this.settings = settings;
            this.Configuration = configuration;
            this.Configuration.Configure(settings);
        }

        public AdapterConfiguration Configuration { get; private set; }

    }
}
