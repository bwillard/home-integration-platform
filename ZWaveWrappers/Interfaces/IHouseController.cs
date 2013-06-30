using System;
using System.Collections.Generic;

namespace ZWaveWrappers.Interfaces
{
    public interface IHouseController
    {
        IEnumerable<IZWaveDevice> Devices { get; }

        string AddDevice();
    }
}
