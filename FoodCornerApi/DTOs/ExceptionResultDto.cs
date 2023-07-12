namespace FoodCornerApi.DTOs
{
    public class ExceptionResultDto
    {
        public string ContentType { get; set; } = default!;
        public int HttpStatusCode { get; set; } = default!;
        public string Message { get; set; } = default!;

        public ExceptionResultDto(string contentType, int httpStatusCode, string message)
        {
            ContentType = contentType;
            HttpStatusCode = httpStatusCode;
            Message = message;
        }

    }
}
