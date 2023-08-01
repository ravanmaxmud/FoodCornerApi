using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Client.Dtoes.Authentication
{
    public class ForgetPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
