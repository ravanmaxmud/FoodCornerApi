using FoodCornerApi.Areas.Client.Dtoes.Authentication;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsAuthenticated { get; }
        public User CurrentUser { get; }

        Task<bool> CheckPasswordAsync(string? email, string? password);
        string GetCurrentUserFullName();
        Task<string> SignInAsync(int id, string? role = null);
        Task<string> SignInAsync(string? email, string? password, string? role = null);
        Task CreateAsync(RegisterDto model);
        Task SignOutAsync();
    }
}
