using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class UserActivation : BaseEntity, IAuditable
    {
        public string? ActivationUrl { get; set; }
        public string? ActivationToken { get; set; }
        public DateTime ExpiredDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
