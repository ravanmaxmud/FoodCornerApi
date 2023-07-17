using FoodCornerApi.Areas.Admin.Controllers;
using FoodCornerApi.Database;
using FoodCornerApi.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FoodCornerApi.Areas.Admin.Dtoes.Product;
using AutoMapper;
using FoodCornerApi.Contracts.File;
using FoodCornerApi.Database.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Humanizer;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using FoodCornerApi.Exceptions;

namespace FoodCornerApi.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public ProductService(IActionDescriptorCollectionProvider provider, DataContext dataContext, IFileService fileService, ILogger<ProductController> logger, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _provider = provider;
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async Task<List<ListDto>> GetAllProduct()
        {
            var product = await _dataContext.Products
                 .Include(p => p.ProductCatagories)
                 .Include(p => p.ProductTags)
                 .Include(p => p.ProductSizes)
                 .Include(p => p.ProductImages).ToListAsync();

            var model = _mapper.Map<List<ListDto>>(product);
            return model;
        }

        public async Task AddProduct(AddDto model)
        {
            CheckProductCategory(model.CategoryIds);
            CheckProductTag(model.TagIds);
            CheckProductSize(model.SizeIds);

            var lastPrice = CalcDiscountPrice(model);
            model.DiscountPrice = lastPrice;
            var product = _mapper.Map<Product>(model);

            if (model.PosterImage is not null)
            {
                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Product);
                await _dataContext.ProductImages.AddAsync(_mapper.Map<ProductImage>((model.PosterImage, product, true, imageNameInSystem)));
            }

            if (model.AllImages is not null)
            {
                foreach (var image in model.AllImages!)
                {
                    var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Product);
                    await _dataContext.ProductImages.AddAsync(_mapper.Map<ProductImage>((image, product, true, allImageNameInSystem)));
                }
            }
            foreach (var catagoryId in model.CategoryIds)
            {
                await _dataContext.ProductCatagories.AddAsync(_mapper.Map<ProductCatagory>((catagoryId, product)));
            }
            foreach (var tagId in model.TagIds)
            {
                await _dataContext.ProductTags.AddAsync(_mapper.Map<ProductTag>((tagId, product)));
            }
            foreach (var sizeId in model.SizeIds)
            {
                await _dataContext.ProductSizes.AddAsync(_mapper.Map<ProductSize>((sizeId, product)));
            }
            await _dataContext.Products.AddAsync(product);
        }

        private void CheckProductSize(List<int> SizeIds)
        {
            foreach (var sizeId in SizeIds)
            {
                if (!_dataContext.Sizes.Any(c => c.Id == sizeId))
                {
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    throw new NotFoundException("Size", sizeId);
                }
            }
        }
        private void CheckProductTag(List<int> TagIds)
        {
            foreach (var tagId in TagIds)
            {
                if (!_dataContext.Tags.Any(c => c.Id == tagId))
                {
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    throw new NotFoundException("Tag", tagId);
                }
            }
        }
        private void CheckProductCategory(List<int> CategoryIds)
        {
            foreach (var categoryId in CategoryIds)
            {
                if (!_dataContext.Categories.Any(c => c.Id == categoryId))
                {
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    throw new NotFoundException("Category", categoryId);
                }
            }
        }

        public async Task DeleteProduct(Product product)
        {
            foreach (var productImage in product.ProductImages!)
            {
                await _fileService.DeleteAsync(productImage.ImageNames, Contracts.File.UploadDirectory.Product);
            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
        }

        private int? CalcDiscountPrice(AddDto model) 
        {
            var discountPrice = model.Price * model.DiscountPercent / 100;
            var lastPrice = model.Price - discountPrice;
            return lastPrice;
        }




        //public async Task<UpdateViewModel> GetUpdatedProduct(Product product, int id)
        //{
        //    var model = new UpdateViewModel
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        DiscountPercent = product.DiscountPercent,
        //        DiscountPrice = product.DiscountPrice,

        //        ImagesUrl = product.ProductImages
        //        .Where(p => p.IsPoster == false)
        //        .Select(p => new UpdateViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

        //        PosterImgUrls = product.ProductImages.Where(p => p.IsPoster == true)
        //        .Select(p => new UpdateViewModel.PosterImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

        //        Catagories = await _dataContext.Categories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
        //        CategoryIds = product.ProductCatagories.Select(pc => pc.CatagoryId).ToList(),

        //        Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.PersonSize)).ToListAsync(),
        //        SizeIds = product.ProductSizes.Select(pc => pc.SizeId).ToList(),

        //        //Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
        //        //ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),

        //        Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync(),
        //        TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),

        //    };

        //    return model;
        //}

        public async Task UpdateProduct(Product product, UpdateDto model)
        {
            var discountPrice = model.Price * model.DiscountPercent / 100;
            var lastPrice = model.Price - discountPrice;

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.UpdateAt = DateTime.Now;
            product.DiscountPercent = model.DiscountPercent;
            product.DiscountPrice = lastPrice;

            #region Catagory
            var categoriesInDb = product.ProductCatagories.Select(bc => bc.CatagoryId).ToList();
            var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
            var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();
            product.ProductCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CatagoryId));
            foreach (var categoryId in categoriesToAdd)
            {
                await _dataContext.ProductCatagories.AddAsync(_mapper.Map<ProductCatagory>((categoryId, product)));
            }
            #endregion
            #region Tag
            var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
            var tagToRemove = tagInDb.Except(model.TagIds).ToList();
            var tagToAdd = model.TagIds.Except(tagInDb).ToList();
            product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));
            foreach (var tagId in tagToAdd)
            {
                await _dataContext.ProductTags.AddAsync(_mapper.Map<ProductTag>((tagId, product)));
            }
            #endregion
            #region Size
            var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
            var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
            var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();
            product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));
            foreach (var sizeId in sizeToAdd)
            {
                await _dataContext.ProductSizes.AddAsync(_mapper.Map<ProductSize>((sizeId, product)));
            }

            #endregion
            #region Images
            if (model.PosterImage is not null)
            {
                var productImg = await _dataContext.ProductImages.Where(p => p.IsPoster == true).FirstOrDefaultAsync(p => p.ProductId == product.Id);
                await _fileService.DeleteAsync(productImg.ImageNameFileSystem, UploadDirectory.Product);
                _dataContext.ProductImages.Remove(productImg);
                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Product);
                await _dataContext.ProductImages.AddAsync(_mapper.Map<ProductImage>((model.PosterImage, product, true, imageNameInSystem)));
            }
            if (model.AllImages is not null)
            {
                foreach (var image in model.AllImages!)
                {
                    var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Product);
                    await _dataContext.ProductImages.AddAsync(_mapper.Map<ProductImage>((image, product, true, allImageNameInSystem)));
                }
            }
            #endregion
        }
    }
}
