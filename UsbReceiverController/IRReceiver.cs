using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HomeAutomation.UsbReceiverController
{
    public class IRReceiver
    {
        private readonly Guid m_guid = new Guid("019a66ff-bfc5-4a69-a0c0-bb3355776677");
        WinUsb m_usbDevice;

        public event Action<IEnumerable<int>> DataReceived;

        public bool IsRunning { get; private set; }

        public void Start()
        {
            DeviceFinder deviceFinder = new DeviceFinder();
            string path = deviceFinder.GetDevicePath(m_guid);

            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Couldn't find path for device, make sure it is plugged in and is using the right driver");
            }

            m_usbDevice = new WinUsb(path);
            m_usbDevice.Connect();
            m_usbDevice.SetupEndPoints();
            IsRunning=true;

            ThreadPool.QueueUserWorkItem(Read);
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void Read(object ignored)
        {
            do
            {
                IEnumerable<int> data = m_usbDevice.BeginRead();
                if (data.Count() == 3 && data.Count(b => b == 0) < 2)
                {
                    InvokeDataReceived(data);
                }
            }
            while (IsRunning);
        }



        private void InvokeDataReceived(IEnumerable<int> data)
        {
            Action<IEnumerable<int>> action = DataReceived;
            if (null != action)
            {
                action(data);
            }
        }
    }
}
