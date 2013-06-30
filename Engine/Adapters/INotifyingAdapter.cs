using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeIntegrationPlatform.Engine.Adapters
{
    /// <summary>
    /// An adapter that can send a particular message to someone.
    /// </summary>
    public interface INotifyingAdapter
    {
        void Notify(String to, String message);
    }
}
