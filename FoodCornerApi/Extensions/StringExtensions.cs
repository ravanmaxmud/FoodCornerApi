namespace FoodCornerApi.Extensions
{
    public static class StringExtensions
    {
        public static string? Truncate(this string? value, int maxLenght, string truncationSuffix = "...")
        {
            return value?.Length > maxLenght ?
                 value.Substring(0, maxLenght) + truncationSuffix : value;
        }
    }
}
