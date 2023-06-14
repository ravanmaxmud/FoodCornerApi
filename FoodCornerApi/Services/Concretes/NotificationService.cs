//using FoodCorner.Areas.Admin.Hubs;
//using FoodCorner.Database.Models;
//using FoodCornerApi.Contracts.Alert;
//using FoodCornerApi.Contracts.Identity;
//using FoodCornerApi.Database;
//using FoodCornerApi.Services.Abstracts;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;

//namespace FoodCornerApi.Services.Concretes
//{
//    public class NotificationService : INotificationService
//    {
//        private readonly IHubContext<AlertHub> _alertHub;
//        private readonly IUserService _userService;
//        private readonly DataContext _dataContext;

//        public NotificationService(IHubContext<AlertHub> alertHub, IUserService userService, DataContext dataContext)
//        {
//            _alertHub = alertHub;
//            _userService = userService;
//            _dataContext = dataContext;
//        }

//        public async Task SendOrderCreatedToAdmin(string orderId)
//        {


//            await InfoOrderAdminAsync();


//            await InfoOrderClientAsync();


//            async Task InfoOrderAdminAsync()
//            {
//                foreach (var user in await _dataContext.Users.Where(u => u.Roles.Name == RoleNames.ADMIN).ToListAsync())
//                {

//                    var title = AlertMessages.ORDER_CREATED_TITLE_TO_MODERATOR;
//                    var content = AlertMessages.ORDER_CREATED_CONTENT_TO_MODERATOR
//                                        .Replace("{user_email}", _userService.CurrentUser.Email)
//                                        .Replace("{tracking_code}", orderId);


//                    var message = new AlertMessage
//                    {
//                        Title = title,
//                        Content = content,
//                        UserId = user.Id,
//                        IsSeen = false,
//                        CreatedAt = DateTime.Now
//                    };

//                    await _dataContext.Messages.AddAsync(message);
//                    await _dataContext.SaveChangesAsync();


//                    await _alertHub.Clients
//                        .Group(user.Id.ToString())
//                        .SendAsync("Notify", new
//                        {
//                            Title = title,
//                            Content = content  //string builder should be used
//                        });
//                }


//            }



//            async Task InfoOrderClientAsync()
//            {
//                var title = AlertMessages.ORDER_CREATED_TITLE_TO_OWNER;
//                var content = AlertMessages.ORDER_CREATED_TITLE_TO_OWNER
//                                    .Replace("{tracking_code}", orderId);


//                var message = new AlertMessage
//                {
//                    Title = title,
//                    Content = content,
//                    UserId = _userService.CurrentUser.Id,
//                    IsSeen = false,
//                    CreatedAt = DateTime.Now
//                };

//                await _dataContext.Messages.AddAsync(message);



//                await _alertHub.Clients
//                        .Group(_userService.CurrentUser.Id.ToString())
//                         .SendAsync("Notify", new
//                         {
//                             Title = title,
//                             Content = content  //string builder should be used
//                         });
//            }
//        }
//    }
//}
