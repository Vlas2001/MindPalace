using Microsoft.AspNetCore.Mvc;

namespace MindPalace.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult CreateActionResultFromData(object? result)
    {
        return result is null ? NotFound() : Ok(result);
    }
}