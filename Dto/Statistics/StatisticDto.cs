using System;
using Shared.Enums;

namespace Dto.Statistics;

public class StatisticDto
{
    public Guid UserId { get; set; }
    
    public StatisticType Type { get; set; }
    
    public DateTime Time { get; set; }
    
    public int CorrectAnswersPercentage { get; set; }
}