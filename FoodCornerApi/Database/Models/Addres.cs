using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Addres : BaseEntity, IAuditable
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
