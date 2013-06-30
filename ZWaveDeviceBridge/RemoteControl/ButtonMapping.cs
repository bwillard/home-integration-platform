using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZWaveDeviceBridge.RemoteControl
{

    [Serializable]
    abstract class ButtonMapping
    {
        protected ButtonMapping(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract ButtonType ButtonType { get;}
    }

    [Serializable]
    class ButtonMappingAction : ButtonMapping
    {
        public ButtonMappingAction(ButtonAction action, string name)
            : base(name)
        {
            Action = action;
        }

        public override ButtonType ButtonType
        {
            get { return RemoteControl.ButtonType.Action; }
        }

        public ButtonAction Action { get; private set; }
    }

    [Serializable]
    class ButtonMappingTarget : ButtonMapping
    {
        public ButtonMappingTarget(int deviceId, string name)
            : base(name)
        {
            DeviceID = deviceId;
        }

        public override ButtonType ButtonType
        {
            get { return RemoteControl.ButtonType.Target; }
        }

        public int DeviceID { get; private set; }
    }
}
