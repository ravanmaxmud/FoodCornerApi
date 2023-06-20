namespace FoodCornerApi.Areas.Admin.Dtoes.Category
{
    public class ListDto
    {
        public int Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? ParentName { get; set; } = default!;
        public string BackgroundİmageUrl { get; set; } = default!;
    }
}
