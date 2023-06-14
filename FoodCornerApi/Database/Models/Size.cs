using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class Size : BaseEntity, IAuditable
    {
        public int PersonSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int? IncreasePercent { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
    }
}
