using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk1
{
    internal class Scene
    {
        public static void addPolygons(ref List<Polygon> list)
        { 
            Polygon p1 = new Polygon();
            p1.lines.Add(new Line(new Point(50, 100), new Point(250, 100)));
            p1.lines[0].horizontal = true;
            p1.lines.Add(new Line(new Point(250,100), new Point(250, 300)));
            p1.lines[1].vertical = true;
            p1.lines.Add(new Line(new Point(250,300), new Point(50, 300)));
            p1.lines[2].horizontal = true;
            p1.lines.Add(new Line(new Point(50, 300), new Point(50, 100)));
            p1.lines[3].vertical = true;

            Polygon p2= new Polygon();
            p2.lines.Add(new Line(new Point(440, 40), new Point(480, 80)));
            p2.lines.Add(new Line(new Point(480, 80), new Point(380, 180)));
            p2.lines.Add(new Line(new Point(380, 180), new Point(500, 300)));
            p2.lines.Add(new Line(new Point(500, 300), new Point(620, 180)));
            p2.lines.Add(new Line(new Point(620, 180), new Point(520, 80)));
            p2.lines.Add(new Line(new Point(520, 80), new Point(560,40)));
            p2.lines.Add(new Line(new Point(560, 40), new Point(700, 180)));
            p2.lines.Add(new Line(new Point(700, 180), new Point(500, 380)));
            p2.lines.Add(new Line(new Point(500, 380), new Point(300, 180)));
            p2.lines.Add(new Line(new Point(300, 180), new Point(440, 40)));
            p2.offset_polygon = true;

            Polygon p3 = new Polygon();
            p3.lines.Add(new Line(new Point(700,100), new Point(1100,100)));
            p3.lines.Add(new Line(new Point(1100,100), new Point(950,300)));
            p3.lines.Add(new Line(new Point(950,300), new Point(700,100)));
            p3.lines[0].horizontal = true;



            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
        }
    }
}
