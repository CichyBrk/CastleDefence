using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Klasa służy do zapisywania wyników graczy(potrzebujemy tylko nazwę i zebrane doświadczenie)
    /// </summary>
    public class PlayerScore
    {
        public string name;
        public int experience;

        public PlayerScore(string name, int experience)
        {
            this.name = name;
            this.experience = experience;
        }

        /// <summary>
        /// konstruktor bezparametrowy, wymagany do serializacji
        /// </summary>
        public PlayerScore()
        {

        }

        /// <summary>
        /// Nadpisujemy metodę .ToString() obiektu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name + "\t" + experience + "\n";
        }
    }
}
