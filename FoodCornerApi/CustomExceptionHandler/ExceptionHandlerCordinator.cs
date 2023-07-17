using FoodCornerApi.CustomExceptionHandler.Abstract;
using FoodCornerApi.CustomExceptionHandler.Concretes;
using FoodCornerApi.DTOs;
using FoodCornerApi.Exceptions;

namespace FoodCornerApi.CustomExceptionHandler
{
    public class ExceptionHandlerCoordinator
    {
        private Dictionary<Type, IExceptionHandler> _exceptionHandlers = new Dictionary<Type, IExceptionHandler>();

        public ExceptionHandlerCoordinator(
            NotFoundExceptionHandler notFoundExceptionHandler,
            BadRequestExceptionHandler badRequestExceptionHandler,
            GeneralExceptionHandler generalExceptionHandler
            )
        {
            RegisterHandler<NotFoundException>(notFoundExceptionHandler);
            RegisterHandler<BadRequestException>(badRequestExceptionHandler);
            RegisterHandler<Exception>(generalExceptionHandler);
        }

        public void RegisterHandler<TException>(IExceptionHandler handler)
            where TException : Exception
        {
            ArgumentNullException.ThrowIfNull(handler);

            _exceptionHandlers[typeof(TException)] = handler;
        }

        public ExceptionResultDto Handle(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            var exceptionType = exception is ApplicationException ? exception.GetType() : typeof(Exception);

            try
            {
                return _exceptionHandlers[exceptionType].Handle(exception);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception($"Exception handler for ({exceptionType.Name}) is not registered in coordinator. \n {e.Message}");
            }
        }

    }
}
