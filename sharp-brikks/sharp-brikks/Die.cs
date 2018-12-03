using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpBrikks
{
    public class Die
    {
        List<DieSide> Sides { get; set; }

        public Die(params DieSide[] sides)
        {
            this.Sides = new List<DieSide>();
            foreach (var item in sides)
            {
                this.Sides.Add(item);
            }
        }

        public DieSide Roll(BrikksRandom rng)
        {
            return this.Sides.GetRange(rng.GenerateSingleInteger(0, this.Sides.Count - 1), 1)[0];
        }
    }
}
