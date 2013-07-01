using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine.Adapters
{
    public interface IDimmable
    {
        void SetLevel(int level);
        int GetLevel();

        event Action<int> LevelChanged;
    }
}
