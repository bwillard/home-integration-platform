using System;

namespace ZWaveDeviceBridge.StatusData
{
    public class DeviceStatus
    {
        public Guid Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime ChangeTime { get; set; }

        public string StatusString { get; set; }
    }
}
