using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AspNetCore.IServiceCollection.AddIUrlHelper;
using FoodCornerApi.BackgroundServices;
using FoodCornerApi.Infrastructure.Configurations;
using FoodCornerApi.Areas.Admin.Mappers;
using FoodCornerApi.CustomExceptionHandler.Concretes;

namespace FoodCornerApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "Identity";
                o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                o.LoginPath = "/authentication/login";
                o.AccessDeniedPath = "/admin/auth/login";
            });

            services.AddHttpContextAccessor();

            services.AddUrlHelper();

            services.AddSwaggerGen(opt =>
            {
                opt.CustomSchemaIds(type => type.ToString());
            });

            services.AddScoped<NotFoundExceptionHandler>();


            services.AddAutoMapper(typeof(Program));

            //services.AddScoped<ValidationCurrentUserAttribute>();

            services.AddSignalR();
        

            services.AddHostedService<DeleteExpiredUpUsers>();
            services.AddHostedService<DeleteIsSeenMessages>();


            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
