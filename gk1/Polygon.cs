using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace gk1
{
    internal class Polygon
    {
        public List<Line> lines;
        public List<Point> points;
        public Point start_point;
        public Point last_point;
        public bool offset_polygon = false;
        public int offset = 20;
        public bool clock = false;
        int r = 20;
        public bool finished;
        Brush brush;
        Brush offset_brush;
        Pen pen;
        Pen offset_pen;
        Dictionary<MyPoint, MyPoint> map;
        public static bool bresenham = false;
        public static bool multi = false;
        public static bool symmetric = false;
        public static Bitmap bitmap;

        public Polygon()
        {
            lines = new List<Line>();
            points = new List<Point>();
            finished = false;
            brush = new SolidBrush(Color.DeepPink);
            offset_brush = new SolidBrush(Color.RosyBrown);
            pen = new Pen(Color.DeepPink);
            offset_pen = new Pen(Color.RosyBrown);
            map=new Dictionary<MyPoint, MyPoint>(new PointComparer());
        }
        public void remove()
        {
            brush.Dispose();
            offset_brush.Dispose();
            pen.Dispose();
            offset_pen.Dispose();
        }
        public void drawPolygon(Graphics g, Pen p)
        {
            Color prev_color = p.Color;

            Brush bb;

            if (lines.Count == 0)
            {
                bb= new SolidBrush(Color.Black);
                g.FillEllipse(bb, last_point.X - r / 4, last_point.Y - r / 4, r / 2, r / 2);
                bb.Dispose();
                return;
            }

            start_point = lines[0].p1;

            foreach (Line l in lines)
            {
                Brush b = new SolidBrush(l.color);
                if (p.Color != Color.Red)  p.Color = l.color;
                p.Width = l.Width;

                if (l.p1.X == int.MinValue || l.p1.Y == int.MinValue) continue;
                g.FillEllipse(b, l.p1.X - r / 4, l.p1.Y - r / 4, r / 2, r / 2);
               
                drawLine(g, p, l.p1, l.p2);

                int x = (l.p1.X + l.p2.X) / 2;
                int y = (l.p1.Y + l.p2.Y) / 2;
                Font font = new Font("Arial", r/2);

                if (l.vertical == true)
                {
                    string textToDraw = "V";
                    g.FillEllipse(b, x-r/2, y-r/2, r, r);
                    g.DrawString(textToDraw, font, Brushes.Black, x-r/2, y-r/2);
                }
                if (l.horizontal == true)
                {
                    string textToDraw = "H";
                    g.FillEllipse(b, x - r / 2, y - r / 2, r, r);
                    g.DrawString(textToDraw, font, Brushes.Black, x - r / 2, y - r / 2);
                }

            }

            if (lines[0].p1 != lines[^1].p2)
            {
                bb = new SolidBrush(Color.Black);
                g.FillEllipse(bb, lines[^1].p2.X - r / 4, lines[^1].p2.Y - r / 4, r / 2, r / 2);
                bb.Dispose();
            }

            if (offset_polygon==true)
               offsetPolygon(offset, g);

            p.Color = prev_color;


        }
        public void checkEdges(int start_index, int i)
        {
            int index = start_index;
            int prev_index = index;
            int j = 0;
            while (j != lines.Count - 1)
            {
                Line line = lines[index];
                if (line.vertical == true)
                {
                    Line.makeVertical(line, i);
                }
                else if (line.horizontal == true)
                {
                    Line.makeHorizontal(line, i);
                }
                else return;

                prev_index = index;
                if (i == 1)
                {
                    index = (index - 1 + lines.Count) % lines.Count;
                    lines[index].p2 = lines[prev_index].p1;
                }
                else
                {
                    index = (index + 1) % lines.Count;
                    lines[index].p1 = lines[prev_index].p2;
                }
                j++;
            }
        }
        public bool isInsidePolygon(Point p)
        {
            bool oddNodes = false;
            foreach (Line line in lines)
            {
                if (((line.p1.Y > p.Y) != (line.p2.Y > p.Y)) && (p.X < (line.p2.X - line.p1.X) * (p.Y - line.p1.Y) / (line.p2.Y - line.p1.Y) + line.p1.X))
                    oddNodes = !oddNodes;
            }
            return oddNodes;
        }
        public bool isClockwisePolygon()
        {
            double sumOfAngles = 0.0;
            for (int i = 0; i < lines.Count; i++)
            {
                Point curr = lines[i].p1;
                Point next = lines[(i + 1) % lines.Count].p1;

                sumOfAngles += (next.X - curr.X) * (next.Y + curr.Y);
            }
            return sumOfAngles < 0;
        }
        public void offsetPolygon(int distance, Graphics g)
        {
            List<Line> result = new List<Line>();
            List<MyPoint> result_p= new List<MyPoint>();
            int outer_ccw = isClockwisePolygon() ? 1 : -1;

            Line prev = lines[lines.Count-1];
            Line curr = lines[0];
            Point p,prev_p = new Point(0, 0) ;
            

            for (int i = 0; i < lines.Count; i++)
            {
                curr = lines[i];
                // v1
                float v1X = curr.p2.X - curr.p1.X;
                float v1Y = curr.p2.Y - curr.p1.Y;
                // wektor prostopadly do v1
                Vector2 v1 = new Vector2(v1X, v1Y);
                Vector2 v1n;
                if (v1X * v1X + v1Y * v1Y < 0.001)
                    v1n = new Vector2(0, 0);
                else v1n = Vector2.Normalize(v1);
                float v1nX = v1n.Y;
                float v1nY = -v1n.X;
                
                // v2
                int v2X = curr.p1.X - prev.p1.X;
                int v2Y = curr.p1.Y - prev.p1.Y;
                // wektor prostopadły do v2
                Vector2 v2 = new Vector2(v2X, v2Y);
                Vector2 v2n;
                if (v2X * v2X + v2Y * v2Y < 0.001)
                    v2n = new Vector2(0, 0);
                else v2n = Vector2.Normalize(v2);
                float v2nX = v2n.Y;
                float v2nY = -v2n.X;

                // wektor dwueiecznej
                float bisX = (v1nX + v2nX) * outer_ccw;
                float bisY = (v1nY + v2nY) * outer_ccw;
                Vector2 v = new Vector2(bisX, bisY);
                Vector2 vn = Vector2.Normalize(v);

                // długość z trygonometrii len = dst/cos(a/2) = dst/(sqrt((1+cosa).2)
                // cosa = |u.v| = xu*xv + yu*yv
                double len = distance / Math.Sqrt((double)((1 + v1nX * v2nX + v1nY * v2nY) / 2));

                if (i == 0)
                {
                    prev_p = new Point((int)(curr.p1.X + len * vn.X), (int)(curr.p1.Y + len * vn.Y)); 
                }
                else
                {
                    p = new Point((int)(curr.p1.X + len * vn.X), (int)(curr.p1.Y + len * vn.Y));
                    result.Add(new Line(prev_p, p));
                    prev_p = p;
                }
                prev = curr;
                result_p.Add(new MyPoint((int)(curr.p1.X + len * vn.X), (int)(curr.p1.Y + len * vn.Y), 0));
            }
            result.Add(new Line(prev_p, result[0].p1));

            intersections(result_p,result,g);
        }
        public void intersections(List<MyPoint> points, List<Line> edges,Graphics g)
        {
            MyPoint prev = points[points.Count - 1];

            Queue<MyPointEvent> queue = new Queue<MyPointEvent>();
            HashSet<MyPoint>seen= new HashSet<MyPoint>(new PointComparer());
            MyPoint startX = points[0];

            map=new Dictionary<MyPoint, MyPoint> ();

            int k = points.Count-1;
            foreach (var p in points)
            {
                MyPoint curr = p;

                if (p.X < startX.X) startX = p;

                map.Add(prev,curr);

                edges[k].Start = prev;
                edges[k].End = curr;

                prev = curr;

                k=(k+1)%points.Count;
            }
            int j,i = 0;
            while(i<edges.Count)
            {
                j = i+1;
                while(j<edges.Count)
                {
                    if (edges[i].p1 == edges[j].p2 || edges[i].p2 == edges[j].p1 || i == j) { j++; continue; }

                    Line l1 = edges[i];
                    Line l2 = edges[j];
                    if (l1.Intersects(l2))
                    {
                        Point p = l1.IntersectionPoint(l2);
                        MyPoint p0 = new MyPoint(p, 0);
                        MyPoint p1 = new MyPoint(p, 1);
                        p0.intersection = true;
                        p1.intersection = true;
                        p0.twin = p1;
                        p1.twin = p0;

                        Line ll1 = new Line(p, l1.p2);
                        Line ll2 = new Line(p, l2.p2);
                        ll1.Start = p1;
                        ll2.Start = p0;
                        ll1.End = edges[i].End;
                        ll2.End = edges[j].End;

                        edges.Add(ll1);
                        edges.Add(ll2);

                        map[edges[i].Start] = p0;
                        map.Add(p0, edges[j].End);
                        map[edges[j].Start] = p1;
                        map.Add(p1, edges[i].End);

                        edges[i].p2 = p;
                        edges[j].p2 = p;

                        edges[i].End = p0;
                        edges[j].End = p1;
                    }
                    j++;
                }
                i++;
            }

            int clock = isClockwisePolygon() ? 1 : -1;

            queue.Enqueue(new MyPointEvent(startX,clock,0));

            while(queue.Count>0)
            {
                MyPointEvent v0 = queue.Dequeue();
                Polygon polygon = new Polygon();
                polygon.lines.Add(new Line(new Point(v0.point.X, v0.point.Y), new Point(0, 0),true));

                MyPoint p0 = v0.point;
                MyPoint p1 = map[p0];
                MyPoint p2 = map[p1];

                while(p1.X!=v0.point.X||p1.Y!=v0.point.Y)
                {
                    //if (polygon.lines.Find(line=>((line.p1.X==p1.X&&line.p1.Y==p1.Y)||(line.p2.X==p1.X&&line.p2.Y==p1.Y)))!=null) break;

                    polygon.lines[^1].p2 = new Point(p1.X,p1.Y);
                    polygon.lines.Add(new Line(new Point(p1.X, p1.Y), new Point(0, 0),true));

                    if (p1.intersection&&!seen.Contains(p1)&&!seen.Contains(p1.twin))
                    {
                        int orientation = Line.CrossProduct(p0, p1, p2) <0 != v0.orientation > 0 ? -v0.orientation : v0.orientation;
                        
                        queue.Enqueue(new MyPointEvent(p1.twin,orientation,v0.winding+orientation));
                        seen.Add(p1);
                    }
                    p0 = p1;
                    p1 = map[p0];
                    p2 = map[p1];
                }

                if (v0.winding != 0) continue;

                polygon.lines[^1].p2 = polygon.lines[0].p1;
                polygon.finished = true;
                polygon.drawPolygon(g, offset_pen);
            }         
        }
        public static void drawLine(Graphics g,Pen p, Point p1, Point p2)
        {
            if (p1.X == int.MinValue || p1.Y == int.MinValue || p2.X == int.MinValue || p2.Y == int.MinValue) return;
            if (bresenham)
            {
                Bresenham.drawBresenham(g, p, p1, p2, bitmap);
                return;
            }
            else if(symmetric)
            {
                Symmetric.drawSymmetric(g, p, p1, p2, bitmap);
                return;
            }
            g.DrawLine(p, p1, p2);
        }
    }
}
