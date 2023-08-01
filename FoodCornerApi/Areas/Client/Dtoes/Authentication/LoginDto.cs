using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Client.Dtoes.Authentication
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
