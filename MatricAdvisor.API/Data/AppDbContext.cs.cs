using Microsoft.EntityFrameworkCore;
using MatricAdvisor.API.Models;

namespace MatricAdvisor.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<University> Universities { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<SubjectRequirement> SubjectRequirements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>()
                .HasMany(u => u.Programmes)
                .WithOne(p => p.University)
                .HasForeignKey(p => p.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Programme>()
                .HasMany(p => p.SubjectRequirements)
                .WithOne(sr => sr.Programme)
                .HasForeignKey(sr => sr.ProgrammeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}