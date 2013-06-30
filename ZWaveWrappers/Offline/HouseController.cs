using ZWaveWrappers.Interfaces;
using System.Collections.Generic;

namespace ZWaveWrappers.Offline
{
    internal class HouseController: IHouseController
    {
        public IEnumerable<IZWaveDevice> Devices
        {
            get
            {
                yield return new BinarySceneSwitch(2);
                yield return new MultilevelSceneSwitch(4);
            }
        }


        public string AddDevice()
        {
            return "No OP";
        }
    }
}
