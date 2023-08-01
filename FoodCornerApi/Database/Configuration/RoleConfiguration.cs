using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Contracts.Identity;

namespace FoodCornerApi.Database.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        private int _idCounter = 1;
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .ToTable("Roles");

            builder
                .HasData(
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.USER,
                        CreatedAt = Convert.ToDateTime("2023-01-24"),
                        UpdateAt = Convert.ToDateTime("2023-01-24")
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.ADMIN,
                        CreatedAt = Convert.ToDateTime("2023-01-24"),
                        UpdateAt = Convert.ToDateTime("2023-01-24")
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.MODERATOR,
                        CreatedAt = Convert.ToDateTime("2023-01-24"),
                        UpdateAt = Convert.ToDateTime("2023-01-24")
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.HR,
                        CreatedAt = Convert.ToDateTime("2023-01-24"),
                        UpdateAt = Convert.ToDateTime("2023-01-24"),
                    }
                );
        }
    }
}
