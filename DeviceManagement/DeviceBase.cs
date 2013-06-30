using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeAutomation.DeviceManagement
{
    public abstract class DeviceBase: IDevice
    {
        public abstract void Poll();

        public int FloorIndex { get; set; }

        public System.Drawing.Point Possition { get; set; }

        public abstract string Name {get;}

        public abstract string Status {get;}

        public abstract System.Data.DataTable DataTable {get;}

        public abstract string ExecuteFunction(string function, string args);
    }
}
