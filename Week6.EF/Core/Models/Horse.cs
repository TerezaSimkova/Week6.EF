using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    public class Horse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int KnightId { get; set; }
    }
}
