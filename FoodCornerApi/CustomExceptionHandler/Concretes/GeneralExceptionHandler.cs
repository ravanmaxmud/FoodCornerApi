﻿using FoodCornerApi.CustomExceptionHandler.Abstract;
using FoodCornerApi.DTOs;
using System.Net.Mime;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace FoodCornerApi.CustomExceptionHandler.Concretes
{
    public class GeneralExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GeneralExceptionHandler> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GeneralExceptionHandler(
            ILogger<GeneralExceptionHandler> logger,
            IWebHostEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public ExceptionResultDto Handle(Exception exception)
        {
            if (_hostingEnvironment.IsProduction())
            {
                string responseMessage = "Internal server error happened.";
                _logger.LogCritical(exception, responseMessage);

                return new ExceptionResultDto(
                    MediaTypeNames.Application.Json,
                    (int)HttpStatusCode.InternalServerError,
                    JsonSerializer.Serialize(responseMessage));
            }
            else
            {
                var responseMessage = GetFailedRequestMessage(_httpContextAccessor.HttpContext!, exception);
                _logger.LogCritical(responseMessage);

                return new ExceptionResultDto(
                    MediaTypeNames.Text.Plain,
                    (int)HttpStatusCode.InternalServerError,
                    responseMessage);
            }
        }

        private string GetFailedRequestMessage(HttpContext context, Exception exception)
        {
            return "Failed Request\n" +
                $"\tSchema: {context.Request?.Scheme}\n" +
                $"\tHost: {context.Request?.Host}\n" +
                $"\tUser: {context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous"}\n" +
                $"\tMethod: {context.Request?.Method}\n" +
                $"\tPath: {context.Request?.Path}\n" +
                $"\tQueryString: {context.Request?.QueryString}\n" +
                $"\tErrorMessage: {exception.Message}\n" +
                $"\tStacktrace (5):\n{exception.StackTrace?.Split('\n').Take(5).Aggregate((a, b) => a + "\n" + b)}";
        }
    }
}
