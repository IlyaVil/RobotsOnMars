using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars
{
    using Utils;

    class RobotManageCenter
    {
        private Rectangle _field;
        private Scent _scent;       

        public RobotManageCenter(int lengthX, int lengthY)
        {
            _field = new Rectangle(0, 0, lengthX, lengthY);
            _scent = new Scent();
         }


        public RobotResult ProcessRobot(Robot robot, IList<RobotCommand> commands)
        {
            var result = new RobotResult() { Position = new Position(robot.Position), IsLost = false };

            for (int i = 0; (i < commands.Count) && !result.IsLost; i++)
            {
                switch (commands[i])
                {
                    case RobotCommand.Move:
                        if (ProcessMove(robot))
                        {
                            result.Position = new Position(robot.Position);
                        }
                        else
                        {
                            result.IsLost = true;
                        }
                        
                        break;

                    case RobotCommand.TurnLeft:
                        robot.TurnLeft();
                        result.Position = new Position(robot.Position);
                        break;

                    case RobotCommand.TurnRight:
                        robot.TurnRight();
                        result.Position = new Position(robot.Position);
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        private bool ProcessMove(Robot robot)
        {
            if (_scent.IsDanger(robot.Position))
            {
                return true;
            }

            var oldPosition = new Position(robot.Position);
            robot.Move();

            bool result = true;
            if (_field.InBounds(robot.Position.Point))
            {
                result = true;
            }
            else
            {
                _scent.AddExperience(oldPosition);
                result = false;
            }

            return result;
        }
    }
}
