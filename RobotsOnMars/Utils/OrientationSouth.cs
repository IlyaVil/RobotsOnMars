using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    sealed class OrientationSouth : Orientation
    {
        public override char Code => MagicProvider.SouthCode;
        public override Vector Vector => new Vector(0, -1);
    }
}
