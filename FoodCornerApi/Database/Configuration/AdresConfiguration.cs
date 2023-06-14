using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Database.Configuration
{
    public class AdresConfiguration : IEntityTypeConfiguration<Addres>
    {
        public void Configure(EntityTypeBuilder<Addres> builder)
        {
            builder
               .ToTable("Address");
        }
    }
}
