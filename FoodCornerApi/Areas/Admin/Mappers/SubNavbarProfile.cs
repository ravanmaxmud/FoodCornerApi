using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.SubNavbar;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class SubNavbarProfile : Profile
    {
        public SubNavbarProfile()
        {
            CreateMap<SubNavbar, ListDto>()
                .ForMember(dest => dest.Navbar, opt => opt.MapFrom(src => src.Navbar.Name));

            CreateMap<AddDto, SubNavbar>();

            CreateMap<UpdateDto, SubNavbar>()
                 .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
