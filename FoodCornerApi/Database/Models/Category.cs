using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Category : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public string Backgroundİmage { get; set; }
        public string BackgroundİmageInFileSystem { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<ProductCatagory> ProductCatagories { get; set; }
        public List<Category> Catagories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
