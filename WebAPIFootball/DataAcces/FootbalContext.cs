using Microsoft.EntityFrameworkCore;
using WebAPIFootball.Model;

namespace WebAPIFootball.DataAcces
{
    public class FootbalContext :DbContext
    {
        public DbSet<Team> teams { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = VIA.db");
        }
    }
    
}