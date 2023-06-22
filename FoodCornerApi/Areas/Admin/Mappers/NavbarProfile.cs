using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Navbar;
using FoodCornerApi.Database.Models;
using static FoodCornerApi.Areas.Admin.Dtoes.Navbar.AddDto;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class NavbarProfile :Profile
    {
        public NavbarProfile()
        {
            CreateMap<Navbar, ListDto>();

            //CreateMap<UrlDto, UrlDto>(); // Map UrlDto to itself (optional)
            CreateMap<AddDto, Navbar>(); // Map AddDto to YourEntityClass
            CreateMap<UpdateDto, Navbar>()
                  .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now)); ;
        }
    }
}
