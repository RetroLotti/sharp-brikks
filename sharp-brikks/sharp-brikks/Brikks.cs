using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    public class Brikks
    {
        private Queue<int> D6Rolls { get; set; }
        private Queue<int> D4Rolls { get; set; }
        private BrikksRandom BrikksRandomNumberGenerator { get; set; }

        public Brikks()
        {
            D6Rolls = new Queue<int>();
            D4Rolls = new Queue<int>();
            BrikksRandomNumberGenerator = new BrikksRandom();
        }

        private void LoadNewD6Rolls()
        {
            List<int> rolls = BrikksRandomNumberGenerator.GenerateIntegers(1000, 1, 6);
            foreach (var item in rolls)
            {
                D6Rolls.Enqueue(item);
            }
        }

        private void LoadNewD4Rolls()
        {
            List<int> rolls = BrikksRandomNumberGenerator.GenerateIntegers(1000, 1, 4);
            foreach (var item in rolls)
            {
                D4Rolls.Enqueue(item);
            }
        }

        public BrikksDiceRoll GetRoll()
        {
            if(D6Rolls.Count == 0)
            {
                LoadNewD6Rolls();
            }
            if(D4Rolls.Count == 0)
            {
                LoadNewD4Rolls();
            }

            return new BrikksDiceRoll(D6Rolls.Dequeue(), D4Rolls.Dequeue());
        }
    }
}
