using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeAutomation.DataObjects
{
    [DataContract]
    public class Floor
    {
        public Floor()
        {
            Walls = new List<LineData>();
            Objects = new List<ControllableObject>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public List<LineData> Walls { get; set; }

        [DataMember]
        public List<ControllableObject> Objects { get; set; }
    }


}
