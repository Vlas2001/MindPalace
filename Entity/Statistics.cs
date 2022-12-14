using System;
using Dto.Enums;

namespace Entity;

public class Statistics
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public StatisticType Type { get; set; }
    
    public DateTime Time { get; set; }
    
    public int CorrectAnswersPercentage { get; set; }
    
    public int CountOfResult { get; set; }
}