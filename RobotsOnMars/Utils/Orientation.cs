using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    abstract class Orientation
    {
        private static List<Orientation> _order = new List<Orientation> { new OrientationNorth(), new OrientationEast(), new OrientationSouth(), new OrientationWest() };

        public virtual char Code { get; }
        public virtual Vector Vector { get; }

        public static readonly Orientation North = _order[0];
        public static readonly Orientation East = _order[1];
        public static readonly Orientation South = _order[2];
        public static readonly Orientation West = _order[3];


        public static bool operator == (Orientation first, Orientation second)
        {
            if (first is null || second is null)
            {
                return false;
            }

            if (ReferenceEquals(first, second))
            {
                return true;
            }

            return first.Code == second.Code;
        }

        public static bool operator !=(Orientation first, Orientation second)
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

            var item = obj as Orientation;

            if (item is null)
            {
                return false;
            }


            return this.Code == item.Code;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return Code.ToString();
        }

        public Point Move(Point src, int steps)
        {
            return src + steps * Vector;
        }

        public Point Move(Point src)
        {
            return Move(src, 1);
        }

        public Orientation TurnLeft(Orientation src)
        {
            var newIndex = (IndexOf(src) + _order.Count - 1) % _order.Count;

            return _order[newIndex];
        }

        public Orientation TurnRight(Orientation src)
        {
            var newIndex = (IndexOf(src) + 1) % _order.Count;

            return _order[newIndex];
        }


        private static int IndexOf(Orientation src)
        {
            var result = _order.IndexOf(src);
            if (result == -1)
            {
                throw new Exception("Orientation not found");
            }

            return result;
        }

        public static Orientation GetByCode(char code)
        {
            return _order.First(x => x.Code == code);
        }

    }
}
