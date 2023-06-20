using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Size;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class SizeProfile : Profile
    {
        public SizeProfile()
        {
            CreateMap<Size, ListDto>();
            CreateMap<AddDto, Size>();
        }
    }
}
