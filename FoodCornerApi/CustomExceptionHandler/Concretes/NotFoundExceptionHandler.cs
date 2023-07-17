using FoodCornerApi.CustomExceptionHandler.Abstract;
using FoodCornerApi.DTOs;
using FoodCornerApi.Exceptions;
using System.Net.Mime;
using System.Net;

namespace FoodCornerApi.CustomExceptionHandler.Concretes
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception)
        {
            var notFoundException = (NotFoundException)exception;

            return new ExceptionResultDto(MediaTypeNames.Text.Plain, (int)HttpStatusCode.NotFound, notFoundException.Message);
        }
    }
}
