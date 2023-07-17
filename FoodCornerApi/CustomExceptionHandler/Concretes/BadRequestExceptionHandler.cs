using FoodCornerApi.DTOs;
using FoodCornerApi.Exceptions;
using System.Net.Mime;
using System.Net;
using FoodCornerApi.CustomExceptionHandler.Abstract;

namespace FoodCornerApi.CustomExceptionHandler.Concretes
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception)
        {
            var badRequestException = (BadRequestException)exception;
            return new ExceptionResultDto(MediaTypeNames.Text.Plain, (int)HttpStatusCode.BadRequest, badRequestException.Message);
        }
    }
}
