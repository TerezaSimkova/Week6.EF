using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    public class Battle
    {
        public int BattleId { get; set; }
        public string Name { get; set; }

        //Relazione molti e molti ocn i cavalieri

        public List<Knight> Knights { get; set; } = new List<Knight>(); // creo e inizializzo la lista cavalieri ,la stessa cosa faccio ai lato dei cavalieri
    }
}
