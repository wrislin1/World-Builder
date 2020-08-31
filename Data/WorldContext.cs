using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldBuilder.Models;

namespace WorldBuilder.Data
{
    public class WorldContext : DbContext
    {
        public WorldContext(DbContextOptions<WorldContext> options) : base(options)
        {

        }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Character> Characters { get; set; }

        public DbSet<Family> Families { get; set; }
        public DbSet<Realtionship> Realtionships { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Lore> Lores { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<World>().ToTable("World");
            modelBuilder.Entity<Character>().ToTable("Character");
            modelBuilder.Entity<Family>().ToTable("Family");
            modelBuilder.Entity<RelationshipType>().ToTable("RelationshipType");
            modelBuilder.Entity<Realtionship>().ToTable("Realtionship");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Lore>().ToTable("Lore");



        }
    }
}
