using System.Runtime.Serialization;

namespace HomeAutomation.DataObjects
{
    [DataContract]
    public class DimableLight : ControllableObject
    {
        [DataMember]
        public byte Level { get; set; }

   
    }
}
