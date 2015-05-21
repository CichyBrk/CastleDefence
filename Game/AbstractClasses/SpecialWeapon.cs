using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    abstract class SpecialWeapon
    {
        Point location; //lokalizacja naszego przedmotu
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        private bool isPickedUp; //status naszego przedmiotu(czy podniesiony)
        public bool IsPickedUp
        {
            get { return isPickedUp; }
            set
            {
                isPickedUp = false; //i znowu nie jest podniesiony(respi się w innym miejscu)
            }
        }

        /// <summary>
        /// Metoda zwraca specjalną właściwość przedmiotu
        /// </summary>
        /// <returns>specjalna wartość</returns>
        public abstract int ReturnSpecialValue();

        /// <summary>
        /// Metoda zmieniająca położenie przedmiotu
        /// </summary>
        public abstract void ChangeItemLocation();
    }
}
