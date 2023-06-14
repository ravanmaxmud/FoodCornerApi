using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class BlogFile : BaseEntity, IAuditable
    {
        public string? FileName { get; set; }
        public string? FileNameInSystem { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
