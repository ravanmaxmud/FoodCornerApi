using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Story : BaseEntity, IAuditable
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}