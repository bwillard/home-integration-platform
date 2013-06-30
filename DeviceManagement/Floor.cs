using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HomeAutomation.DeviceManagement
{
    public class Floor
    {
        private List<Rectangle> m_shapes = new List<Rectangle>();

        public int Index { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<Rectangle> Shapes
        {
            get
            {
                return m_shapes;
            }
        }
    }
}
