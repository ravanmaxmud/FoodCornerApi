using Microsoft.AspNetCore.Mvc;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Services.Abstracts;
using FoodCornerApi.Contracts.Email;
using FoodCornerApi.Database;

namespace FoodCornerApi.Services.Concretes
{
    public class UserActivationService : IUserActivationService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly DateTime _activationExpireDate;
        private const string EMAIL_CONFIRMATION_ROUTE_NAME = "client-auth-activate";
        private const string PASSWORD_CHANGE_TOKEN = "client-auth-forgetPasswordToken";
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserActivationService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUrlHelper urlHelper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;

            double activationValidityMonute =
                Convert.ToDouble(configuration.GetRequiredSection("ActivationValidityMinute").Value);

            _activationExpireDate = DateTime.Now.AddMinutes(activationValidityMonute);
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task SendActivationUrlAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            var token = GenerateActivationToken();
            var activationUrl = GenerateUrl(token, EMAIL_CONFIRMATION_ROUTE_NAME);
            await CreateUserActivationAsync(user, token, activationUrl, _activationExpireDate);
            var activationMessageDto = PrepareActivationMessage(user.Email!, activationUrl);

            _emailService.Send(activationMessageDto);
        }

        public async Task SendChangePasswordUrlAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            var token = GenerateActivationToken();
            var activationUrl = GenerateUrl(token, PASSWORD_CHANGE_TOKEN);
            await CreatePasswordForgetAsync(user, token, activationUrl, _activationExpireDate);
            var activationMessageDto = PrepareChangePasswordMessage(user.Email!, activationUrl);

            _emailService.Send(activationMessageDto);
        }


        private string GenerateActivationToken()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateUrl(string token, string routeName)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            return _urlHelper.RouteUrl(routeName, new { token }, request.Scheme, request.Host.Value)!;
        }

        private async Task<UserActivation> CreateUserActivationAsync(User user, string token, string activationUrL, DateTime expireDate)
        {
            var userActivation = new UserActivation
            {
                User = user,
                ActivationToken = token,
                ActivationUrl = activationUrL,
                ExpiredDate = expireDate,
            };

            await _dataContext.UserActivations.AddAsync(userActivation);

            return userActivation;
        }

        private async Task<PasswordForget> CreatePasswordForgetAsync(User user, string token, string activationUrL, DateTime expireDate)
        {
            var passwordForget = new PasswordForget
            {
                User = user,
                ActivationToken = token,
                ActivationUrl = activationUrL,
                ExpiredDate = expireDate,
            };

            await _dataContext.PasswordForgets.AddAsync(passwordForget);

            return passwordForget;
        }



        private MessageDto PrepareActivationMessage(string email, string activationUrl)
        {
            string body = EmailMessages.Body.ACTIVATION_MESSAGE
                      .Replace(EmailMessageKeyword.ACTIVATION_URL, activationUrl);

            string subject = EmailMessages.Subject.ACTIVATION_MESSAGE;
            return new MessageDto(email, subject, body);
        }




        private MessageDto PrepareChangePasswordMessage(string email, string activationUrl)
        {
            var pathToFile = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() +
            "Client" + Path.DirectorySeparatorChar.ToString() + "EmailTempalte" +
            Path.DirectorySeparatorChar.ToString() + "ChangePassword.html";


            string body = "";
            using (StreamReader streamReader = File.OpenText(pathToFile))
            {
                body = streamReader.ReadToEnd();
            }

            string message = activationUrl;

            string messageBody = string.Format(body,
            message);

            string subject = EmailMessages.Subject.CHANGEPASSWORD_MESSAGE;

            return new MessageDto(email, subject, messageBody);
        }
    }
}
