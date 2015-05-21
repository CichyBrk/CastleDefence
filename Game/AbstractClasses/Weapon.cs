using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Weapon
    {
        /// <summary>
        /// Cena przedmiotu
        /// </summary>
        int price;
        public int Price {
            get { return price; }
            private set { price = value; }
        }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="price"></param>
        public Weapon(int price)
        {
            Price = price;
        }

        /// <summary>
        /// Zwróć cenę przedmiotu
        /// </summary>
        /// <returns></returns>
        public abstract int ReturnPrice();

        /// <summary>
        /// Zwróć specjalną właściwość przedmiotu
        /// </summary>
        /// <returns></returns>
        public abstract int ReturnSpecialProperty();
    }
}
