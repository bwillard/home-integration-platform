using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine.Adapters
{
    interface IToggle
    {
        void TurnOn();
        void TurnOff();

        bool IsOn();

        event Action<bool> Toggled;
    }
}
