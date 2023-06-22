namespace FoodCornerApi.Areas.Admin.Dtoes.SubNavbar
{
    public class UpdateDto
    {
        public string Name { get; set; }
        public string ToURL { get; set; }

        public int Order { get; set; }

        public int NavbarId { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
