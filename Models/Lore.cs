using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldBuilder.Models
{
    public class Lore
    {
        public int LoreID { get; set; }
        public int WorldID { get; set; }
        public string Title { get; set; }
        public World World { get; set; }
        public string Details { get; set; }


    }
}
