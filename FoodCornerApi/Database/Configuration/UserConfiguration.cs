using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .ToTable("Users");

            builder
              .HasOne(u => u.Basket)
                .WithOne(b => b.User)
                  .HasForeignKey<Basket>(u => u.UserId);
        }
    }
}
