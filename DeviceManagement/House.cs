using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZWaveWrappers;
using ZWaveWrappers.Interfaces;

namespace HomeAutomation.DeviceManagement
{
    public class House
    {
        private Wrapper m_zwaveWrapper;
        private List<IDevice> m_devices = new List<IDevice>();
        private List<Floor> m_floors = new List<Floor>();

        public House(bool online)
        {
            m_zwaveWrapper = new Wrapper(online ? Mode.Online : Mode.Offline);

            foreach (IZWaveDevice device in m_zwaveWrapper.Controller.Devices)
            {
                if (device is IBinarySceneSwitch)
                {
                    m_devices.Add(new BinaryLight(device as IBinarySceneSwitch));
                }
            }

            if (online)
            {
                m_devices.Add(new OfficeJet6500("OfficeJet6500"));
            }

        }

        private IEnumerable<IDevice> HardCodedDevices
        {
            get
            {
                yield return new WebController();
                yield return new OfficeJet6500("OfficeJet6500");
            }
        }

        public IEnumerable<IDevice> Devices
        {
            get
            {
                return m_devices;
            }
        }

        public IEnumerable<Floor> Floors
        {
            get
            {
                return m_floors;
            }
        }
    }
}
