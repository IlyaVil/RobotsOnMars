using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    class Rectangle
    {
        public int XMin;
        public int XMax;
        public int YMin;
        public int YMax;

        public Rectangle(Point left_bottom, Point right_top)
        {
            Initialize(left_bottom.X, left_bottom.Y, right_top.X, right_top.Y);
        }


        public Rectangle(int x1, int y1, int x2, int y2)
        {
            Initialize(x1, y1, x2, y2);
        }

        private void Initialize(int x1, int y1, int x2, int y2)
        {
            XMin = x1 < x2 ? x1 : x2;
            XMax = x2 >= x1 ? x2 : x1;
            YMin = y1 < y2 ? y1 : y2;
            YMax = y2 >= y1 ? y2 : y1;
        }

        public bool InBounds(Point point)
        {
            return point.X >= XMin && point.X <= XMax && point.Y >= YMin && point.Y <= YMax;
        }
    }
}
