using FoodCornerApi.Areas.Client.Dtoes.Authentication;
using FoodCornerApi.Database;
using FoodCornerApi.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCornerApi.Areas.Client.Controllers
{
    [ApiController]
    [Area("client")]
    [Route("client/auth")]
    public class AuthneticationController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IUserActivationService _userActivationService;
        private readonly ILogger<AuthneticationController> _logger;

        public AuthneticationController(DataContext dbContext, IUserService userService, IUserActivationService userActivationService, ILogger<AuthneticationController> logger)
        {
            _dbContext = dbContext;
            _userService = userService;
            _userActivationService = userActivationService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDto dto)
        {
            return Ok(await _userService.SignInAsync(dto.Email, dto.Password));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)    
        {
            var users = await _dbContext.Users.ToListAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email Address is already un use.");
                _logger.LogWarning($"({model.Email}) This Email Address is already un use.");
                return BadRequest($"{model} is invalid");
            }
            var emails = new List<string>();
            emails.Add(model.Email);
            await _userService.CreateAsync(model);
            return Ok("User Aded Sucesifully");
        }
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> CurrentUser() 
        {
            return Ok(_userService.GetCurrentUserFullName());
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            return Ok("Ok");
        }

        [HttpGet("activate/{token}", Name = "client-auth-activate")]
        public async Task<IActionResult> Activate([FromRoute] string token)
        {
            var userActivation = await _dbContext.UserActivations.Include(u => u.User)
                .FirstOrDefaultAsync(u => !u.User!.IsActive && u.ActivationToken == token);

            if (userActivation is null)
            {
                return NotFound("Activation Token Not Found");
            }

            if (DateTime.Now > userActivation.ExpiredDate)
            {
                return BadRequest("Token expired olub teessufler");
            }
            userActivation.User.IsActive = true;
            await _dbContext.SaveChangesAsync();
            return Ok("Client is now Active!!");
        }
    }
}
