using System.Data.Entity;

namespace gamejamtarts.Models
{
    public class Db : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasMany(x => x.Team);
            modelBuilder.Entity<Game>().HasMany(x => x.Images).WithRequired(x => x.Game);
            modelBuilder.Entity<Game>().HasMany(x => x.Files).WithRequired(x => x.Game);

        }
        
        public DbSet<Game> Games { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<GameFile> GameFiles { get; set; }
    }
}

