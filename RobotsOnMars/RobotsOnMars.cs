using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobotsOnMars
{
    public class RobotsOnMars
    {
        private string _inputFilename;
        private string _outputFilename;
        private long _memoryAllocated = 0;


        public RobotsOnMars(string input, string output)
        {
            _inputFilename = input;
            _outputFilename = output;
        }

        public void Run()
        {
            try
            {
                using (var reader = new MarsFileReader(_inputFilename))
                    using (var writer = new StreamWriter(_outputFilename))
                {
                    var fieldSize = reader.ReadMarsSize();
                    var manageCenter = new RobotManageCenter(fieldSize.X, fieldSize.Y);

                    while (!reader.EndOfData)
                    {
                        var pos = reader.ReadPosition();
                        var commands = reader.ReadCommands();

                        var result = manageCenter.ProcessRobot(new Robot(pos), commands);
                        writer.WriteLine(result);
                    }

                    //this one for memory test
                    _memoryAllocated = GC.GetTotalMemory(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        //this one for memory test
        public long GetAllocatedMemory
        {
            get => _memoryAllocated;
        }
    }
}
