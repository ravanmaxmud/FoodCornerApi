using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Blog : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<BlogFile>? BlogFiles { get; set; }
        public List<BlogAndBlogCategory>? BlogCategories { get; set; }
        public List<BlogAndBlogTag>? BlogTags { get; set; }
    }
}
