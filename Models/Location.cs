using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldBuilder.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public int WorldID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public World World { get; set; }

    }
}
