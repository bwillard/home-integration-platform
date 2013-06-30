using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HomeAutomation.DataObjects;

namespace ZWaveDeviceBridge.DataService
{
    [ServiceContract(Namespace = "ZWaveDeviceBridge.DataService")]
    public interface IHomeStateView
    {
        [OperationContract(IsOneWay = true)]
        void ObjectStateChanged(ControllableObject changedObject);
    }
}
