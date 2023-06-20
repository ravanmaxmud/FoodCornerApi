

namespace FoodCornerApi.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{area=exists}/{controller=home}/{action=index}");

            //app.MapHub<AlertHub>("hubs/alert-hub");
            //app.MapHub<ChatHub>("hubs/chat-hub");
        }
    }
}
