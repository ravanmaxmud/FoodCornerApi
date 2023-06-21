using AutoMapper;
using FoodCornerApi.Areas.Admin.Dtoes.Tag;
using FoodCornerApi.Database;
using FoodCornerApi.Database.Models;
using FoodCornerApi.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("admin/tag")]
    public class TagController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TagController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var tags = await _dataContext.Tags.ToListAsync();
            return Ok(_mapper.Map<List<ListDto>>(tags));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]AddDto dto) 
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };

            var tag = _mapper.Map<AddDto,Tag>(dto);
            await _dataContext.Tags.AddRangeAsync(tag);
            await _dataContext.SaveChangesAsync();
            return Ok("Tag Aded Sucesifully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(s => s.Id == id);
            if (tag == null) return NotFound();
            _dataContext.Tags.Remove(tag);
            await _dataContext.SaveChangesAsync();
            return Ok("Tag Deleted Sucesifully!");
        }
    }
}
