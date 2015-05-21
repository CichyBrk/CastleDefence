using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Player : Mover
    {
        #region Pola klasy player

        public Point PlayerLocation { get { return location; } } //location znajduje się w klasie mover
        
        public List<Weapon> Weapons = new List<Weapon>();

        private string name; //nazwa
        public string Name { get { return name; } }

        private int health; //zdrowie
        public int Health { 
            get { return this.health; } 
            private set { this.health = value; } 
        }

        private int money; //pieniądze
        public int Money{
            get { return this.money; }
            private set { this.money = value; }
        }

        private int attack; //siła ataku
        public int Attack
        {
            get { return this.attack; }
            private set { this.attack = value; }
        }

        private int defence = 0; //siła obrony
        public int Defence
        {
            get { return this.defence; }
            private set { this.defence = value; }
        }

        private int experience; //doświadczenie
        public int Experience{
            get { return this.experience; }
            private set 
            { 
                this.experience = value;
                if (this.health < 300)
                {
                    health += 5;
                }
            }
        }

        public int Speed //właściwość odnosząca się do pola (speed) klasy bazowej
        {
            get { return base.speed; }
            private set { base.speed = value; }
        }

        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="location"></param> //lokalizacja
        /// <param name="name"></param> //nazwa
        /// <param name="health"></param> //ilość punktów życia
        /// <param name="money"></param> //ilość pieniędzy
        /// <param name="experience"></param> //doświadczenie
        /// <param name="speed"></param> //szybkość
        /// <param name="attack"></param> //siła ataku
        public Player(Point location, string name, int health, int money, int experience, int speed, int attack) : base(location)
        {
            Speed = speed;
            this.name = name;
            Health = health;
            Money = money;
            Experience = experience;
            Attack = attack;
        }

        #region Metody służące do aktualizacji pól gracza (atak, obrona etc.)

        /// <summary>
        /// Metoda służąca do zwiększania doświadczenia
        /// </summary>
        /// <param name="experience"></param> //ilość o jaką zwiększamy doświadczenie
        public void GiveExperience(int experience)
        {
            Experience += experience;
        }

        /// <summary>
        /// Metoda służąca do zadawania obrażeń graczowi
        /// </summary>
        /// <param name="hitPower"></param> //mo obrażeń
        public void Hit(int hitPower)
        {
            Health -= hitPower;
        }

        /// <summary>
        /// Metoda służąca do modyfikacji ilości pieniędzy
        /// </summary>
        /// <param name="money"></param>
        public void GiveMoney(int money)
        {
            Money += money;
        }

        /// <summary>
        /// Metoda służąca do modyfikacji wartości ataku w zależności od wybranego itemu
        /// </summary>
        /// <param name="damage"></param>
        public void GiveNewAttackItem(int damage)
        {
            Attack = damage;
        }

        /// <summary>
        /// Metoda służąca do modygfikacji wartości obrony w zależności od wybranego itemu
        /// </summary>
        /// <param name="defence"></param>
        public void GiveNewDefenceItem(int defence)
        {
            Defence = defence;
        }

        /// <summary>
        /// Metoda służąca do usuwania zużytych potek z ekwipunku gracza
        /// </summary>
        public void DeletePotion()
        {
            //List<Weapon> toDelete = new List<Weapon>();
            foreach (var item in Weapons)
            {
                if (item is IPotion)
                {
                    //toDelete.Add(item);
                    Weapons.Remove(item);
                    break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Metoda służąca do poruszania się
        /// </summary>
        /// <param name="direction"></param> //kierunek
        /// <param name="boundaries"></param> //obszar mapy
        public void Move(Direction direction, Rectangle boundaries)
        {
            base.location = base.Move(direction, boundaries);
        }
    }
}
