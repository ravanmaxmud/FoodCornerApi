using FoodCornerApi.Exceptions;
using FoodCornerApi.Extensions;
using System.Globalization;
using FoodCornerApi.Middlewares;

namespace FoodCornerApi.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            
            app.UseStaticFiles();

            app.UseCustomExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI();
            //app.ConfigureExceptionHandler();
          
            //app.ConfigureCustomExceptionMiddleware();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/not-found-example", () =>
            {
                throw new NotFoundException("Information is not found in DB");
            });

            app.MapGet("/bad-request-example", () =>
            {
                throw new BadRequestException("Requester URL is invalid");
            });

            app.MapControllers();
    
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{area=exists}/{controller=home}/{action=index}");

            //app.MapHub<AlertHub>("hubs/alert-hub");
            //app.MapHub<ChatHub>("hubs/chat-hub");
        }
    }
}
