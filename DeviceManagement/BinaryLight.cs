using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZWaveWrappers.Interfaces;
using System.Threading;

namespace HomeAutomation.DeviceManagement
{
    public class BinaryLight : DeviceBase
    {
        IBinarySceneSwitch m_light;

        public BinaryLight(IBinarySceneSwitch binaryLight)
        {
            m_light = binaryLight;
        }

        public override void Poll()
        {
            m_light.Ping();
        }

        public override string Name
        {
            get
            {
                return m_light.Name;
            }
        }

        public override string Status
        {
            get
            {
                return m_light.ToString();
            }
        }

        public override DataTable DataTable
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(System.String)).Caption = "Name";
                dt.Columns.Add("Level", typeof(System.String)).Caption = "Level";

                dt.Rows.Add(new object[] { Name, Level, });
                return dt;
            }
        }

        public  byte Level
        {
            get
            {
                return m_light.Level;
            }
        }

        public void TurnOn()
        {
            m_light.PowerOn();
        }

        public void TurnOff()
        {
            m_light.PowerOff();
        }

        public override string ExecuteFunction(string function, string args)
        {
            if(0==string.Compare(function,"TurnOff",true))
            {
                TurnOff();
                return "OK";
            }
            else if (0 == string.Compare(function, "TurnOn", true))
            {
                TurnOn();
                return "OK";
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
