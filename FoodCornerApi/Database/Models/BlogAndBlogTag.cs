using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class BlogAndBlogTag : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int BlogTagId { get; set; }
        public BlogTag Tag { get; set; }
    }
}
