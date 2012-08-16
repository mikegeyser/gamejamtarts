using System.Data.Entity;

namespace gamejamtarts.Models
{
    public class Db : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}

