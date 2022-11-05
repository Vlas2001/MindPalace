using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MindPalace.Controllers
{
    [Authorize]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult Data()
        {
            return Ok("Result");
        }
    }
}