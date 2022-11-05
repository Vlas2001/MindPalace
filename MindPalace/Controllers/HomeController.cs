using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MindPalace.Controllers
{
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet("data")]
        [AllowAnonymous]
        public IActionResult Data()
        {
            var weatherForecast = new List<string>()
            {
                "Fetch data works",
            };
            return Ok(weatherForecast);
        }
    }
}