using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
        Task SendChangePasswordUrlAsync(User user);
    }
}
