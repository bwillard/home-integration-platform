using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;

namespace HomeAutomation.DeviceManagement
{
    public class OfficeJet6500 : DeviceBase
    {
        private string m_networkName;
        private string m_status;

        public OfficeJet6500(string networkName)
        {
            m_networkName = networkName;
        }

        public int BlackInkLevel { get; private set; }
        public int YellowInkLevel { get; private set; }
        public int CyanInkLevel { get; private set; }
        public int MagentaInkLevel { get; private set; }

        public int PagesPrinted { get; private set; }

        public override string Status { get { return m_status; } }

        public override string Name { get {return m_networkName;} }

        public override void Poll()
        {
            WebRequest reqest = WebRequest.Create(string.Format("http://{0}/index.htm?cat=info&page=printerInfo",m_networkName));
            HttpWebResponse response = (HttpWebResponse)reqest.GetResponse();

            TextReader reader = new StreamReader(response.GetResponseStream());
            string responseText = reader.ReadToEnd();

            BlackInkLevel= InkLevel("blackink", responseText);
            YellowInkLevel = InkLevel("yellowink", responseText);
            CyanInkLevel = InkLevel("cyanink", responseText);
            MagentaInkLevel = InkLevel("magentaink", responseText);

            m_status = GetStatus(responseText);


            reqest = WebRequest.Create(string.Format("http://officejet6500/index.htm?cat=info&page=usage", m_networkName));
            response = (HttpWebResponse)reqest.GetResponse();

            reader = new StreamReader(response.GetResponseStream());
            responseText = reader.ReadToEnd();

            PagesPrinted = GetPagesPrinted(responseText);
        }

        private int GetPagesPrinted(string response)
        {
            Regex regex = new Regex("Total Page Count:.*?<b>(\\d+)</b>", RegexOptions.Singleline);
            Match match = regex.Match(response);

            if (match == null)
            {
                return -1;
            }

            return int.Parse(match.Groups[1].Value.Trim('\n', ' '));
        }

        private string GetStatus(string response)
        {
            Regex regex = new Regex("<td class=\"status\">.*?<SPAN.*?>(.*?)</SPAN>.*?</td>", RegexOptions.Singleline);
            Match match = regex.Match(response);

            if (match == null)
            {
                return "Unknown";
            }

            return match.Groups[1].Value.Trim('\n',' ');
        }

        private static int InkLevel(string color, string response)
        {
            Regex regex = new Regex("blackink=(\\d+)");
            Match match = regex.Match(response);

            if (match == null)
            {
                return 0;
            }

            return int.Parse(match.Groups[1].Value);
        }

        public override DataTable DataTable
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(System.String)).Caption = "Name";
                dt.Columns.Add("Status", typeof(System.String)).Caption = "Status";
                dt.Columns.Add("PagesPrinter", typeof(System.String)).Caption = "PagesPrinter";
                dt.Columns.Add("BlackInkLevel", typeof(System.String)).Caption = "BlackInkLevel";
                dt.Columns.Add("CyanInkLevel", typeof(System.String)).Caption = "CyanInkLevel";
                dt.Columns.Add("YellowInkLevel", typeof(System.String)).Caption = "YellowInkLevel";
                dt.Columns.Add("MagentaInkLevel", typeof(System.String)).Caption = "MagentaInkLevel";

                dt.Rows.Add(new object[] {  Name , Status , PagesPrinted , BlackInkLevel , CyanInkLevel , YellowInkLevel , MagentaInkLevel });
                return dt;
            }
        }

        public override string ExecuteFunction(string function, string args)
        {
            throw new NotImplementedException();
        }
        
    }
}
