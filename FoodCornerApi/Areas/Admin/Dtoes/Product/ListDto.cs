namespace FoodCornerApi.Areas.Admin.Dtoes.Product
{
    public class ListDto
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!; 
        public int Price { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
