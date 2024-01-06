using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk1
{
    internal class MyPoint
    {
        public int X;
        public int Y;
        public int index;
        public bool intersection = false;
        public MyPoint twin;
        public MyPoint(int X, int Y, int index=0)
        {
            this.X = X;
            this.Y = Y;
            this.index = index;
        }
        public MyPoint(Point p,int index)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.index = index;
        }
        public bool Equals(MyPoint other)
        {
            return X == other.X && Y == other.Y;
        }
    }
    internal class PointComparer : IEqualityComparer<MyPoint>
    {
        public bool Equals(MyPoint p1, MyPoint p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public int GetHashCode(MyPoint obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }

    }
}

