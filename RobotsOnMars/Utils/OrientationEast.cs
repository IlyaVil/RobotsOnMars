using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    sealed class OrientationEast : Orientation
    {
        public override char Code => MagicProvider.EastCode;
        public override Vector Vector => new Vector(1, 0);
    }
}
