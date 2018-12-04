using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    public class BrikksResultPictureBox : BrikksPictureBox
    {
        [DefaultValue(null)]
        public Side D6Result { get; set; }

        [DefaultValue(null)]
        public Side D4Result { get; set; }
    }
}
