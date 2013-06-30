using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ZWaveDeviceBridge.StatusData;
using System.IO;

namespace ZWaveDeviceBridge
{
    public class DBStuff
    {
        public static void CreateDB()
        {
            /*Configuration configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(DeviceStatus).Assembly);

            SchemaExport schemaExport = new SchemaExport(configuration);
            schemaExport.Execute(true,true, false);*/
        }
    }
}
