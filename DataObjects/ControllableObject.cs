using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HomeAutomation.DataObjects
{
    [KnownType(typeof(Light))]
    [KnownType(typeof(DimableLight))]
    [XmlInclude(typeof(Light))]
    [XmlInclude(typeof(DimableLight ))]
    [DataContract]
    public class ControllableObject 
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }
}
