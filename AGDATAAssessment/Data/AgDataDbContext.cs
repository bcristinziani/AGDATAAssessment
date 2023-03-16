using AGDATAAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AGDATAAssessment.Data
{
    public class AgDataDbContext : DbContext
    {
        public AgDataDbContext(DbContextOptions<AgDataDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
