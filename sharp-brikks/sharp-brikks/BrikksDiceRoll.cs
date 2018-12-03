using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBrikks
{
    public class BrikksDiceRoll
    {
        readonly DieSide D4;
        readonly DieSide D6;

        public BrikksDiceRoll(int d6, int d4)
        {
            D4 = new DieSide(d4);
            D6 = new DieSide(d6);
        }
    }
}
