using System.Runtime.Serialization;
namespace HomeAutomation.DataObjects
{
    [DataContract]
    public class LineData
    {
        [DataMember]
        public int X1 { get; set; }

        [DataMember]
        public int X2 { get; set; }

        [DataMember]
        public int Y1 { get; set; }

        [DataMember]
        public int Y2 { get; set; }
    }
}
