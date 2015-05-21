using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class SwordSteel : Weapon, IAttackItem
    {
        /// <summary>
        /// Siła specjalnej właściwości przedmiotu
        /// </summary>
        int damage;
        public int Damage { get { return damage; } }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="price"> Cena przedmiotu </param>
        /// <param name="specjalPropertyPower"> moc specjalniej właściwości przedmiotu </param>
        public SwordSteel(int price, int damage) : base(price)
        {
            this.damage = damage;
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
            return Damage;
        }
    }
}
