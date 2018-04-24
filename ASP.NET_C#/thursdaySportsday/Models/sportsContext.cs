using Microsoft.EntityFrameworkCore;

namespace thursdaySportsday.Models
{
    public class SportsContext : DbContext
    {
        public SportsContext(DbContextOptions<SportsContext> options) : base(options) { }

        public DbSet<User> Users {get;set;}
        
        public DbSet<Guest> Guests {get;set;}

        public DbSet<Sport> Sports {get;set;}
    }
}