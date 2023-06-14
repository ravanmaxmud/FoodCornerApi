using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Database.Configuration
{
    public class SubNavbarConfiguration : IEntityTypeConfiguration<SubNavbar>
    {
        public void Configure(EntityTypeBuilder<SubNavbar> builder)
        {
            builder
               .ToTable("SubNavbars");
        }
    }
}
