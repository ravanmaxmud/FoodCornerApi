using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.Product
{
    public class AddDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }

        [Required]
        public List<int> CategoryIds { get; set; }

        [Required]
        public List<int> TagIds { get; set; }

        [Required]
        public List<int> SizeIds { get; set; }

        public int? DiscountPercent { get; set; }
        public int? DiscountPrice { get; set; }

        [Required]
        public IFormFile PosterImage { get; set; }

        [Required]
        public List<IFormFile>? AllImages { get; set; }
    }
}
