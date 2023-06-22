using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Navbar;
using FoodCornerApi.Database.Models;


namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class NavbarProfile :Profile
    {
        public NavbarProfile()
        {
            CreateMap<Navbar, ListDto>();

            //CreateMap<UrlDto, UrlDto>(); // Map UrlDto to itself (optional)
            CreateMap<AddDto, Navbar>(); 
            CreateMap<UpdateDto, Navbar>()
                  .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now)); 
        }
    }
}
