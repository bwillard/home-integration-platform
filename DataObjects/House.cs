using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeAutomation.DataObjects
{
    [DataContract]
    public class House
    {
        public House()
        {
            Floors = new List<Floor>();
        }

        [DataMember]
        public List<Floor> Floors { get; set; }

        public void ItemUpdate(ControllableObject controllableObject)
        {
            foreach (Floor floor in Floors)
            {
                for (int i = 0; i < floor.Objects.Count; i++)
                {
                    if (floor.Objects[i].Id == controllableObject.Id)
                    {
                        floor.Objects[i] = controllableObject;
                    }
                }
            }
        }

        public ControllableObject GetItem(int id)
        {
            foreach (Floor floor in Floors)
            {
                for (int i = 0; i < floor.Objects.Count; i++)
                {
                    if (floor.Objects[i].Id == id)
                    {
                        return floor.Objects[i];
                    }
                }
            }
            return null;
        }
    }
}
