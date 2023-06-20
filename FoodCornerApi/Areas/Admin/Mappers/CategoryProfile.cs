using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Category;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Services.Abstracts;

namespace FoodCornerApi.Areas.Admin.Mappers
{
    public class CategoryProfile : Profile
    {
        private readonly IFileService _fileService;
        public CategoryProfile(IFileService fileService)
        {
            _fileService = fileService;
        }
        public CategoryProfile()
        {
            CreateMap<Category, ListDto>()
                 .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Title : null))
                 .ForMember(dest => dest.BackgroundİmageUrl, opt => opt.MapFrom(src => src.BackgroundİmageInFileSystem));

            CreateMap<AddDto, Category>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Backgroundİmage, opt => opt.MapFrom(src => src.Backgroundİmage!.FileName));

            CreateMap<UpdateDto, Category>()
             .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Backgroundİmage, opt => opt.MapFrom(src => src.Backgroundİmage!.FileName));
        }

    }
    
}
