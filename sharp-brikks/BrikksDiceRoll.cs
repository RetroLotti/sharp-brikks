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
        public DieSide D4 { get; private set; }
        public DieSide D6 { get; private set; }

        public BrikksDiceRoll(int d6, int d4)
        {
            D4 = new DieSide(d4);
            D6 = new DieSide(d6+20);
        }
    }
}
