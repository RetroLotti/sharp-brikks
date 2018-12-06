using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    /// <summary>
    /// This is a Brikks dice roll.
    /// Consisting of a D4 side and a D6 side.
    /// </summary>
    public class BrikksDiceRoll
    {
        //public DieSide D4 { get; private set; }
        //public DieSide D6 { get; private set; }

        public Side D4 { get; private set; }
        public Side D6 { get; private set; }

        public BrikksDiceRoll(int d6, int d4)
        {
            D4 = (Side)d4;
            D6 = (Side)(d6+20);
        }
    }
}
