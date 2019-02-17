using Cranium.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cranium.Data.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Question>().HasMany<Answer>().WithOne(x => x.Question);
            modelBuilder.Entity<QuestionType>().HasMany<Question>().WithOne(x => x.QuestionType);
            modelBuilder.Entity<Category>().HasMany<Question>().WithOne(x => x.Category);
        }
    }
}