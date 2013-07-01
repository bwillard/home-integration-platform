using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine.Connectors
{
    /// <summary>
    /// Allows a method to be called when a toggle value changes
    /// </summary>
    class ToggledConnector
    {
        private Action output;
        private bool triggerOn;
        public ToggledConnector(Action output, bool triggerOn)
        {
            this.output = output;
            this.triggerOn = triggerOn;
        }

        public void Fire(bool value)
        {
            if (value == this.triggerOn)
            {
                this.output();
            }
        }
    }
}
