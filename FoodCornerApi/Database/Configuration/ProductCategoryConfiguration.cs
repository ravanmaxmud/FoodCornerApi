using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Database.Configuration
{

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCatagory>
    {
        public void Configure(EntityTypeBuilder<ProductCatagory> builder)
        {
            builder
               .ToTable("ProductCategoryies");
        }
    }
}
