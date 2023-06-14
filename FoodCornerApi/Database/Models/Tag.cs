using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Tag : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
