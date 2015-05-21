using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    abstract class Enemy : Mover
    {
        #region Pola klasy

        public Point EnemyLocation { get { return base.location; } } //location znajduje się w klasie mover

        private int attack; //siła ataku
        public int Attack {
            get { return this.attack; }
            set { this.attack = value; }
        }

        private int numberOfHealths; //ilość życia
        public int NumberOfHealths
        {
            get { return this.numberOfHealths; }
            set { this.numberOfHealths = value; }
        }

        public int Speed //szybkość poruszania się
        {
            get { return this.speed; }
            set { 
                base.speed = value;
            }
        }

        #endregion

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="location"> lokalizacja potwora </param>
        /// <param name="name"> nazwa </param>
        /// <param name="numberOfHealths"> ilość życia </param> 
        /// <param name="speed"> szybkość </param>
        /// <param name="attack"> siła ataku </param>
        public Enemy(Point location, int numberOfHealths, int speed, int attack) : base(location)
        {
            base.speed = speed;
            Attack = attack;
            NumberOfHealths = numberOfHealths;
        }

        #region Metody obsługi pól potwora

        /// <summary>
        /// Metoda zwracająca siłę ataku potwora
        /// </summary>
        /// <returns>Siła ataku potwora</returns>
        public int AttackPower()
        {
            return Attack;
        }

        /// <summary>
        /// Metoda zadająca obrażenia potworowi
        /// </summary>
        /// <param name="hitPower"></param>
        public void Hit(int hitPower)
        {
            NumberOfHealths -= hitPower;
        }

        #endregion

        /// <summary>
        /// Metoda sprawdzająca, czy potwór jeszcze żyje
        /// </summary>
        /// <returns>czy potwór żyje</returns>
        public bool Live()
        {
            if (numberOfHealths <= 0)
                return false;
            else
                return true;
        }

        public abstract void Move(Rectangle boundaries);
    }
}
