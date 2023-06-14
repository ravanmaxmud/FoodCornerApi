using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class OrderProduct : BaseEntity, IAuditable
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public int? SizeId { get; set; }
        public Size? Size { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
