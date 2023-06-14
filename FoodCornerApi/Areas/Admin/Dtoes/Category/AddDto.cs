using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.Category
{
    public class AddDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile? Backgroundİmage { get; set; }
        public string? BackgroundİmageUrl { get; set; }

        public int? CategoryId { get; set; }
        public List<CategoryListDto>? Categories { get; set; }
    }
}
