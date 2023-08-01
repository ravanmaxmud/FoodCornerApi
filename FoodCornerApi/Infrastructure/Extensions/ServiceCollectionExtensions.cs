﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AspNetCore.IServiceCollection.AddIUrlHelper;
using FoodCornerApi.BackgroundServices;
using FoodCornerApi.Infrastructure.Configurations;
using FoodCornerApi.Areas.Admin.Mappers;
using FoodCornerApi.CustomExceptionHandler.Concretes;
using Microsoft.AspNetCore.Mvc;
using FoodCornerApi.CustomExceptionHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodCornerApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
            services.AddControllers().AddJsonOptions(x =>
               x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddMvc();
            services.AddHttpContextAccessor();

            services.AddUrlHelper();

            services.AddSwaggerGen(opt =>
            {
                opt.CustomSchemaIds(type => type.ToString());
            });

            services.AddTransient<ExceptionHandlerCoordinator>();
            services.AddScoped<NotFoundExceptionHandler>();
            services.AddScoped<BadRequestExceptionHandler>();
            services.AddScoped<GeneralExceptionHandler>();


            services.AddAutoMapper(typeof(Program));

            //services.AddScoped<ValidationCurrentUserAttribute>();

            services.AddSignalR();


            services.AddHostedService<DeleteExpiredUpUsers>();
            services.AddHostedService<DeleteIsSeenMessages>();


            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
