using System.Runtime.Serialization;

namespace HomeAutomation.DataObjects
{
    [DataContract]
    public class Light : ControllableObject
    {
        [DataMember]
        public bool On { get; set; }
    }
}
