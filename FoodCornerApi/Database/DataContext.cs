using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Extensions;

namespace FoodCornerApi.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
       : base(options)
        {

        }

        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<SubNavbar> SubNavbars { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCatagory> ProductCatagories { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Addres> Address { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<PasswordForget> PasswordForgets { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AboutVidio> Vidios { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogAndBlogCategory> BlogAndBlogCategories { get; set; }
        public DbSet<BlogAndBlogTag> BlogAndBlogTags { get; set; }

        public DbSet<BlogFile> BlogFiles { get; set; }
        public DbSet<AlertMessage> Messages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }
}
