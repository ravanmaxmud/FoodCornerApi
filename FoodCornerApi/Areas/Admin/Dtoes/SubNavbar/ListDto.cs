namespace FoodCornerApi.Areas.Admin.Dtoes.SubNavbar
{
    public class ListDto
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string ToURL { get; set; } = default!;
        public int Order { get; set; } = default!;
        public string Navbar { get; set; } = default!;
    }
}
