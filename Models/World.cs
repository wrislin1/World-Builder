using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldBuilder.Models
{
    public class World
    {
         public string Name { get; set; }
         public string Summary { get; set; }
         public int WorldID { get; set; }

        public ICollection<Character> Characters { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Lore> Lores { get; set; }
    }
}
