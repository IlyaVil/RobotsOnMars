using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    class Point
    {
        public int X = 0;
        public int Y = 0;

        public Point()
        {

        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point src)
        {
            this.X = src.X;
            this.Y = src.Y;
        }

        public static bool operator == (Point first, Point second)
        {
            if (first is null || second is null)
            {
                return false;
            }

            if (ReferenceEquals(first, second))
            {
                return true;
            }

            return FieldsEquality(first, second);
        }


        public static bool operator !=(Point first, Point second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var point2 = obj as Point;

            if (point2 is null)
            {
                return false;
            }

            return FieldsEquality(this, point2);
        }

        private static bool FieldsEquality(Point first, Point second)
        {
            return (first.X == second.X) && (first.Y == second.Y);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public static Point operator -(Point item)
        {
            return new Point(-item.X, -item.Y);
        }
    }
}
