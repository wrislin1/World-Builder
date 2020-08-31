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
            if (!context.Roles.Any())
            {
                var roles = new Role[]
                {
                    new Role{Description="Father"},new Role{Description="Mother"},new Role{Description="Brother"}, new Role{Description="Sister"},
                    new Role{Description="Uncle"}, new Role{Description="Aunt"},new Role{Description="Uncle"},new Role{Description="Cousin"}, new Role{Description="GrandFather"},
                    new Role{Description="Grandmother"}, new Role{Description="Husband"}, new Role{Description="Wife"}, new Role{Description="Boyfriend"},
                    new Role{Description="Girlfriend"}, new Role{Description="Friends"}
                };

                foreach(Role r in roles)
                {
                    context.Roles.Add(r);
                }
                context.SaveChanges();
            }

            if(!context.RelationshipTypes.Any())
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
            }

        }
    }
}
