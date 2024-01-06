namespace gk1
{
    internal class Line
    {
        public Point p1;
        public Point p2;
        public MyPoint Start;
        public MyPoint End;
        public bool horizontal = false;
        public bool vertical = false;
        public int index;
        public Color color;
        public int Width;
        public Line(Point p1, Point p2,bool offset=false,int i = 0) {
            this.p1 = p1;
            this.p2 = p2;
            this.index = i;
            this.Start = new MyPoint(p1, 0);
            this.End= new MyPoint(p2, 0);
            this.Width = 1;

            Random r = new Random();
            if (offset==false) color = Color.FromArgb(r.Next(190)+56, r.Next(190)+56, r.Next(190)+56);
            else color = Color.Black;
        }
        public static double CrossProduct(MyPoint p1, MyPoint p2, MyPoint p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }
        public bool Intersects(Line b)
        {
            double d1 = CrossProduct(Start, End, b.Start);
            double d2 = CrossProduct(Start, End, b.End);
            double d3 = CrossProduct(b.Start, b.End, Start);
            double d4 = CrossProduct(b.Start, b.End, End);

            return (d1 * d2 < 0 && d3 * d4 < 0);
        }
        public Point IntersectionPoint(Line b)
        {
            if (!Intersects(b))
                throw new InvalidOperationException("Szukany punkt przecięcia nie istnieje (lub jest końcem któregoś z odcinków)!");

            double determinant = (Start.X - End.X) * (b.Start.Y - b.End.Y) - (Start.Y - End.Y) * (b.Start.X - b.End.X);

            double x = ((Start.X * End.Y - Start.Y * End.X) * (b.Start.X - b.End.X) - (Start.X - End.X) * (b.Start.X * b.End.Y - b.Start.Y * b.End.X)) / determinant;
            double y = ((Start.X * End.Y - Start.Y * End.X) * (b.Start.Y - b.End.Y) - (Start.Y - End.Y) * (b.Start.X * b.End.Y - b.Start.Y * b.End.X)) / determinant;

            return new Point((int)x, (int)y);
        }
        public bool Equals(Line s2)
        {
            if (s2 == null)
                return false;

            if (Start.X != s2.Start.X || Start.Y != s2.Start.Y || End.X != s2.End.X || End.Y != s2.End.Y)
                return false;
            return true;
        }
        public static void makeVertical(Line l, int i)
        {
            if (i == 1)
                l.p1.X = l.p2.X;
            else
                l.p2.X = l.p1.X;
        }
        public static void makeHorizontal(Line l, int i)
        {
            if (i == 1)
                l.p1.Y = l.p2.Y;
            else
                l.p2.Y = l.p1.Y;
        }
    }
}
