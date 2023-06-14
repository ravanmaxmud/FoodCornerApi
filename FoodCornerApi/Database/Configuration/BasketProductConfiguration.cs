using FoodCornerApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodCornerApi.Database.Configuration
{
    public class BasketProductConfiguration : IEntityTypeConfiguration<BasketProduct>
    {
        public void Configure(EntityTypeBuilder<BasketProduct> builder)
        {
            builder
                .ToTable("basket-products");

            builder
             .HasOne(bp => bp.Basket)
             .WithMany(basket => basket.BasketProducts)
             .HasForeignKey(bp => bp.BasketId);

            builder
              .HasOne(bp => bp.Product)
              .WithMany(product => product.BasketProducts)
              .HasForeignKey(bp => bp.ProductId);

        }
    }
}
