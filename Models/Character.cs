﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldBuilder.Models
{
    public class Character
    {
         public string Name { get; set; }

         public int CharacterID { get; set; }

        public int WorldID { get; set; }
        public World World { get; set; }
        public int? LocationID { get; set; }
        public virtual Location Location { get; set; }

        public string Summary { get; set; }

        public ICollection<Relationship> Relationships { get; set; }
    }
}
