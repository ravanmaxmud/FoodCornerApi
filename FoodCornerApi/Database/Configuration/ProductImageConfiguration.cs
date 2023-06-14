using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Database.Configuration
{

    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder
               .ToTable("ProductImages");
        }
    }

}

