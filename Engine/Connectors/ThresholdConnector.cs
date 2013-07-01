using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine.Connectors
{
    class ThresholdConnector
    {
        private Action output;
        private bool greaterThan;
        private int threshold;
        public ThresholdConnector(Action output, int threshold, bool greaterThan)
        {
            this.output = output;
            this.greaterThan = greaterThan;
            this.threshold = threshold;
        }

        public void Fire(int value)
        {
            bool matched = this.greaterThan ? (value > this.threshold) : (value < this.threshold);
            if (matched)
            {
                output();
            }
        }
    }
}
