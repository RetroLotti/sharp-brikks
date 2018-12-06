using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    /// <summary>
    /// This class is a side with an according side type to represent a single side of a die.
    /// </summary>
    public class DieSide
    {
        public SideType SideType { get; set; }
        public Side Side { get; set; }

        public DieSide(int side)
        {
            this.SideType = SideType.none;
            this.Side = (Side)side;
        }
        public DieSide(SideType sideType, Side side)
        {
            this.SideType = sideType;
            this.Side = side;
        }
    }

    public enum SideType
    {
        none = 0,
        number = 1,
        text = 2,
        color = 3
    }

    /// <summary>
    /// Representation of all possible die sides.
    /// If more numeric sides are needed the code has to be changed in order to reflect colors starting with a higher number
    /// </summary>
    public enum Side
    {
        zero = 0,
        one = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5,
        six = 6,
        seven = 7,
        eight = 8,
        nine = 9,
        ten = 10,
        eleven = 11,
        twelve = 12,
        thirteen = 13,
        fourteen = 14,
        fiveteen = 15,
        sixteen = 16,
        seventeen = 17,
        eightteen = 18,
        nightteen = 19,
        twenty = 20,
        red = 21,
        blue = 22,
        black = 23,
        white = 24,
        yellow = 25,
        green = 26,
        orange = 27
    }
}
