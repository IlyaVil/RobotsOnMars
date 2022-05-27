using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars
{
    using Utils;

    class Program
    {
        static void Main(string[] args)
        {
            string infile = "";
            string outfile = "";

            if (args.Count() == 0)
            {
                infile = MagicProvider.DefaultInputFile;
                outfile = MagicProvider.DefaultOutputFile;
            }
            else if (args.Count() == 2)
            {
                infile = args[0];
                outfile = args[1];
            }
            else
            {
                Console.WriteLine("Wrong parameters. Usage: RobotsOnMars.exe [<input_file>] [<output_file>]");
                return;
            }

            var mars = new RobotsOnMars(infile, outfile);
            mars.Run();
        }
    }
}
