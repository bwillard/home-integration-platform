using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HomeAutomation.DataObjects;

namespace ZWaveDeviceBridge.DataService
{
    [ServiceContract(CallbackContract = typeof(IHomeStateView))]
    public interface IHomeStatusService
    {
        [OperationContract]
        House GetHouse();

        [OperationContract(IsOneWay = true)]
        void UpdateState(ControllableObject controllableObject);
    }
}
