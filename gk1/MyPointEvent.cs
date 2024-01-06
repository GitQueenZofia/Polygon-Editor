using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk1
{
    internal class MyPointEvent
    {
        public int orientation = 0;
        public int winding = 0;
        public MyPoint point;
        public MyPointEvent(MyPoint point, int orientation, int winding)
        {
            this.orientation = orientation;
            this.winding = winding;
            this.point = point;
        }

    }
}
