using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class BasketProduct : BaseEntity, IAuditable
    {
        public int? CurrentPrice { get; set; }
        public int? CurrentDiscountPrice { get; set; }
        public int BasketId { get; set; }
        public Basket? Basket { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int? SizeId { get; set; }
        public Size? Size { get; set; }

        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
