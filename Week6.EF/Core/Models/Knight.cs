using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    public class Knight
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relazione 1 a molti con le armi
        public List<Weapon> Weapons { get; set; } = new List<Weapon>();

        //Ralazione molti e molti con la battaglia

        public List<Battle> Battles { get; set; } = new List<Battle>();

        //relazione uno a uno con cavallo
        public Horse Horse { get; set; } //NP
    }
}
