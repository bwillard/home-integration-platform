using ZWaveWrappers.Interfaces;
using System;

namespace ZWaveWrappers.Offline
{
    public class BinarySceneSwitch : IBinarySceneSwitch
    {
        private bool m_on;

        public BinarySceneSwitch(int nodeId)
        {
            NodeID = nodeId;
        }

        public event Action<IZWaveDevice> DeviceStateChanged;

        public string Name
        {
            get { return "Offline switch"; }
        }

        public int NodeID {get;private set;}

        public byte Level
        {
            get { return (byte)(m_on ? 255 : 0); }
        }

        public void Ping()
        {
        }

        public void PowerOn()
        {
            m_on = true;
        }

        public void PowerOff()
        {
            m_on = false;
        }

        private void InvokeDeviceStateChanged()
        {
            var action = DeviceStateChanged;
            if (null != action)
            {
                action(this);
            }
        }
    }
}
