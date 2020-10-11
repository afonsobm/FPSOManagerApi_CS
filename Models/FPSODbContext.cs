using Microsoft.EntityFrameworkCore;

namespace FPSOManagerApi_CS.Models
{
    public class FPSODbContext : DbContext
    {
        public FPSODbContext(DbContextOptions<FPSODbContext> options) : base(options)
        {
        }

        public DbSet<Equipment> equipment { get; set; }
        public DbSet<Vessel> vessels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
               builder.Entity<Vessel>()
               .HasKey(e => e.code);

               builder.Entity<Equipment>()
               .HasKey(e => e.code);

               base.OnModelCreating(builder);
        }
    }
}