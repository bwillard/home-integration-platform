using System.Data;
using System.Drawing;
namespace HomeAutomation.DeviceManagement
{
    public interface IDevice
    {
        void Poll();

        int FloorIndex { get; set; }

        Point Possition { get; set; }

        string Name { get; }

        string Status { get; }

        DataTable DataTable { get; }

        string ExecuteFunction(string function, string args);
    }
}
