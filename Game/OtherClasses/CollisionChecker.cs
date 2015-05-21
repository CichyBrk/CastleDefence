using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class CollisionChecker
    {
        /// <summary>
        /// Metoda wykrywająca kolizję pomiędzy obiektami
        /// </summary>
        /// <param name="a"> Obiekt a </param>
        /// <param name="b"> Obiekt b </param>
        /// <returns></returns>
        public static bool CheckColision(Point a, Point b)
        {
            if (Math.Abs(a.X - b.X) <= 35 && //35, to pomniejszona o pięć wielkość picture boxów
                Math.Abs(a.Y - b.Y) <= 35)
            {
                return true;
            }
            return false;
        }
    }
}
