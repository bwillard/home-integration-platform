using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZWaveWrappers.Interfaces;
using HomeAutomation.UsbReceiverController;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace HomeIntegrationPlatform.Engine.RemoteControl
{
    class ButtonHandler
    {
        private IZWaveDevice m_currentDevice;
        private ButtonMappingStore m_store = new ButtonMappingStore("Default");
        private IRReceiver m_receiver = new IRReceiver();
        private bool m_training;
        private IEnumerable<IZWaveDevice>  m_devices;

        public ButtonHandler(IEnumerable<IZWaveDevice> devices)
        {
            m_receiver.DataReceived += DataReceived;
            m_devices = devices;
            try
            {
                m_receiver.Start();
            }
            catch (Exception)
            {
                Console.WriteLine("No IR reciever found");
            }
        }

        public ObservableCollection<ButtonMapping> Mappings
        {
            get
            {
                return m_store.Mappings;
            }
        }
        
        private void ButtonPressed(ButtonMapping mapping)
        {
            if (mapping.ButtonType == ButtonType.Target)
            {
                ButtonMappingTarget target = (ButtonMappingTarget)mapping;
                m_currentDevice = m_devices.FirstOrDefault(device => device.NodeID == target.DeviceID);
            }
            else
            {
                ButtonMappingAction action = (ButtonMappingAction)mapping;

                if (null == m_currentDevice)
                {
                    m_devices.ForEach(device => ApplyAction(device, action.Action));
                }
                else
                {
                    ApplyAction(m_currentDevice, action.Action);
                }
            }
        }

        private void ApplyAction(IZWaveDevice device, ButtonAction action)
        {
            IBinarySceneSwitch binarySwitch = device as IBinarySceneSwitch;
            IMultilevelSceneSwitch multiLevelSwitch = device as IMultilevelSceneSwitch;
            switch (action)
            {
                case ButtonAction.On:
                    if (null != binarySwitch)
                    {
                        binarySwitch.PowerOn();
                    }
                    break;
                case ButtonAction.Off:
                    if (null != binarySwitch)
                    {
                        binarySwitch.PowerOff();
                    }
                    break;
                case ButtonAction.Up:
                    if (null != multiLevelSwitch)
                    {
                        multiLevelSwitch.Level += 10;
                    }
                    break;
                case ButtonAction.Down:
                    if (null != multiLevelSwitch)
                    {
                        multiLevelSwitch.Level -= 10;
                    }
                    break;

            }
        }

        private void DataReceived(IEnumerable<int> data)
        {
            ButtonMapping mapping = m_store.GetMapping(data);

            if (null != mapping)
            {
                ButtonPressed(mapping);
            }
        }

        internal void Train()
        {
            ButtonMapping mapping;
            if (MessageBoxResult.Yes == MessageBox.Show("Is this an action? (as opposed to a device)", "Action?", MessageBoxButton.YesNo))
            {
                ButtonAction action = PickActionWindow.Display();
                mapping = new ButtonMappingAction(action, action.ToString());
            }
            else
            {
                int deviceId = PickDeviceWindow.Display();
                mapping = new ButtonMappingTarget(deviceId, "Device " + deviceId);
            }


            if (m_training)
            {
                return;
            }
            m_training = true;
            AutoResetEvent gotData = new AutoResetEvent(false);
            IEnumerable<int> data1 = null;
            IEnumerable<int> data2 = null;

            IEnumerable<int> data = null;

            Action<IEnumerable<int>> handler = d =>
            {
                data = d;
                gotData.Set();
            };

            m_receiver.DataReceived += handler;
            try
            {
                MessageBox.Show("After hitting ok please press the button to train twice.");

                gotData.WaitOne();
                data1 = data;


                gotData.WaitOne();

                data2 = data;

                
                if (!data1.Same(data2))
                {
                   MessageBox.Show("Data doesn't match, can't train sorry.");
                    return;
                }

                m_store.AddMapping(data1, mapping);
               
            }
            finally
            {
                m_receiver.DataReceived -= handler;
                m_training = false;
            }
        }
    }
}
