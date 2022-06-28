using Microsoft.EntityFrameworkCore;
using NordFish.Database.Entities;
using NordFish.EntityFramework.Configuration;

namespace NordFish.EntityFramework
{
    public class NordFishDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Conserves { get; set; }

        public NordFishDbContext(DbContextOptions<NordFishDbContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
