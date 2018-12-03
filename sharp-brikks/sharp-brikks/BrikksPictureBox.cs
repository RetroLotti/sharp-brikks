using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpBrikks
{
    public class BrikksPictureBox : PictureBox
    {
        [Description("Indicates that this block can no longer be changed."), Category("Brikks")]
        public bool IsFixed { get; set; }
        [Description("Did the player mark this field?"), Category("Brikks")]
        public bool IsMarked { get; set; }
        [Description("The original front image."), Category("Brikks")]
        public Image OriginalImage { get; set; }
    }
}
