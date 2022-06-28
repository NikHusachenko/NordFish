using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NordFish.Database.Entities;

namespace NordFish.EntityFramework.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products").HasKey(products => products.Id);

            builder.Property(product => product.Description).HasMaxLength(255);
            builder.Property(product => product.Name).HasMaxLength(31);

            builder.HasOne<UserEntity>(products => products.User)
                .WithMany(user => user.Products)
                .HasForeignKey(product => product.UserFK);
        }
    }
}
