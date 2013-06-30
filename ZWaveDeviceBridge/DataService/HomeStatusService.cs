using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using HomeAutomation.DataObjects;
using System.ServiceModel.Web;
using System.IO;
using System.Xml.Serialization;
using ZWaveWrappers;
using ZWaveWrappers.Interfaces;

namespace ZWaveDeviceBridge.DataService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class HomeStatusService :IHomeStatusService, IXSSPolicy
    {
        private string m_houseName = "Default";

        private Wrapper m_zwaveWrapper = MainWindow.Wrapper;
        //private DataZWaveSync m_dataSync = new DataZWaveSync();

        public event Action<ControllableObject> StateChanged;

        public House House { get; private set; }

        public HomeAutomation.DataObjects.House GetHouse()
        {
            House = new House();

            if (File.Exists(FileName))
            {
                House = LoadHouse();
            }
            else
            {
                House = GetDefaultHouse();
                SaveHouse(House);
            }

            foreach (IZWaveDevice device in m_zwaveWrapper.Controller.Devices)
            {
                ControllableObject data = House.GetItem(device.NodeID);

                //m_dataSync.PushFromZwaveToData(device, data);

                device.DeviceStateChanged += DeviceStatusChanged;
            }

            return House;
        }

        public void DeviceStatusChanged(IZWaveDevice device)
        {
            ControllableObject data = House.GetItem(device.NodeID);
            //m_dataSync.PushFromZwaveToData(device, data);
            InvokeStateChanged(data);
        }

        private void InvokeStateChanged(ControllableObject controllableObject)
        {
            House.ItemUpdate(controllableObject);
            SaveHouse(House);

            Action<ControllableObject> action = StateChanged;
            if (null != action)
            {
                action(controllableObject);
            }
        }

        IHomeStateView Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IHomeStateView>();
            }
        }


        public void UpdateState(HomeAutomation.DataObjects.ControllableObject controllableObject)
        {
            throw new NotImplementedException();
        }

        private string FileName
        {
            get
            {
                return Path.Combine("C:\\Data\\", m_houseName + ".hfd");
            }
        }

        private House GetDefaultHouse()
        {
            House house = new House();
            Floor floor = new Floor { Name = "Main", Width = 500, Height = 500 };
            floor.Objects.Add(new Light { Name = "My light", Id = 2, X = 50, Y = 10, On = true });
            floor.Objects.Add(new DimableLight { Name = "Dim light", Id = 4, X = 200, Y = 200, Level = 50 });
            floor.Walls.Add(new LineData { X1 = 0, Y1 = 0, X2 = 500, Y2 = 0 });
            floor.Walls.Add(new LineData { X1 = 0, Y1 = 0, X2 = 0, Y2 = 500 });
            floor.Walls.Add(new LineData { X1 = 500, Y1 = 500, X2 = 500, Y2 = 0 });
            floor.Walls.Add(new LineData { X1 = 500, Y1 = 500, X2 = 0, Y2 = 500 });
            house.Floors.Add(floor);
            return house;
        }

        private House LoadHouse()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(House));

            using (FileStream fs = File.Open(FileName, FileMode.Open))
            {
                return (House)serializer.Deserialize(fs);
            }
        }

        private void SaveHouse(House house)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(House));

            FileMode fileMode;

            if (File.Exists(FileName))
            {
                fileMode = FileMode.Truncate;
            }
            else
            {
                fileMode = FileMode.CreateNew;
            }

            using (FileStream fs = File.Open(FileName, fileMode))
            {
                serializer.Serialize(fs, house);
            }
        }


        Stream StringToStream(string result)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml";
            return new MemoryStream(Encoding.UTF8.GetBytes(result));
        }

        public Stream GetSilverlightPolicy()
        {
            Console.WriteLine("called GetSilverlightPolicy");
            string result = @"<?xml version=""1.0"" encoding=""utf-8""?>
<access-policy>
    <cross-domain-access>
        <policy>
            <allow-from http-request-headers=""*"">
                <domain uri=""*""/>
            </allow-from>
            <grant-to>
                <resource path=""/"" include-subpaths=""true""/>
            </grant-to>
        </policy>
    </cross-domain-access>
</access-policy>";
            return StringToStream(result);
        }
    }
}
