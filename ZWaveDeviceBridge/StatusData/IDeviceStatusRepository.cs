using System;
using System.Collections.Generic;

namespace ZWaveDeviceBridge.StatusData
{
    public interface IDeviceStatusRepository
    {
        void Add(DeviceStatus product);

        DeviceStatus GetStatus(Guid statusId);

        DeviceStatus GetLatestStatus(int id);

        ICollection<DeviceStatus> GetStatus(int id);

        ICollection<DeviceStatus> GetStatus(DateTime beging, DateTime ending);
    }
}
