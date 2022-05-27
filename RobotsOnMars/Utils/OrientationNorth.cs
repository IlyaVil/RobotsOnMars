using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    sealed class OrientationNorth : Orientation
    {
        public override char Code => MagicProvider.NorthCode;
        public override Vector Vector => new Vector(0, 1);
    }
}
