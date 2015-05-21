using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class RedPotion : Weapon, IPotion
    {
        /// <summary>
        /// Siła specjalnej właściwości przedmiotu
        /// </summary>
        int potionPower;
        public int PotionPower { get { return potionPower; } }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="price"> Cena przedmiotu </param>
        /// <param name="specjalPropertyPower"> moc specjalniej właściwości przedmiotu </param>
        public RedPotion(int price, int specjalPropertyPower) : base(price)
        {
            this.potionPower = specjalPropertyPower;
        }

        /// <summary>
        /// Metoda zwraca cenę przedmiotu
        /// </summary>
        /// <returns></returns>
        public override int ReturnPrice()
        {
            return base.Price;
        }

        /// <summary>
        /// Metoda zwraca moc specjalnej właściwości przedmiotu
        /// </summary>
        /// <returns></returns>
        public override int ReturnSpecialProperty()
        {
            return PotionPower;
        }
    }
}
