using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Contracts.Identity;
using FoodCornerApi.Database;
using FoodCornerApi.Exceptions;
using FoodCornerApi.Services.Abstracts;
using FoodCornerApi.Areas.Client.Dtoes.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Azure;

namespace FoodCornerApi.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private readonly IConfiguration _configuration;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService,
            IConfiguration configuration)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
            _configuration = configuration;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }
                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
                if (idClaim is null)
                    throw new IdentityCookieException("Identity cookie not found");

                _currentUser = _dataContext.Users
                    .Include(u => u.Basket)
                    .ThenInclude(ub => ub.BasketProducts)
                    .First(u => u.Id == int.Parse(idClaim.Value));

                return _currentUser;
                #region JWT
                //============== Get Current User With JWT token=============================
                //string jwtToken = _httpContextAccessor.HttpContext!.Request.Cookies["JwtToken"]!;
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var decodedToken = tokenHandler.ReadJwtToken(jwtToken);
                #endregion
            }
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }
        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user is null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user!.Password);

        }
        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }
        public async Task SignInAsync(int id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID, id.ToString())
            };
            if (role is not null) claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            #region Jwt
            //================== Jwt SignIn With Token ==============================
            //var issuer = _configuration.GetSection("Jwt:Issuer").Value;
            //var audience = _configuration.GetSection("Jwt:Audience").Value;
            //var key = _configuration.GetSection("Jwt:Key").Value;
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var expireDate = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExperationMinute"]));

            //var tokenConfigurations = new JwtSecurityToken(
            //    issuer,
            //    audience,
            //    claims,
            //    expires: expireDate,
            //    signingCredentials: credentials
            //    );

            //var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenConfigurations);

            ////Save Token In Cookie
            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    SameSite = SameSiteMode.Strict,
            //    Secure = true
            //};
            // _httpContextAccessor.HttpContext!.Response.Cookies.Append("JwtToken", jwtToken, cookieOptions);
            //return jwtToken;
            #endregion
        }
        public async Task<string> SignInAsync(string? email, string? password, string? role = null)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);
            var user = await _dataContext.Users.Include(u => u.Roles).FirstAsync(u => u.Email == email);
            if (user is not null && BCrypt.Net.BCrypt.Verify(password, user.Password) && user.IsActive == true)
            {
              await SignInAsync(user.Id, role);
            }
            return user.Email;
        }
        public async Task CreateAsync(RegisterDto model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                RoleId = _dataContext.Roles.Where(r => r.Name == "user").FirstOrDefault()!.Id,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            await _dataContext.Users.AddAsync(user);


            var basket = new Basket
            {
                User = user,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now

            };
            await _dataContext.Baskets.AddAsync(basket);
            await _userActivationService.SendActivationUrlAsync(user);
            await _dataContext.SaveChangesAsync();
        }
        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
