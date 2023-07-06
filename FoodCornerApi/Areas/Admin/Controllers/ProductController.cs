using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Product;
using FoodCornerApi.Database;
using FoodCornerApi.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("admin/product")]
    //[Authorize(Roles = "admin")]
    public class ProductController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IActionDescriptorCollectionProvider provider, DataContext dataContext, IFileService fileService, ILogger<ProductController> logger, IMapper mapper, IProductService productService)
        {
            _provider = provider;
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            return Ok(await _productService.GetAllProduct());
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] AddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productService.AddProduct(dto);
            await _dataContext.SaveChangesAsync();
            return Ok("Product Aded Sucesifully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            var product = await _dataContext.Products.Include(p=> p.ProductImages).FirstOrDefaultAsync(x => x.Id == id);
            if (product is null) return NotFound("Product Not Found");
            await _productService.DeleteProduct(product!);
            return Ok("Product Remove Sucesifully");

        }


    }
}
