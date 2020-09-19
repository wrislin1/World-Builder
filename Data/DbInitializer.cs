using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldBuilder.Models;

namespace WorldBuilder.Data
{
    public class DbInitializer
    {
        public static void Initialize(WorldContext context)
        {
            context.Database.EnsureCreated();
            if (!context.RelationshipTypes.Any())
            {
                var RelationshipTypes = new RelationshipType[]
                {
                    new RelationshipType{Description="Parent"},new RelationshipType{Description="Sibling"},
                    new RelationshipType{Description="Cousin"}, new RelationshipType{Description="GrandParent"},
                     new RelationshipType{Description="Spouse"},new RelationshipType{Description="Romantic Partner"}, new RelationshipType{Description="Friend"}
                    , new RelationshipType{Description="GrandChild"}, new RelationshipType{Description="Child"}
                };

                foreach(RelationshipType r in RelationshipTypes)
                {
                    context.RelationshipTypes.Add(r);
                }
                context.SaveChanges();
            }

            /* if(!context.RelationshipTypes.Any())
            {
                var relationshiptypes = new RelationshipType[] 
                {
                    new RelationshipType{Description="Related"}, new RelationshipType{Description="Know Eachother"},new RelationshipType{Description="Married"},new RelationshipType{Description="Dating"}
                    
                };
                foreach(RelationshipType t in relationshiptypes)
                {
                    context.RelationshipTypes.Add(t);
                }
                context.SaveChanges();
            } */

        }
    }
}
