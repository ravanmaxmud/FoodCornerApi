using Microsoft.AspNetCore.Mvc;

namespace FoodCornerApi.Areas.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("admin/home")]
    public class AdminHomeController : ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
