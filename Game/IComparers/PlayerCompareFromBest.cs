using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PlayerCompareFromBest : IComparer<PlayerScore>
    {
        /// <summary>
        /// Metoda sortuje listę graczy pod względem doświadczenia.
        /// Od najwyższego do najniższego 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Compare(PlayerScore a, PlayerScore b)
        {
            if (a.experience > b.experience)
                return -1;
            if (a.experience < b.experience)
                return 1;
            
            return 0;
        }
    }
}
