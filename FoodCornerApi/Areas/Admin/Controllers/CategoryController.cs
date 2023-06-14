using FoodCornerApi.Areas.Admin.Dtoes.Category;
using FoodCornerApi.Database;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("admin/category")]
    //[Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public CategoryController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        [HttpGet("List",Name ="admin-category-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Categories.Select(c => new ListDto(c.Id, c.Title, c.Parent.Title,
                _fileService.GetFileUrl(c.BackgroundİmageInFileSystem, Contracts.File.UploadDirectory.Category))).ToListAsync();

            return Ok(model);
        }

        [HttpPost("Add",Name ="admin-category-add")]
        public async Task<IActionResult> Add([FromForm] AddDto dto)
        {

            if (!ModelState.IsValid) 
            {
              return BadRequest(ModelState);
            }

                      
            var imageNameInSystem = await _fileService.UploadAsync(dto.Backgroundİmage, Contracts.File.UploadDirectory.Category);


            var category = new Category
            {

                Title = dto.Title,
                ParentId = dto.CategoryId,
                Backgroundİmage = dto.Backgroundİmage.FileName,
                BackgroundİmageInFileSystem = imageNameInSystem

            };
            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();


            return Ok($"Caategory({category.Title}) Aded Sucesifully");
        }



        [HttpDelete("delete/{id}",Name ="admin-category-delete")]
        public async Task<IActionResult> Delete(int id) 
        {
             var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            var allCategory = await _dataContext.Categories.ToListAsync();
            if (category is null){ return NotFound(); }

            foreach (var cate in allCategory)
            {
                if (category.Id == cate.Id)
                {
                    await _fileService.DeleteAsync(cate.BackgroundİmageInFileSystem, Contracts.File.UploadDirectory.Category);
                    _dataContext.Remove(cate);
                }
            }

            await _fileService.DeleteAsync(category.BackgroundİmageInFileSystem,Contracts.File.UploadDirectory.Category);
            _dataContext.Remove(category);
            await _dataContext.SaveChangesAsync();
            
            return Ok("Category Removed Sucesifully");
        
        }
    }
}
