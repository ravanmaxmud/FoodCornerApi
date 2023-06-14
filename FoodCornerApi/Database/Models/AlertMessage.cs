using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class AlertMessage : BaseEntity, IAuditable
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
