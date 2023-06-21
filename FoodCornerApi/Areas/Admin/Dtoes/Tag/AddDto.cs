using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.Tag
{
    public class AddDto
    {
        [Required]
        public string Title { get; set; }
    }
}
