using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Death : Enemy
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="location">Lokalizacja potwora</param> 
        /// <param name="numberOfHealths">ilość życia potwora</param>
        /// <param name="speed">szybkość potwora</param>
        /// <param name="attack">siła ataku potwora</param>
        public Death(Point location, int numberOfHealths, int speed, int attack)
            : base(location, numberOfHealths, speed, attack)
        {

        }

        /// <summary>
        /// Metoda Move potwora
        /// </summary>
        /// <param name="boundaries">obszar, po którym się poruszamy</param>
        public override void Move(Rectangle boundaries)
        {
            int temp = RandomUtility.MyRandom.Next(1, 5); //losujemy kierunek ruchu(przenieś do klasy bazowej)
            if (temp == 1)
	        {
		        base.location = Move(Direction.Top, boundaries);
	        }
            if (temp == 2)
            {
                base.location = Move(Direction.Bottom, boundaries);
            }
            if (temp == 3)
            {
                base.location = Move(Direction.Left, boundaries);
            }
            if (temp == 4)
            {
                base.location = Move(Direction.Right, boundaries);
            }
        }
    }
}
