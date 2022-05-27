using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobotsOnMars
{
    using Utils;

    class MarsFileReader : IDisposable
    {
        private string _filename;
        private StreamReader _reader;

        public MarsFileReader(string filename)
        {
            _filename = filename;
            _reader = new StreamReader(_filename);
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
        }

        public bool EndOfData
        {
            get
            {
                return _reader.EndOfStream;
            }
        }

        public Point ReadMarsSize()
        {
            var line = ReadNonEmptyLine();
            var splits = line.Split(new char[] {' '}, 2);

            var result = new Point(int.Parse(splits[0]), int.Parse(splits[1]));

            return result;
        }

        public Position ReadPosition()
        {
            var line = ReadNonEmptyLine();
            var splits = line.Split(new char[] {' '}, 3);

            var point = new Point(int.Parse(splits[0]), int.Parse(splits[1]));
            var orient = Orientation.GetByCode(splits[2][0]);

            return new Position(point, orient);
        }

        public IList<RobotCommand> ReadCommands()
        {
            var result = new List<RobotCommand>();
            var line = ReadNonEmptyLine().ToUpper();

            for (int i = 0; i < line.Length; i++)
            {
                RobotCommand com;
                switch (line[i])
                {
                    case 'F':
                        com = RobotCommand.Move;
                        break;

                    case 'L':
                        com = RobotCommand.TurnLeft;
                        break;

                    case 'R':
                        com = RobotCommand.TurnRight;
                        break;

                    default:
                        continue;
                }

                result.Add(com);
            }

            return result;
        }

        private string ReadNonEmptyLine()
        {
            string line = "";
            while (!_reader.EndOfStream)
            {
                line = _reader.ReadLine();
                if (line.Trim() != "")
                {
                    break;
                }
            }

            return line;
        }

    }
}
