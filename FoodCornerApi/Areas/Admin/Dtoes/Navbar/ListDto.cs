namespace FoodCornerApi.Areas.Admin.Dtoes.Navbar
{
    public class ListDto
    { 
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string ToURL { get; set; } = default!;
        public int Order { get; set; } = default!;
        public bool IsViewHeader { get; set; } = default!;
        public bool IsViewFooter { get; set; } = default!;
    }
}
