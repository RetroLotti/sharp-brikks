using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpbrikks
{
    /// <summary>
    /// This class contains everything you need for your day-to-day life in Brikks
    /// </summary>
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

        /// <summary>
        /// Loads 1000 new D6 rolls into the queue. In case your IP ran out of random.org 
        /// quota we just create 50 new rolls using .NET random.
        /// </summary>
        private void LoadNewD6Rolls()
        {
            List<int> rolls = BrikksRandomNumberGenerator.GenerateIntegers(1000, 1, 6);

            // fallback to good old .net RNG
            if (rolls.Count == 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    rolls.Add(ThreadSafeRandom.ThisThreadsRandom.Next(1, 6));
                }
            }

            foreach (var item in rolls)
            {
                D6Rolls.Enqueue(item);
            }
        }

        /// <summary>
        /// Loads 1000 new D4 rolls into the queue. In case your IP ran out of random.org 
        /// quota we just create 50 new rolls using .NET random.
        /// </summary>
        private void LoadNewD4Rolls()
        {
            List<int> rolls = BrikksRandomNumberGenerator.GenerateIntegers(1000, 1, 4);

            // fallback to good old .net RNG
            if (rolls.Count == 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    rolls.Add(ThreadSafeRandom.ThisThreadsRandom.Next(1, 4));
                }
            }

            foreach (var item in rolls)
            {
                D4Rolls.Enqueue(item);
            }
        }

        /// <summary>
        /// Returns a new dice roll this includes a D4 and a D6 true random roll.
        /// If no more rolls are available we just get more!
        /// </summary>
        /// <returns><seealso cref="BrikksDiceRoll"/></returns>
        public BrikksDiceRoll Roll()
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
