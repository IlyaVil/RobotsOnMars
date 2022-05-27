using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars.Utils
{
    sealed class OrientationWest : Orientation
    {
        public override char Code => MagicProvider.WestCode;
        public override Vector Vector => new Vector(-1, 0);
    }
}
