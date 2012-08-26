using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace gamejamtarts.Models
{
    public class Db : DbContext
    {
        public Db() : base()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

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

        public IQueryable<Game> AllGames
        {
            get
            {
                return Games
                    .Include(x => x.Team)
                    .Include(x => x.Files)
                    .Include(x => x.Images);
            }
        }
    }
}

