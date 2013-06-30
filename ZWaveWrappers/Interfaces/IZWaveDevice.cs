using System;
namespace ZWaveWrappers.Interfaces
{
    public interface IZWaveDevice
    {
        string Name { get; }

        int NodeID { get; }

        event Action<IZWaveDevice> DeviceStateChanged;
    }
}
