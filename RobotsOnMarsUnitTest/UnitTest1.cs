using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RobotsOnMarsUnitTest
{
    using RobotsOnMars;
    using RobotsOnMars.Utils;
    using System.Collections.Generic;
    using System.Diagnostics;

    [TestClass]
    public class TestRobots
    {
        private static readonly string _input = MagicProvider.DefaultInputFile;
        private static readonly string _output = MagicProvider.DefaultOutputFile;

        private static readonly string _sampleInput = @"5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
";
        private static readonly string _sampleOutput = @"1 1 E
3 3 N LOST
2 3 S
";

        public TestRobots()
        {

        }

        [TestMethod]
        //Check simple commands work correcty
        public void SimpleMoves()
        {
            var robots = new List<(int, int, char, string, int, int, char, bool)>()
            {
                (2, 2, 'N', "F", 2, 3, 'N', false),
                (2, 2, 'E', "F", 3, 2, 'E', false),
                (2, 2, 'S', "F", 2, 1, 'S', false),
                (2, 2, 'W', "F", 1, 2, 'W', false),

                (2, 2, 'N', "L", 2, 2, 'W', false),
                (2, 2, 'N', "R", 2, 2, 'E', false),

                (2, 2, 'E', "L", 2, 2, 'N', false),
                (2, 2, 'E', "R", 2, 2, 'S', false),

                (2, 2, 'S', "L", 2, 2, 'E', false),
                (2, 2, 'S', "R", 2, 2, 'W', false),

                (2, 2, 'W', "L", 2, 2, 'S', false),
                (2, 2, 'W', "R", 2, 2, 'N', false),
            };

            TestSolution(5, 5, robots);
        }

        [TestMethod]
        //check if move and robot doesn't break the next one
        public void SequenceMoves()
        {
            var robots = new List<(int, int, char, string, int, int, char, bool)>()
            {
                (2, 2, 'N', "FFRFFL", 4, 4, 'N', false),
                (0, 0, 'E', "FLFRFLFRFLFR", 3, 3, 'E', false),
            };

            TestSolution(5, 5, robots);
        }

        [TestMethod]
        //borders walk and return to the same point
        public void Borders()
        {
            int x = 10;
            int y = 5;
            var robots = new List<(int, int, char, string, int, int, char, bool)>()
            {
                (0, 0, 'N', new string('F', y) + "R" + new string('F', x) + "R" + new string('F', y) + "R" + new string('F', x), 0, 0, 'W', false),
            };

            TestSolution(x, y, robots);
        }

        [TestMethod]
        //pass through all points in field
        public void FillField()
        {
            int x = 10;
            int y = 5;
            char turn = 'R';
            string command = "";
            for (int i = 0; i <= x; i++)
            {
                command += new string('F', y);
                if (i < x)
                {
                    command += turn.ToString() + "F" + turn.ToString();
                }
                //command += new string(turn, 2);
                turn = turn == 'R' ? 'L' : 'R';
            }

            var robots = new List<(int, int, char, string, int, int, char, bool)>()
            {
                (0, 0, 'N', command, x, (x % 2) == 0 ? y : 0, ((x + 1) % 2) == 0 ? 'S' : 'N', false),
            };

            TestSolution(x, y, robots);
        }

        [TestMethod]
        //check if scent works correctly
        public void Lost()
        {

            var robots = new List<(int, int, char, string, int, int, char, bool)>()
            {
                (2, 0, 'S', "FFFFF", 2, 0, 'S', true),
                (2, 0, 'S', "FFFFF", 2, 0, 'S', false),
                (2, 0, 'N', "F", 2, 1, 'N', false),
                (2, 0, 'W', "F", 1, 0, 'W', false),
                (2, 0, 'E', "F", 3, 0, 'E', false),

                (2, 5, 'N', "FFFFF", 2, 5, 'N', true),
                (2, 5, 'N', "FFFFF", 2, 5, 'N', false),
                (2, 5, 'S', "F", 2, 4, 'S', false),
                (2, 5, 'W', "F", 1, 5, 'W', false),
                (2, 5, 'E', "F", 3, 5, 'E', false),

                (0, 2, 'W', "FFFFF", 0, 2, 'W', true),
                (0, 2, 'W', "FFFFF", 0, 2, 'W', false),
                (0, 2, 'E', "F", 1, 2, 'E', false),
                (0, 2, 'N', "F", 0, 3, 'N', false),
                (0, 2, 'S', "F", 0, 1, 'S', false),

                (5, 2, 'E', "FFFFF", 5, 2, 'E', true),
                (5, 2, 'E', "FFFFF", 5, 2, 'E', false),
                (5, 2, 'W', "F", 4, 2, 'W', false),
                (5, 2, 'N', "F", 5, 3, 'N', false),
                (5, 2, 'S', "F", 5, 1, 'S', false),

            };

            TestSolution(5, 5, robots);
        }

        [TestMethod]
        //check scent in the corners
        public void CornerLost()
        {
            var robots = new List<(int, int, char, string, int, int, char, bool)>()
            {
                (0, 0, 'S', "FFFFF", 0, 0, 'S', true),
                (0, 0, 'W', "FFFFF", 0, 0, 'W', true),

                (0, 5, 'N', "FFFFF", 0, 5, 'N', true),
                (0, 5, 'W', "FFFFF", 0, 5, 'W', true),

                (5, 0, 'S', "FFFFF", 5, 0, 'S', true),
                (5, 0, 'E', "FFFFF", 5, 0, 'E', true),

                (5, 5, 'N', "FFFFF", 5, 5, 'N', true),
                (5, 5, 'E', "FFFFF", 5, 5, 'E', true),

            };

            TestSolution(5, 5, robots);
        }

        [TestMethod]
        //memory test:
        //    increasing fieldSize - increasing memory assumption for scent cells
        //    set maxMemory to memory limitation
        public void MemoryTest()
        {
            int fieldSize = 1000;
            long maxMemory = 100 * 1024 * 1024; //100MB

            using (var sw = new StreamWriter(_input))
            {
                sw.WriteLine(string.Format("{0} {1}", fieldSize, fieldSize));
                for (int i = 0; i <= fieldSize; i++)
                {
                    sw.Write(InputLine(i, 0, 'S', "F"));
                    sw.Write(InputLine(i, fieldSize, 'N', "F"));
                    sw.Write(InputLine(0, i, 'W', "F"));
                    sw.Write(InputLine(fieldSize, i, 'E', "F"));
                }
            }

            var program = new RobotsOnMars(_input, _output);
            program.Run();
            Assert.IsTrue(program.GetAllocatedMemory < maxMemory, "Used memory: {0}mb, allowed: {1}mb", program.GetAllocatedMemory / 1024 / 1024, maxMemory / 1024 / 1024);
        }

        [TestMethod]
        //Test speed of solution
        //   fieldSize - affects on search time in scent
        //   robots - count of robots
        //   commandsPerRobot - how much command each robot will process
        //   percentTurns - precent of turns commands (LR) from all commands (randomly).
        //   maxTime - limit of time
        public void SpeedTest()
        {
            int fieldSize = 100;
            int robots = 1000;
            int commandsPerRobot = 100;
            int percentTurns = 30;
            TimeSpan maxTime = TimeSpan.FromSeconds(1);

            var rnd = new Random();
            string orientations = "NESW";
            using (var sw = new StreamWriter(_input))
            {
                sw.WriteLine(string.Format("{0} {1}", fieldSize, fieldSize));

                for (int r = 0; r < robots; r++)
                {
                    string command = "";
                    for (int c = 0; c < commandsPerRobot; c++)
                    {
                        if (rnd.Next(100) > percentTurns)
                        {
                            command += "F";
                        }
                        else
                        {
                            command += rnd.Next(2) == 0 ? "L" : "R";                            
                        }
                    }

                    sw.Write(InputLine(rnd.Next(fieldSize + 1), rnd.Next(fieldSize + 1), orientations[rnd.Next(4)], command));
                }
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var program = new RobotsOnMars(_input, _output);
            program.Run();
            watch.Stop();

            Assert.IsTrue(watch.Elapsed < maxTime, "Duration too long: {0}s, allowed: {1}s", watch.Elapsed.TotalSeconds, maxTime.TotalSeconds);
        }

        [TestMethod]
        //sample from task
        public void TestSample()
        {
            PutInput(_sampleInput);
            var robots = new RobotsOnMars(_input, _output);
            robots.Run();
            CheckOutput(_sampleOutput);
        }

        private void PutInput(string text)
        {
            File.WriteAllText(_input, text);
        }

        private void CheckOutput(string answer)
        {
            var text = File.ReadAllText(_output).Trim();
            Assert.AreEqual<string>(answer.Trim(), text.Trim());
        }

        private string AnswerLine(int x, int y, char o, bool l)
        {
            return string.Format("{0} {1} {2}{3}\r\n", x, y, o, l ? " LOST" : "");
        }

        private string InputLine(int x, int y, char o, string cc)
        {
            return string.Format("{0} {1} {2}\r\n{3}\r\n", x, y, o, cc);
        }

        private void TestSolution(int x, int y, IList<(int, int, char, string, int, int, char, bool)> robots)
        {
            string inputStr = string.Format("{0} {1}\r\n", x, y);
            string answerStr = "";
            for (int i = 0; i < robots.Count; i++)
            {
                var item = robots[i];
                inputStr += InputLine(item.Item1, item.Item2, item.Item3, item.Item4);
                answerStr += AnswerLine(item.Item5, item.Item6, item.Item7, item.Item8);
            }

            File.WriteAllText(_input, inputStr);
            var program = new RobotsOnMars(_input, _output);
            program.Run();
            string outputStr = File.ReadAllText(_output);

            Assert.AreEqual<string>(answerStr.Trim(), outputStr.Trim());
        }
    }
}
