namespace FoodCornerApi.Areas.Admin.Dtoes.Category
{
    public class ListDto
    {
        public ListDto(int id, string title, string parentName, string backgroundİmageUrl)
        {
            Id = id;
            Title = title;
            ParentName = parentName;
            BackgroundİmageUrl = backgroundİmageUrl;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ParentName { get; set; }
        public string BackgroundİmageUrl { get; set; }
    }
}
