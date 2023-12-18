using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MistApp.Models;

namespace MistApp
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Game { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Copy> Copy { get; set; }
        public DbSet<Friendship> Friendship { get; set; }
        public DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=mist.db");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
