using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.Navbar
{
    public class UpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ToURL { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public bool IsViewHeader { get; set; }
        [Required]
        public bool IsViewFooter { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
