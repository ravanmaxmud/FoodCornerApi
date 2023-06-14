using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class BlogCategory : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }
        public List<BlogAndBlogCategory> BlogCategories { get; set; }
        public List<BlogCategory> Categoris { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
