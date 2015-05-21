using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Przedmiot służy do regeneracji hp
    /// </summary>
    interface IPotion
    {
        int PotionPower { get; }
    }
}
