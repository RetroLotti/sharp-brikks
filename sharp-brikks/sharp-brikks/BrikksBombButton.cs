using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBrikks
{
    public class BrikksBombButton : Button
    {
        public int BombValue { get; set; }
        public bool BombUsed { get; set; }
    }
}
