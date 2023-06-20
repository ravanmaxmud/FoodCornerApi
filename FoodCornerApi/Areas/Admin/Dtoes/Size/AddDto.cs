using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.Size
{
    public class AddDto
    {
        [Required]
        public int PersonSize { get; set; }
        [Required]
        public int IncreasePercent { get; set; }
    }
}
