using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.SubNavbar
{
    public class AddDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string? ToURL { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public int NavbarId { get; set; }
    }
}
