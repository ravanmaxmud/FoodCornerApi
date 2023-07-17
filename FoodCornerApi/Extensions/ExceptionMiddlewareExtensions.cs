using FoodCornerApi.CustomExceptionMiddleware;
using FoodCornerApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Build.ObjectModelRemoting;
using System.Net;

namespace FoodCornerApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
