using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NordFish.Database.Entities;

namespace NordFish.EntityFramework.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users").HasKey(users => users.Id);

            builder.Property(users => users.Login).HasMaxLength(15);
            builder.Property(users => users.Password).HasMaxLength(15);
            builder.Property(users => users.Email).HasMaxLength(31);
        }
    }
}
