using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    using Utils;

    class Position
    {
        public Point Point;
        public Orientation Orientation;

        public Position(Point point, Orientation orientation)
        {
            Initialize(point, orientation);
        }

        public Position(Position position)
        {
            Initialize(position.Point, position.Orientation);
        }

        private void Initialize(Point point, Orientation orientation)
        {
            Point = new Point(point);
            Orientation = orientation;
        }

        public static bool operator == (Position first, Position second)
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

        public static bool operator !=(Position first, Position second)
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

            var item = obj as Position;

            if (item is null)
            {
                return false;
            }

            return FieldsEquality(this, item);
        }

        private static bool FieldsEquality(Position first, Position second)
        {
            return first.Point == second.Point && first.Orientation == second.Orientation;
        }

        public override int GetHashCode()
        {
            var hashCode = 1913679258;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(Point);
            hashCode = hashCode * -1521134295 + EqualityComparer<Orientation>.Default.GetHashCode(Orientation);
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Point.ToString(), Orientation.ToString());
        }
    }
}
