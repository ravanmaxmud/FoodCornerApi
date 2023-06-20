using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Size;
using FoodCornerApi.Database;
using FoodCornerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("admin/size")]
    public class SizeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<SizeController> _logger;
        private readonly IMapper _mapper;

        public SizeController(DataContext dataContext, ILogger<SizeController> logger, IMapper mapper)
        {
            _dataContext = dataContext;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var sizes = await _dataContext.Sizes.ToListAsync();
            return Ok(_mapper.Map<List<ListDto>>(sizes));
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm]AddDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var size = _mapper.Map<AddDto, Size>(dto);
            await _dataContext.Sizes.AddAsync(size);
            await _dataContext.SaveChangesAsync();
            return Ok("Size Aded Sucesifully!");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s=> s.Id == id);
            if (size == null) return NotFound();
            _dataContext.Sizes.Remove(size);
            await _dataContext.SaveChangesAsync();
            return Ok("Size Deleted Sucesifully!");
        }

    }
}
