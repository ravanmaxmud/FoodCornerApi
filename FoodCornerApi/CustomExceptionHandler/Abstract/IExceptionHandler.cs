using FoodCornerApi.DTOs;

namespace FoodCornerApi.CustomExceptionHandler.Abstract
{
    public interface IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception);
    }
}
