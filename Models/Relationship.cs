using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldBuilder.Models
{
    public class Relationship
    {
        public int RelationshipID { get; set; }
        public int Character1ID { get; set; }
        public int Character2ID { get; set; }
        public int RelationshipTypeID { get; set; }
        public string Details { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }

    }
}
