using Microsoft.EntityFrameworkCore;

namespace Buoi5.Models
{
    public class AppicationDbContext: DbContext
    {

        public AppicationDbContext(DbContextOptions<AppicationDbContext> options) : base(options)
        {

        }

        public DbSet<Students> Students { get; set; }
        public DbSet<Grades> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.gradeId);
        }
    }
    

    }

