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
               builder.Entity<Contato>().HasKey(m => m.Id);
               base.OnModelCreating(builder);
        }
    }
}