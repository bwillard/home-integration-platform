using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZWaveDeviceBridge.Adapters
{
    interface IToggle
    {
        void TurnOn();
        void TurnOff();

        bool IsOn();
    }
}
