using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Quiz.db");
        }
    }
}
