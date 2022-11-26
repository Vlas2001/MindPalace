using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.MemorizeValues;

namespace MindPalace.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemorizeController: ControllerBase
{
    private readonly ValuesMemorizeService _valuesMemorizeService;

    public MemorizeController(ValuesMemorizeService valuesMemorizeService)
    {
        _valuesMemorizeService = valuesMemorizeService;
    }

    [HttpGet("get-words")]
    public async Task<ActionResult> GetRandomWords(int count)
    {
        return Ok(await _valuesMemorizeService.GetRandomWordsAsync(count));
    }
    
    [HttpGet("get-numbers")]
    public ActionResult GetRandomNumbers(int count, int maxValue)
    {
        return Ok(_valuesMemorizeService.GetRandomNumbers(count, maxValue));
    }
    
    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        await _valuesMemorizeService.Seed();
        return Ok();
    }
}