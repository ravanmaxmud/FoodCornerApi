using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.SubNavbar;
using FoodCornerApi.Database;
using FoodCornerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("admin/subNavBar")]
    public class SubNavbarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public SubNavbarController(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List() 
        {
            var subNav = await _dataContext.SubNavbars.Include(n => n.Navbar).ToListAsync();
            return Ok(_mapper.Map<List<ListDto>>(subNav));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]AddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_dataContext.Navbars.Any(n => n.Id == dto.NavbarId)) return NotFound();
            var subNav = _mapper.Map<AddDto, SubNavbar>(dto);
            await _dataContext.AddAsync(subNav);
            await _dataContext.SaveChangesAsync();
            return Ok("Sub Navbar Aded Sucesifully");
        }



        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDto dto)
        {
            if (!ModelState.IsValid)  return BadRequest(ModelState);
            var subNav = await _dataContext.SubNavbars.FirstOrDefaultAsync(s=> s.Id == id);
            if (subNav == null) return NotFound();
            var updatedSubNav = _mapper.Map(dto, subNav);
            await _dataContext.SaveChangesAsync();
            return Ok("SubNavbar Update Sucesifully");
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subNav = await _dataContext.SubNavbars.FirstOrDefaultAsync(s=> s.Id == id);
            if (subNav == null) return NotFound();
            _dataContext.SubNavbars.Remove(subNav);
            await _dataContext.SaveChangesAsync();
            return Ok("Sub Navbar Removed Sucesifully");
        }
    }
}
