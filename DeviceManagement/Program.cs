using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace HomeAutomation.DeviceManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            OfficeJet6500 printer = new OfficeJet6500("officejet6500");
            printer.Poll();
        }
    }
}
