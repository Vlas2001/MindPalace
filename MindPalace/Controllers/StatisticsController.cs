using Dto.Enums;
using Dto.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Statistics;

namespace MindPalace.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StatisticsController: ControllerBase
{
    private readonly StatisticsService _statistics;
    
    public StatisticsController(StatisticsService statistics)
    {
        _statistics = statistics;
    }
    
    [HttpGet("get-user-statistics")]
    public async Task<IActionResult> GetAllStatistics(Guid userId, StatisticType type = StatisticType.All)
    {
        return Ok(await _statistics.GetAllStatisticsAsync(userId, type));
    }

    [HttpPut("add-test-result")]
    public async Task<IActionResult> AddTestResult(StatisticDto statistic)
    {
        await _statistics.AddTestResultAsync(statistic);
        return Ok();
    }
}