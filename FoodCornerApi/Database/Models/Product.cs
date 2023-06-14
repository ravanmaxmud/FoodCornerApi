using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Product : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }
        public int? DiscountPercent { get; set; }
        public List<ProductCatagory>? ProductCatagories { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
        public List<Comment>? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
