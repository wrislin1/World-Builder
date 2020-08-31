using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldBuilder.Models
{
    public class Realtionship
    {
        public int Id { get; set; }
        public int FamilyID { get; set; }
        public int Character1ID { get; set; }
        public int Character2ID { get; set; }
        public int RelationshipTypeID { get; set; }
        public int Character1RoleID { get; set; }
        public int Character2RoleID { get; set; }
        public string Details { get; set; }
    }
}
