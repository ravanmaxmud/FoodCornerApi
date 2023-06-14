namespace FoodCornerApi.Areas.Admin.Dtoes.Category
{
    public class CategoryListDto
    {
        public CategoryListDto(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
