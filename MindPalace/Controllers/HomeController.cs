using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MindPalace.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HomeController: BaseController
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Ok");
    }
}