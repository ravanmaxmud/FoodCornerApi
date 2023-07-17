using FoodCornerApi.CustomExceptionHandler;
using FoodCornerApi.Middlewares;

namespace FoodCornerApi.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ExceptionHandlerCoordinator coordinator)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(context, ex, coordinator);
            }
        }
        private Task HandleExceptionsAsync(HttpContext context, Exception exception, ExceptionHandlerCoordinator coordinator)
        {
            var exceptionResult = coordinator.Handle(exception);

            context.Response.ContentType = exceptionResult.ContentType;
            context.Response.StatusCode = exceptionResult.HttpStatusCode;

            return context.Response.WriteAsync(exceptionResult.Message);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
