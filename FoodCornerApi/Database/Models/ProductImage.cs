using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class ProductImage : BaseEntity, IAuditable
    {
        public string ImageNames { get; set; }
        public string ImageNameFileSystem { get; set; }
        public bool IsPoster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
