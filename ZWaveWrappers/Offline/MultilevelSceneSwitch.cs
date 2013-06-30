using ZWaveWrappers.Interfaces;
using System;

namespace ZWaveWrappers.Offline
{
    public class MultilevelSceneSwitch : IMultilevelSceneSwitch
    {
        private byte m_level;

        public MultilevelSceneSwitch(int nodeId)
        {
            NodeID = nodeId;
        }

        public event Action<IZWaveDevice> DeviceStateChanged;

        public string Name
        {
            get { return "Offline dimable switch"; }
        }

        public int NodeID {get;private set;}

        public byte Level 
        {
            get
            {
                return m_level;
            }
            set
            {
                m_level = value;
            }
        }

        public void Ping()
        {
        }

        public void PowerOn()
        {
            Level = 255;
        }

        public void PowerOff()
        {
            Level = 0;
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
