using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http.Headers;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace gk1
{
    public partial class Form1 : Form
    {
        List<Polygon> polygons;
        bool start_drawing = true;
        Polygon current_polygon;
        Point current_point;
        Bitmap drawing_bitmap;

        int r = 20;
        int radius = 20;

        bool drawing = true;
        bool selecting = false;
        //bool bresenham = false;

        Point? selected_vertex = null;
        Line? selected_edge = null;
        Polygon? selected_polygon = null;
        bool selected = false;

        int vertex_index1 = 0;
        int vertex_index2 = 0;
        int edge_index = 0;

        Point prev_location;
        Pen pen;
        Pen thickPen;

        public Form1()
        {
            InitializeComponent();

            pen = new Pen(Color.DeepPink);
            thickPen = new Pen(Color.DeepPink, 3);
            thickPen.Width = 3;
            polygons = new List<Polygon>();

        }
        private void whiteboard_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g;

            if (drawing)
            {
                if (!start_drawing)
                {
                    g = drawAndInvalidate();

                    double r1 = e.Location.X - (current_polygon.start_point.X - r / 2);
                    double r2 = e.Location.Y - (current_polygon.start_point.Y - r / 2);

                    if (r1 * r1 + r2 * r2 <= r * r)
                    {
                        if (current_polygon.lines.Count > 1)
                            g.DrawEllipse(Pens.Red, current_polygon.start_point.X - r, current_polygon.start_point.Y - r, 2 * r, 2 * r);
                    }
                    Polygon.drawLine(g, pen, current_point, new Point(e.X, e.Y));
                    g.Dispose();
                }
            }
            if (selecting)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (selected_vertex != null)
                    {
                        selected_polygon.lines[vertex_index1].p2 = new Point(e.X, e.Y);
                        selected_polygon.lines[vertex_index2].p1 = new Point(e.X, e.Y);
                        selected_vertex = new Point(e.X, e.Y);

                        selected_polygon.checkEdges(vertex_index1, 1);
                        selected_polygon.checkEdges(vertex_index2, 2);
                    }
                    else if (selected_edge != null)
                    {
                        Point p1 = new Point(selected_edge.p1.X + e.X - prev_location.X, selected_edge.p1.Y + e.Y - prev_location.Y);
                        Point p2 = new Point(selected_edge.p2.X + e.X - prev_location.X, selected_edge.p2.Y + e.Y - prev_location.Y);

                        selected_polygon.lines[edge_index].p1 = p1;
                        selected_polygon.lines[edge_index].p2 = p2;

                        int lines_count = selected_polygon.lines.Count;

                        selected_polygon.lines[(lines_count + edge_index - 1) % lines_count].p2 = p1;
                        selected_polygon.lines[(edge_index + 1) % lines_count].p1 = p2;

                        selected_polygon.checkEdges((lines_count + edge_index - 1) % lines_count, 1);
                        selected_polygon.checkEdges((edge_index + 1) % lines_count, 2);

                        prev_location = e.Location;
                    }
                    else if (selected_polygon != null)
                    {
                        foreach (Line line in selected_polygon.lines)
                        {
                            line.p1 = new Point(line.p1.X + e.X - prev_location.X, line.p1.Y + e.Y - prev_location.Y);
                            line.p2 = new Point(line.p2.X + e.X - prev_location.X, line.p2.Y + e.Y - prev_location.Y);
                        }
                        prev_location = new Point(e.Location.X, e.Location.Y);
                    }
                }
                else if (selected) ;
                else findAndSelect(new Point(e.X, e.Y));

                g = drawAndInvalidate();
                g.Dispose();
            }
        }
        private void whiteboard_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                current_point = e.Location;
                Graphics g = drawAndInvalidate();

                // dodajemy nowy wierzcho³ek do aktualnego wielok¹ta
                if (!start_drawing)
                {
                    double r1 = e.Location.X - (current_polygon.start_point.X - r / 4);
                    double r2 = e.Location.Y - (current_polygon.start_point.Y - r / 4);
                    // sprawdzamy czy trafiliœmy do wiercho³ka pocz¹tkowego
                    if (r1 * r1 + r2 * r2 <= r * r)
                    {
                        if (current_polygon.lines.Count > 1)
                        {
                            current_polygon.lines.Add(new Line(current_polygon.last_point, current_polygon.start_point));
                            //g.DrawLine(pen, current_polygon.last_point, current_polygon.start_point);
                            Polygon.drawLine(g, pen, current_polygon.last_point, current_polygon.start_point);
                            start_drawing = true;
                            current_polygon.finished = true;
                        }
                    }
                    else
                    {
                        current_polygon.lines.Add(new Line(current_polygon.last_point, current_point));
                        current_polygon.last_point = current_point;
                    }
                }
                else // zaczynamy rysowanie nowego wielok¹ta
                {
                    start_drawing = false;
                    current_polygon = new Polygon();
                    current_polygon.start_point = current_point;
                    polygons.Add(current_polygon);
                    current_polygon.last_point = current_point;
                    current_polygon.points.Add(current_point);
                }
                current_polygon.points.Add(current_point);

                g.Dispose();

            }
            else // wybieramy wierzcho³ek/krawêdŸ/wielok¹t do przesuniêcia
            {
                if (e.Button == MouseButtons.Left)
                    selected = findAndSelect(new Point(e.X, e.Y));
            }
        }
        private void whiteboard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(drawing_bitmap, 0, 0);
        }
        private void drawing_button_Click(object sender, EventArgs e)
        {
            drawing = true;
            selecting = false;
            selected_vertex = null;
            selected_edge = null;
            selected_polygon = null;
        }
        private void select_button_Click(object sender, EventArgs e)
        {
            if (current_polygon != null && !current_polygon.finished)
            {
                polygons.Remove(current_polygon);
                start_drawing = true;
            }
            drawing = false;
            selecting = true;
            selected = false;
            selected_vertex = null;
            selected_edge = null;
            selected_polygon = null;
        }
        private void whiteboard_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selected = false;
                selected_edge = null;
                selected_vertex = null;
                selected_polygon = null;

                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
            else if (selected_vertex == null && selected_edge == null && selected_polygon != null && selected_polygon.offset_polygon == true)
            {
                slider.Value = selected_polygon.offset;
            }
        }
        private Line? findEdge(Point p)
        {
            Line edge = null;
            foreach (Polygon polygon in polygons)
            {
                foreach (Line line in polygon.lines)
                {
                    if (isPointNearLine(p, line, radius))
                    {
                        selected_polygon = polygon;
                        edge = line;
                        prev_location = p;
                        break;
                    }
                }
            }
            return edge;
        }
        private Line? findPoint(Point p)
        {
            Line edge = null;
            foreach (Polygon polygon in polygons)
            {
                foreach (Line line in polygon.lines)
                {
                    double distance = (line.p1.X - p.X) * (line.p1.X - p.X) + (line.p1.Y - p.Y) * (line.p1.Y - p.Y);
                    if (distance <= radius)
                    {
                        selected_polygon = polygon;
                        edge = line;
                        break;
                    }
                }
            }
            return edge;
        }
        private void drawPolygons(Graphics g)
        {
            foreach (Polygon polygon in polygons)
            {
                polygon.drawPolygon(g, pen);
            }
            if (selecting)
            {
                if (selected_vertex != null)
                {
                    Brush b = new SolidBrush(selected_edge.color);
                    g.FillEllipse(b, selected_edge.p1.X - r / 2, selected_edge.p1.Y - r / 2, r, r);
                    b.Dispose();
                }
                else if (selected_edge != null)
                {
                    pen.Color = Color.Red;
                    Polygon.drawLine(g, pen, selected_edge.p1, selected_edge.p2);
                    pen.Color = Color.Black;
                    edge_width_slider.Value = selected_edge.Width;
                }
                else if (selected_polygon != null)
                {
                    pen.Color = Color.Red;
                    selected_polygon.drawPolygon(g, pen);
                    pen.Color = Color.Black;
                }
            }
        }
        private Graphics drawAndInvalidate()
        {
            whiteboard.Invalidate();
            drawing_bitmap = new Bitmap(whiteboard.Width, whiteboard.Height);
            Polygon.bitmap = drawing_bitmap;

            Graphics g = Graphics.FromImage(drawing_bitmap);
            drawPolygons(g);

            whiteboard.Image = drawing_bitmap;

            return g;
        }
        private bool findAndSelect(Point p)
        {
            selected_vertex = null;
            selected_edge = null;
            selected_polygon = null;

            // szukamy wierzcho³ka
            Line line = findPoint(p);
            if (line != null)
            {
                selected_vertex = line.p1;
                selected_edge = line;

                vertex_index2 = selected_polygon.lines.FindIndex(line => (line.p1 == selected_edge.p1 && line.p2 == selected_edge.p2));
                vertex_index1 = (vertex_index2 - 1 + selected_polygon.lines.Count) % selected_polygon.lines.Count;
                return true;
            }
            // szukamy krawêdzi
            line = findEdge(p);
            if (line != null)
            {
                selected_vertex = null;
                selected_edge = line;

                edge_index = selected_polygon.lines.FindIndex(line => (line.p1 == selected_edge.p1 && line.p2 == selected_edge.p2));
                prev_location = p;
                return true;
            }
            // szukamy wielokata
            foreach (Polygon polygon in polygons)
            {
                if (polygon.isInsidePolygon(p))
                {
                    selected_polygon = polygon;
                    prev_location = p;
                    return true;
                }
            }
            return false;
        }
        private void remove_vertex_button_Click(object sender, EventArgs e)
        {
            if (selected_vertex != null)
            {
                int index1 = selected_polygon.lines.FindIndex(line => line.p2 == selected_vertex);
                int index2 = (index1 + 1) % selected_polygon.lines.Count;
                int index3 = (index2 + 1) % selected_polygon.lines.Count;
                selected_polygon.lines[index1].p2 = selected_polygon.lines[index2].p2;
                selected_polygon.lines[index3].p1 = selected_polygon.lines[index2].p2;

                selected_polygon.lines[index1].vertical = selected_polygon.lines[index1].horizontal =
                    selected_polygon.lines[index3].vertical = selected_polygon.lines[index3].horizontal = false;

                selected_polygon.lines.RemoveAt(index2);
                selected_polygon = null;
                selected_vertex = null;
                selected_edge = null;
                selected = false;

                drawAndInvalidate();
            }
        }
        private void add_vertex_button_Click(object sender, EventArgs e)
        {
            if (selected_vertex == null && selected_edge != null)
            {
                int index1 = selected_polygon.lines.FindIndex(line => line == selected_edge);
                int index2 = (index1 + 1) % selected_polygon.lines.Count;

                Point p1 = selected_polygon.lines[index1].p1;
                Point p2 = selected_polygon.lines[index1].p2;

                //Line line1 = new Line(p1, new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));
                Line line2 = new Line(new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2), p2);

                selected_polygon.lines[index1].p2 = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                selected_polygon.lines[index1].vertical = false;
                selected_polygon.lines[index1].horizontal = false;
                selected_polygon.lines.Insert(index2, line2);

                selected_polygon = null;
                selected_edge = null;
                selected = false;

                drawAndInvalidate();
            }
        }
        private bool isPointNearLine(Point p, Line line, float d)
        {
            double pX = line.p2.X - line.p1.X;
            double pY = line.p2.Y - line.p1.Y;
            double t = ((p.X - line.p1.X) * pX + (p.Y - line.p1.Y) * pY) / ((pX * pX) + (pY * pY));

            if (t > 1) t = 1;
            else if (t < 0) t = 0;

            double dX = line.p1.X + t * pX - p.X;
            double dY = line.p1.Y + t * pY - p.Y;
            return Math.Sqrt(dX * dX + dY * dY) <= d;
        }
        private void horizontal_button_Click(object sender, EventArgs e)
        {
            if (selected_vertex == null && selected_edge != null)
            {
                int lines_count = selected_polygon.lines.Count;
                int index = selected_polygon.lines.FindIndex(line => line.p1 == selected_edge.p1 && line.p2 == selected_edge.p2);
                int prev_index = (index - 1 + lines_count) % lines_count;
                int next_index = (index + 1) % lines_count;

                if (selected_polygon.lines[prev_index].horizontal == true || selected_polygon.lines[next_index].horizontal == true)
                {
                    MessageBox.Show("Two adjacent lines cannot be horizontal.");
                    return;
                }

                int y = (selected_edge.p1.Y + selected_edge.p2.Y) / 2;
                int x1 = selected_edge.p1.X;
                int x2 = selected_edge.p2.X;

                selected_polygon.lines[index].horizontal = true;
                selected_polygon.lines[index].p1 = new Point(x1, y);
                selected_polygon.lines[index].p2 = new Point(x2, y);
                selected_polygon.lines[prev_index].p2 = new Point(x1, y);
                selected_polygon.lines[next_index].p1 = new Point(x2, y);

                selected = false;
                selected_edge = null;
                selected_polygon = null;
                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
        }
        private void vertical_button_Click(object sender, EventArgs e)
        {
            if (selected_vertex == null && selected_edge != null)
            {
                int lines_count = selected_polygon.lines.Count;
                int index = selected_polygon.lines.FindIndex(line => line.p1 == selected_edge.p1 && line.p2 == selected_edge.p2);
                int prev_index = (index - 1 + lines_count) % lines_count;
                int next_index = (index + 1) % lines_count;

                if (selected_polygon.lines[prev_index].vertical == true || selected_polygon.lines[next_index].vertical == true)
                {
                    MessageBox.Show("Two adjacent lines cannot be vertical.");
                    return;
                }

                int x = (selected_edge.p1.X + selected_edge.p2.X) / 2;
                int y1 = selected_edge.p1.Y;
                int y2 = selected_edge.p2.Y;

                selected_polygon.lines[index].vertical = true;
                selected_polygon.lines[index].p1 = new Point(x, y1);
                selected_polygon.lines[index].p2 = new Point(x, y2);
                selected_polygon.lines[prev_index].p2 = new Point(x, y1);
                selected_polygon.lines[next_index].p1 = new Point(x, y2);

                selected = false;
                selected_edge = null;
                selected_polygon = null;

                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
        }
        private void remove_h_button_Click(object sender, EventArgs e)
        {
            if (selected_edge != null && selected_edge.horizontal)
            {
                selected_edge.horizontal = false;
                selected_edge = null;
                selected_vertex = null;
                selected = false;
                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
        }
        private void remove_v_button_Click(object sender, EventArgs e)
        {
            if (selected_edge != null && selected_edge.vertical)
            {
                selected_edge.vertical = false;
                selected_edge = null;
                selected_vertex = null;
                selected = false;
                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
        }
        private void offset_button_Click(object sender, EventArgs e)
        {
            if (selected_polygon != null)
            {
                selected_polygon.offset_polygon = !selected_polygon.offset_polygon;
                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
        }
        private void slider_ValueChanged(object sender, EventArgs e)
        {
            if (selected_polygon != null)
            {
                selected_polygon.offset = slider.Value;
                Graphics g = drawAndInvalidate();
                g.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pen.Dispose();
            thickPen.Dispose();
            foreach (Polygon polygon in polygons)
                polygon.remove();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Scene.addPolygons(ref polygons);
            drawing_bitmap = new Bitmap(whiteboard.Width, whiteboard.Height);
            drawAndInvalidate();
        }

        private void draw_library_button_CheckedChanged(object sender, EventArgs e)
        {
            Polygon.bresenham = false;
            Polygon.multi = false;
            drawAndInvalidate();
        }

        private void draw_bresenham_button_CheckedChanged(object sender, EventArgs e)
        {
            Polygon.bresenham = true;
            Polygon.multi = false;
            Polygon.symmetric = false;
            drawAndInvalidate();
        }

        private void draw__CheckedChanged(object sender, EventArgs e)
        {
            Polygon.bresenham = false;
            Polygon.multi = false;
            Polygon.symmetric = true;
            drawAndInvalidate();
        }

        private void edge_width_slider_ValueChanged(object sender, EventArgs e)
        {
            if (selected_vertex == null && selected_edge != null)
            {
                selected_edge.Width = edge_width_slider.Value;
                drawAndInvalidate();
            }
        }
    }
}