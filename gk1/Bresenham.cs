using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk1
{
    public static class Bresenham
    {
        static Bitmap bitmap;
        public static void drawBresenham(Graphics g,Pen pen, Point p1, Point p2,Bitmap drawing_bitmap)
        {
            bitmap = drawing_bitmap;
            if (Math.Abs(p1.X - p2.X) > Math.Abs(p1.Y - p2.Y))
                drawBresenhamX(g, pen, p1, p2);
            else drawBresenhamY(g, pen, p1, p2);
        }
        public static void drawBresenhamX(Graphics g, Pen pen, Point p1, Point p2)
        {
            int r = (int)pen.Width;
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);

            int x = p1.X < p2.X ? p1.X : p2.X;
            int y = x == p1.X ? p1.Y : p2.Y;

            int incrY = 0;
            if(y==p1.Y) incrY = p2.Y > p1.Y ? 1 : -1;
            if(y==p2.Y) incrY = p2.Y < p1.Y ? 1 : -1;
            int incrX = 1;

            int end = p1.X > p2.X ? p1.X : p2.X;

            int err = 2 * dy - dx;
            int incrE = 2 * dy;
            int incrNE = 2 * (dy - dx);

            setPixels(x, y, pen, 1, r);

            while (x<end)
            {
                if (err<0)
                {
                    err += incrE;
                    x+=incrX;
                }
                else 
                {
                    err += incrNE;
                    x+=incrX;
                    y+=incrY;
                }
                setPixels(x, y, pen, 1, r);
            }
        }
        public static void drawBresenhamY(Graphics g, Pen pen, Point p1, Point p2)
        {
            int r = (int)pen.Width;
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);

            int y = p1.Y < p2.Y ? p1.Y : p2.Y;
            int x = y == p1.Y ? p1.X : p2.X;

            int incrX = 0;
            if (x == p1.X) incrX = p2.X > p1.X ? 1 : -1;
            if (x == p2.X) incrX = p2.X < p1.X ? 1 : -1;
            int incrY = 1;

            int end = p1.Y > p2.Y ? p1.Y : p2.Y;

            int err = 2 * dx - dy;
            int incrE = 2 * dx;
            int incrNE = 2 * (dx - dy);

            setPixels(x, y, pen, r, 1);

            while (y < end)
            {
                if (err < 0)
                {
                    err += incrE;
                    y += incrY;
                }
                else
                {
                    err += incrNE;
                    x += incrX;
                    y += incrY;
                }
                setPixels(x, y, pen, r, 1);
            }
        }
        public static void setPixels(int x, int y, Pen pen,int ri, int rj)
        {
            for (int i = -ri/2; i <= ri/2; i++)
            {
                for (int j = -rj/2; j <= rj/2; j++)
                {
                    if (x + i < bitmap.Width && y + j < bitmap.Height && x + i > 0 && y + j > 0)
                        bitmap.SetPixel(x + i, y + j, pen.Color);
                }
            }
        }
    }
}
