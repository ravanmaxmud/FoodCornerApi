using System.ComponentModel.DataAnnotations;

namespace FoodCornerApi.Areas.Admin.Dtoes.Navbar
{
    public class AddDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string ToURL { get; set; }

        //public List<UrlDto>? Urls { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public bool IsViewHeader { get; set; }
        [Required]
        public bool IsViewFooter { get; set; }


        //public class UrlDto
        //{
        //    public UrlDto(string? routName, string? path)
        //    {
        //        RoutName = routName;
        //        Path = path;
        //    }
        //    public UrlDto()
        //    {

        //    }

        //    public string? RoutName { get; set; }
        //    public string? Path { get; set; }
        //}
    }
}
