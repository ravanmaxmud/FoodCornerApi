using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Navbar;
using FoodCornerApi.Database;
using FoodCornerApi.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    
    [ApiController]
    [Area("admin")]
    [Route("admin/navBar")]
    public class NavbarController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _provider;
        private readonly DataContext _dataContext;
        private readonly ILogger<NavbarController> _logger;
        private readonly IMapper _mapper;
        public NavbarController(DataContext dataContext, IActionDescriptorCollectionProvider provider, ILogger<NavbarController> logger, IMapper mapper)
        {
            _dataContext = dataContext;
            _provider = provider;
            _logger = logger;
            _mapper = mapper;
        }


        #region Urls
        [HttpGet("urls")]
        public async Task<IActionResult> GetUrlsForGetMethods()
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type));
            var urls = new List<string>();

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods().Where(m => m.CustomAttributes.Any(attr => attr.AttributeType == typeof(HttpGetAttribute)));

                foreach (var method in methods)
                {
                    var url = $"/{controllerType.Name.Replace("Controller", "")}/{method.Name}";

                    urls.Add(url);
                }
            }
            return Ok(urls);
        }
        #endregion

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var navBar = await _dataContext.Navbars.ToListAsync();
            return Ok(_mapper.Map<List<ListDto>>(navBar));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] AddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var navbar = _mapper.Map<AddDto,Navbar>(dto);
            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();
            return Ok("NavbarAded Sucesifully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateDto dto)
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar == null) return NotFound();
            var updatedNavbar = _mapper.Map(dto, navbar);
            await _dataContext.SaveChangesAsync();
            return Ok("Navbar Updated Sucesifully!");

        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n=> n.Id == id);
            if (navbar == null) return NotFound();
            _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();
            return Ok("Navbar Removed Sucesifully");
        }


    }
}
