using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZWaveDeviceBridge.Adapters
{
    public interface IDimmable
    {
        void SetLevel(int level);
        int GetLevel();
    }
}
