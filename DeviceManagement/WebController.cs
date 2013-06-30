using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HomeAutomation.DeviceManagement
{
    public class WebController : DeviceBase
    {
        private string m_name;
        private string m_status;

        public WebController()
        {
            m_name = "Web Controller";
            m_status = "OK";
        }

        public override void Poll()
        {
            //do nothing
        }

        public override string Name { get { return m_name; } }

        public override string Status { get { return m_status; } }

        public override DataTable DataTable
        {
            get
            {
                DataTable table = new DataTable();

                table.Columns.Add("Name", typeof(System.String)).Caption = "Name";
                table.Columns.Add("Status", typeof(System.String)).Caption = "Status";
                table.Rows.Add(new object[] {  Name , Status });

                return table;
            }
        }

        public override string ExecuteFunction(string function, string args)
        {
            throw new NotImplementedException();
        }
    }
}
