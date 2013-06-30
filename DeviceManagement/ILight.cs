using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeAutomation.DeviceManagement
{
    public interface ILight:IDevice
    {
        void TurnOn();
        void TurnOff();

        byte Level { get; }
    }
}
