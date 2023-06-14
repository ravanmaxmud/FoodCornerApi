namespace FoodCornerApi.Database.Models.Common
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
