using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Product;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Services.Abstracts;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ListDto>()
                 .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductImages!
                      .Where(p=> p.IsPoster == true).FirstOrDefault()!.ImageNameFileSystem));
        }
    }
}
