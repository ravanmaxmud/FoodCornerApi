using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Product;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Services.Abstracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            ////////////////////////////////////////List/////////////////////////////////////////////////////////
            CreateMap<Product, ListDto>()
                  .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductImages!
                      .Where(p => p.IsPoster == true).FirstOrDefault()!.ImageNameFileSystem));

            //////////////////////////////////////Add////////////////////////////////////////////////////////

            CreateMap<AddDto, Product>()
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(d => d.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));


            CreateMap<(int model, Product product), ProductCatagory>()
                 .ForMember(d => d.CatagoryId, opt => opt.MapFrom(src => src.model))
                 .ForMember(d => d.Product, opt => opt.MapFrom(src => src.product));

            CreateMap<(int model, Product product), ProductTag>()
               .ForMember(d => d.TagId, opt => opt.MapFrom(src => src.model))
               .ForMember(d => d.Product, opt => opt.MapFrom(src => src.product));

            CreateMap<(int model, Product product), ProductSize>()
               .ForMember(d => d.SizeId, opt => opt.MapFrom(src => src.model))
               .ForMember(d => d.Product, opt => opt.MapFrom(src => src.product));


            CreateMap<(IFormFile ProductImage, Product product, bool IsPoster, string imageNameInSystem), ProductImage>()
             .ForMember(d => d.IsPoster, opt => opt.MapFrom(src => src.IsPoster))
              .ForMember(d => d.ImageNames, opt => opt.MapFrom(src => src.ProductImage.FileName))
              .ForMember(d => d.ImageNameFileSystem, opt => opt.MapFrom(src => src.imageNameInSystem))
             .ForMember(d => d.Product, opt => opt.MapFrom(src => src.product));

        }

    }
}
