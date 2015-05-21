using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Gold : SpecialWeapon
    {
        private int specialMoney;

        /// <summary>
        /// Metoda sprawdza, czy przedmiot jest podniesiony
        /// </summary>
        /// <returns></returns>
        public bool IsPickedUp() //sprawdzamy, czy przedmiot jest podniesiony
        {
            return base.IsPickedUp;
        }

        /// <summary>
        /// Metoda służąca do podniesienia przedmiotu
        /// </summary>
        /// <returns></returns>
        public int pickUp() //podnosimy przedmiot
        {
            ChangeItemLocation(); //jak podniesiemy pzedmiot, to zmieniamy jego lokalizacje
            base.IsPickedUp = false; //zmieniamy status podniesienia przedmiotu
            return specialMoney; //zwracamy ilość specjalnego doświadczenia za podniesienie przedmiotu
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Gold(int specialMoney)
        {
            this.specialMoney = specialMoney;
            base.IsPickedUp = false;
            ChangeItemLocation();
        }

        /// <summary>
        /// Metoda zwraca ilość pieniędzy, które zyskujemy przy podniesieniu sztabki
        /// </summary>
        /// <returns></returns>
        public override int ReturnSpecialValue()
        {
            return specialMoney;
        }

        /// <summary>
        /// Metoda zmieniająca położenie przedmiotu
        /// </summary>
        public override void ChangeItemLocation()
        {
            int x = RandomUtility.MyRandom.Next(100, 490); //zakres naszej mapy(oś x) wiem, że mało profesjonalnie, bo powinienem to przechwytywać
            int y = RandomUtility.MyRandom.Next(100, 250); //zakres naszej mapy(oś x) wiem, że mało profesjonalnie, bo powinienem to przechwytywać
            base.Location = new Point(x, y); //przyposujemy nową lokalizację
        }
    }
}
