using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars
{
    using Utils;

    class RobotResult
    {
        public Position Position;
        public bool IsLost;

        public override string ToString()
        {
            return string.Format("{0} {1} {2}{3}", Position.Point.X, Position.Point.Y, Position.Orientation.Code, IsLost ? " LOST" : "");
        }

        public static bool operator == (RobotResult first, RobotResult second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            return first.Position == second.Position && first.IsLost == second.IsLost;
        }

        public static bool operator != (RobotResult first, RobotResult second)
        {
            return !(first == second);
        }
    }
}
