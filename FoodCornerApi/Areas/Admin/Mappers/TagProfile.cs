using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Tag;
using FoodCornerApi.Database.Models;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, ListDto>();
            CreateMap<AddDto, Tag>();
        }
    }
}
