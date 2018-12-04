using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    public class BrikksResultPictureBox : BrikksPictureBox
    {
        private BrikksDiceRoll diceRoll;

        public BrikksDiceRoll DiceRoll
        {
            get
            {
                return this.diceRoll;
            }

            set
            {
                if (value != this.diceRoll)
                {
                    this.diceRoll = value;
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject($"{this.diceRoll.D6.Side.ToString()}_{this.diceRoll.D4.Side.ToString()}");
                }
            }
        }
    }
}
