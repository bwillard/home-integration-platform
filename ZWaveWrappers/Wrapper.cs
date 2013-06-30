using ZWaveWrappers.Interfaces;
using System;

namespace ZWaveWrappers
{
    public class Wrapper
    {
        public Wrapper(Mode mode)
        {
            switch (mode)
            {
                case Mode.Offline:
                    Controller = new Offline.HouseController();
                    break;
                case Mode.Online:
                    //Controller = new ControlThinkAdapters.HouseController();
                    break;
                default:
                    throw new Exception("Unknown mode: " + mode);
            }
        }

        public IHouseController Controller { get; private set; }
    }
}
