using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Role : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<User> Users { get; set; }
    }
}
