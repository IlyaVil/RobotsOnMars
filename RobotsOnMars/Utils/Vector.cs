using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    class Vector : Point
    {

        public Vector(int x, int y)
            : base(x, y)
        {

        }

        public Vector(Vector src)
        {
            X = src.X;
            Y = src.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var vector2 = obj as Vector;

            if (vector2 == null)
            {
                return false;
            }

            return (this.X == vector2.X) && (this.Y == vector2.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Vector operator -(Vector item)
        {
            return new Vector(-item.X, -item.Y);
        }

        public static Point operator + (Point first, Vector second)
        {
            return new Point(first.X + second.X, first.Y + second.Y);
        }

        public static Point operator - (Point first, Vector second)
        {
            return new Point(first.X - second.X, first.Y - second.Y);
        }

        public static Vector operator +(Vector first, Vector second)
        {
            return new Vector(first.X + second.X, first.Y + second.Y);
        }

        public static Vector operator -(Vector first, Vector second)
        {
            return new Vector(first.X - second.X, first.Y - second.Y);
        }

        public static Vector operator *(Vector first, int second)
        {
            if (second == 1)
            {
                return new Vector(first);
            }

            return new Vector(first.X * second, first.Y * second);
        }

        public static Vector operator *(int first, Vector second) 
        {
            return second * first;
        }
    }
}
