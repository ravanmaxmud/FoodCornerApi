using FoodCornerApi.Areas.Admin.Dtoes.Product;
using FoodCornerApi.Database.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FoodCornerApi.Services.Abstracts
{
    public interface IProductService
    {
        Task<List<ListDto>> GetAllProduct();
        Task AddProduct(AddDto model);
        Task DeleteProduct(Product product);

        //Task<bool> CheckProductSize(List<int> SizeIds, ModelStateDictionary ModelState);
        //Task<bool> CheckProductTag(List<int> TagIds, ModelStateDictionary ModelState);
        //Task<bool> CheckProductCategory(List<int> CategoryIds, ModelStateDictionary ModelState);

        //Task<UpdateViewModel> GetUpdatedProduct(Product product, int id);
        //Task UpdateProduct(Product product, UpdateViewModel model);

    }
}
