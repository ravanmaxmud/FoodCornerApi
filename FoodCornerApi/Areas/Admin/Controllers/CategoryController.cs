using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Category;
using FoodCornerApi.Database;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Exceptions;
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
        private readonly IMapper _mapper;

        public CategoryController(DataContext dataContext, IFileService fileService, IMapper mapper)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _mapper = mapper;
        }


        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var categories = await _dataContext.Categories.ToListAsync();
             return Ok(_mapper.Map<List<ListDto>>(categories));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] AddDto dto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var imageNameInSystem = await _fileService.UploadAsync(dto.Backgroundİmage!, Contracts.File.UploadDirectory.Category);
            var category = _mapper.Map<AddDto, Category>(dto);
            category.BackgroundİmageInFileSystem = imageNameInSystem;
            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return Ok($"Category({category.Title}) Aded Sucesifully");
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromForm]UpdateDto dto) 
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return NotFound("Notfound");  
            if (dto.Backgroundİmage is not null) await _fileService.DeleteAsync(category.BackgroundİmageInFileSystem, 
             Contracts.File.UploadDirectory.Category);
            var imageNameInSystem = await _fileService.UploadAsync(dto.Backgroundİmage!, Contracts.File.UploadDirectory.Category);
            var updatedCategory = _mapper.Map(dto,category);
            updatedCategory.BackgroundİmageInFileSystem = imageNameInSystem;
            await _dataContext.SaveChangesAsync();
            return Ok("Category Updated Sucesifully!");
        }

        [HttpDelete("delete/{id}")]
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
