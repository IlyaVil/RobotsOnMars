using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsOnMars
{
    using Utils;

    class Scent
    {
        private IDictionary<Position, byte> _lostMoves;

        public Scent()
        {
            _lostMoves = new Dictionary<Position, byte>();
        }

        public bool IsDanger(Position pos)
        {
            return _lostMoves.ContainsKey(pos);
        }

        public void AddExperience(Position pos)
        {
            _lostMoves[pos] = 1;
        }
    }
}
