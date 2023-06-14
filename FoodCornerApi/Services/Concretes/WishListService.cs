//using FoodCorner.Areas.Client.ViewModels.Wishlist;
//using FoodCorner.Areas.Client.ViewModels.Home.Modal;
//using Microsoft.EntityFrameworkCore;
//using System.Text.Json;
//using FoodCornerApi.Services.Abstracts;
//using FoodCornerApi.Contracts.File;
//using FoodCornerApi.Database.Models;
//using FoodCornerApi.Database;

//namespace FoodCornerApi.Services.Concretes
//{
//    public class WishListService : IWishListService
//    {
//        private readonly DataContext _dataContext;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IFileService _fileService;



//        public WishListService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IFileService fileService)
//        {
//            _dataContext = dataContext;
//            _httpContextAccessor = httpContextAccessor;
//            _fileService = fileService;
//        }

//        public async Task<List<WishlistCookieViewModel>> AddBasketProductAsync(Product product)
//        {


//            return await AddCookie();


//            async Task<List<WishlistCookieViewModel>> AddCookie()
//            {
//                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["wishList"];

//                var productCookieViewModel = productCookieValue is not null
//                   ? JsonSerializer.Deserialize<List<WishlistCookieViewModel>>(productCookieValue)
//                   : new List<WishlistCookieViewModel> { };

//                var cookieViewModel = productCookieViewModel!.FirstOrDefault(pc => pc.Id == product.Id);

//                if (cookieViewModel is null)
//                {

//                    productCookieViewModel!.Add
//                           (new WishlistCookieViewModel(product.Id, product.Name, product.ProductImages!.Take(1).FirstOrDefault() != null
//                              ? _fileService.GetFileUrl(product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameFileSystem, UploadDirectory.Product)
//                                  : string.Empty,
//                                  product.DiscountPrice == null ? product.Price : (decimal)product.DiscountPrice));
//                }

//                _httpContextAccessor.HttpContext.Response.Cookies.Append("wishList", JsonSerializer.Serialize(productCookieViewModel));

//                return productCookieViewModel;
//            };
//        }
//    }
//}
