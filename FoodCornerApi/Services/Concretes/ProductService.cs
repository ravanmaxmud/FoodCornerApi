//using FoodCorner.Areas.Admin.Controllers;
//using FoodCorner.Areas.Admin.ViewModels.Product;
//using FoodCorner.Database.Models;
//using FoodCorner.Migrations;
//using FoodCornerApi.Contracts.File;
//using FoodCornerApi.Database;
//using FoodCornerApi.Database.Models;
//using FoodCornerApi.Services.Abstracts;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.EntityFrameworkCore;
//using System.Net;
//using Product = FoodCornerApi.Database.Models.Product;

//namespace FoodCornerApi.Services.Concretes
//{
//    public class ProductService : IProductService
//    {
//        private readonly IActionDescriptorCollectionProvider _provider;
//        private readonly DataContext _dataContext;
//        private readonly IFileService _fileService;
//        private readonly ILogger<ProductController> _logger;
//        private readonly IHttpContextAccessor _contextAccessor;

//        public ProductService(IActionDescriptorCollectionProvider provider, DataContext dataContext, IFileService fileService, ILogger<ProductController> logger, IHttpContextAccessor contextAccessor)
//        {
//            _provider = provider;
//            _dataContext = dataContext;
//            _fileService = fileService;
//            _logger = logger;
//            _contextAccessor = contextAccessor;
//        }

//        public async Task<List<ListItemViewModel>> GetAllProduct(string? search = null, string? searchBy = null, int page = 1)
//        {
//            var productsQuery = _dataContext.Products.Include(p => p.ProductImages).Include(p => p.ProductCatagories).AsQueryable();

//            if (searchBy == "Name")
//            {
//                productsQuery = productsQuery.Where(p => p.Name.StartsWith(search) || Convert.ToString(p.Price).StartsWith(search) || Convert.ToString((decimal)p.DiscountPrice!).StartsWith(search) || search == null);
//            }
//            else
//            {
//                productsQuery = productsQuery.OrderBy(p => p.Price);
//            }
//            var model = await productsQuery
//              .Include(p => p.ProductImages).OrderByDescending(p => p.CreatedAt)
//              .Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
//              p.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault() != null
//              ? _fileService.GetFileUrl(p.ProductImages.Where(p => p.IsPoster == true).FirstOrDefault().ImageNameFileSystem, UploadDirectory.Product)
//              : string.Empty)).ToListAsync();

//            return model;
//        }

//        public async Task AddProduct(AddViewModel model)
//        {
//            var discountPrice = model.Price * model.DiscountPercent / 100;
//            var lastPrice = model.Price - discountPrice;

//            var product = new Product
//            {
//                Name = model.Name,
//                Description = model.Description,
//                Price = model.Price,
//                CreatedAt = DateTime.Now,
//                UpdateAt = DateTime.Now,
//                DiscountPercent = model.DiscountPercent,
//                DiscountPrice = lastPrice,
//            };

//            await _dataContext.Products.AddAsync(product);

//            if (model.PosterImage is not null)
//            {
//                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Product);


//                var productImage = new ProductImage
//                {
//                    Product = product,
//                    ImageNames = model.PosterImage.FileName,
//                    ImageNameFileSystem = imageNameInSystem,
//                    IsPoster = true,

//                };
//                await _dataContext.ProductImages.AddAsync(productImage);
//            }

//            if (model.AllImages is not null)
//            {
//                foreach (var image in model.AllImages!)
//                {
//                    var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Product);

//                    var productAllImage = new ProductImage
//                    {
//                        Product = product,
//                        ImageNames = image.FileName,
//                        ImageNameFileSystem = allImageNameInSystem,
//                        IsPoster = false
//                    };
//                    await _dataContext.ProductImages.AddAsync(productAllImage);
//                }
//            }

//            ///////////////////////////////////////////////////////////////////
//            foreach (var catagoryId in model.CategoryIds)
//            {
//                var productCatagory = new ProductCatagory
//                {
//                    CatagoryId = catagoryId,
//                    Product = product,
//                };

//                await _dataContext.ProductCatagories.AddAsync(productCatagory);
//            }
//            foreach (var tagId in model.TagIds)
//            {
//                var productTag = new ProductTag
//                {
//                    TagId = tagId,
//                    Product = product,
//                };

//                await _dataContext.ProductTags.AddAsync(productTag);
//            }
//            foreach (var sizeId in model.SizeIds)
//            {
//                var productSize = new ProductSize
//                {
//                    SizeId = sizeId,
//                    Product = product,
//                };

//                await _dataContext.ProductSizes.AddAsync(productSize);
//            }
//        }

//        public async Task<bool> CheckProductSize(List<int> SizeIds, ModelStateDictionary ModelState)
//        {
//            foreach (var sizeId in SizeIds)
//            {
//                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
//                {
//                    ModelState.AddModelError(string.Empty, "Something went wrong");
//                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
//                    return false;
//                }
//            }
//            return true;
//        }

//        public async Task<bool> CheckProductTag(List<int> TagIds, ModelStateDictionary ModelState)
//        {
//            foreach (var tagId in TagIds)
//            {
//                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
//                {
//                    ModelState.AddModelError(string.Empty, "Something went wrong");
//                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
//                    return false;
//                }
//            }
//            return true;
//        }

//        public async Task<bool> CheckProductCategory(List<int> CategoryIds, ModelStateDictionary ModelState)
//        {
//            foreach (var categoryId in CategoryIds)
//            {
//                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
//                {
//                    ModelState.AddModelError(string.Empty, "Something went wrong");
//                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
//                    return false;
//                }
//            }
//            return true;
//        }

//        public async Task<UpdateViewModel> GetUpdatedProduct(Product product, int id)
//        {
//            var model = new UpdateViewModel
//            {
//                Id = product.Id,
//                Name = product.Name,
//                Description = product.Description,
//                Price = product.Price,
//                DiscountPercent = product.DiscountPercent,
//                DiscountPrice = product.DiscountPrice,

//                ImagesUrl = product.ProductImages
//                .Where(p => p.IsPoster == false)
//                .Select(p => new UpdateViewModel.Images(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

//                PosterImgUrls = product.ProductImages.Where(p => p.IsPoster == true)
//                .Select(p => new UpdateViewModel.PosterImages(p.Id, _fileService.GetFileUrl(p.ImageNameFileSystem, UploadDirectory.Product))).ToList(),

//                Catagories = await _dataContext.Categories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
//                CategoryIds = product.ProductCatagories.Select(pc => pc.CatagoryId).ToList(),

//                Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.PersonSize)).ToListAsync(),
//                SizeIds = product.ProductSizes.Select(pc => pc.SizeId).ToList(),

//                //Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
//                //ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),

//                Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync(),
//                TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),

//            };

//            return model;
//        }

//        public async Task UpdateProduct(Product product, UpdateViewModel model)
//        {
//            var discountPrice = model.Price * model.DiscountPercent / 100;
//            var lastPrice = model.Price - discountPrice;

//            if (model.ProductImgIds is null)
//            {
//                foreach (var item in product.ProductImages.Where(p => p.IsPoster == false))
//                {
//                    await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Product);
//                    _dataContext.ProductImages.Remove(item);
//                    await _dataContext.SaveChangesAsync();
//                }

//            }
//            if (model.ProductImgIds is not null)
//            {
//                var removedImg = product.ProductImages.Where(p => p.IsPoster == false).Where(pi => !model.ProductImgIds.Contains(pi.Id)).ToList();

//                foreach (var item in removedImg)
//                {
//                    if (item.Id != 0)
//                    {
//                        var image = await _dataContext.ProductImages.FirstOrDefaultAsync(p => p.Id == item.Id);
//                        await _fileService.DeleteAsync(item.ImageNameFileSystem, UploadDirectory.Product);
//                        _dataContext.ProductImages.Remove(item);
//                        await _dataContext.SaveChangesAsync();
//                    }
//                }
//            }


//            product.Name = model.Name;
//            product.Description = model.Description;
//            product.Price = model.Price;
//            product.UpdateAt = DateTime.Now;
//            product.DiscountPercent = model.DiscountPercent;
//            product.DiscountPrice = lastPrice;

//            #region Catagory
//            var categoriesInDb = product.ProductCatagories.Select(bc => bc.CatagoryId).ToList();
//            var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
//            var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

//            product.ProductCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CatagoryId));

//            foreach (var categoryId in categoriesToAdd)
//            {
//                var productCatagory = new ProductCatagory
//                {
//                    CatagoryId = categoryId,
//                    Product = product,
//                };

//                await _dataContext.ProductCatagories.AddAsync(productCatagory);
//            }
//            #endregion

//            #region Tag
//            var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
//            var tagToRemove = tagInDb.Except(model.TagIds).ToList();
//            var tagToAdd = model.TagIds.Except(tagInDb).ToList();

//            product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


//            foreach (var tagId in tagToAdd)
//            {
//                var productTag = new ProductTag
//                {
//                    TagId = tagId,
//                    Product = product,
//                };

//                await _dataContext.ProductTags.AddAsync(productTag);
//            }
//            #endregion


//            #region Size
//            var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
//            var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
//            var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

//            product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


//            foreach (var sizeId in sizeToAdd)
//            {
//                var productSize = new ProductSize
//                {
//                    SizeId = sizeId,
//                    Product = product,
//                };

//                await _dataContext.ProductSizes.AddAsync(productSize);
//            }

//            #endregion


//            #region Images
//            if (model.PosterImage is not null)
//            {
//                var productImg = await _dataContext.ProductImages.Where(p => p.IsPoster == true).FirstOrDefaultAsync(p => p.ProductId == product.Id);
//                await _fileService.DeleteAsync(productImg.ImageNameFileSystem, UploadDirectory.Product);
//                _dataContext.ProductImages.Remove(productImg);

//                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Product);
//                var productImage = new ProductImage
//                {
//                    Product = product,
//                    ImageNames = model.PosterImage.FileName,
//                    ImageNameFileSystem = imageNameInSystem,
//                    IsPoster = true,

//                };
//                await _dataContext.ProductImages.AddAsync(productImage);
//            }
//            if (model.AllImages is not null)
//            {
//                foreach (var image in model.AllImages!)
//                {
//                    var allImageNameInSystem = await _fileService.UploadAsync(image, UploadDirectory.Product);

//                    var productAllImage = new ProductImage
//                    {
//                        Product = product,
//                        ImageNames = image.FileName,
//                        ImageNameFileSystem = allImageNameInSystem,
//                        IsPoster = false
//                    };
//                    await _dataContext.ProductImages.AddAsync(productAllImage);
//                }
//            }
//            #endregion
//        }
//    }
//}
