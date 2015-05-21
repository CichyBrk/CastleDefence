using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Metoda zwracająca nową instancję klasy random
    /// </summary>
    public static class RandomUtility
    {
        private static Random myRandom = new Random();
        public static Random MyRandom { get { return myRandom; } }
    }
}
