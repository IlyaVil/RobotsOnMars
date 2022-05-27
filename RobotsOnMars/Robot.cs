using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars
{
    using Utils;

    class Robot
    {
        public Position Position;

        public Robot(Position position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return Position.ToString();
        }

        public void Move()
        {
            Position.Point = Position.Point + Position.Orientation.Vector;
        }

        public void TurnLeft()
        {
            Position.Orientation = Position.Orientation.TurnLeft(Position.Orientation);
        }

        public void TurnRight()
        {
            Position.Orientation = Position.Orientation.TurnRight(Position.Orientation);
        }
    }
}
