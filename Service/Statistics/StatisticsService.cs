using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataContext;
using Dto.Enums;
using Dto.Statistics;
using Microsoft.EntityFrameworkCore;

namespace Service.Statistics;

public class StatisticsService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public StatisticsService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<StatisticDto>> GetAllStatisticsAsync(Guid userId, StatisticType type)
    {
        if (type == StatisticType.All)
        {
            return _mapper.Map<List<StatisticDto>>(await _dbContext.Statistics.Where(s => s.Id == userId).ToListAsync());
        }
        return _mapper.Map<List<StatisticDto>>(await _dbContext.Statistics.Where(s => s.Id == userId && s.Type == type).ToListAsync());
    }

    public async Task AddTestResultAsync(StatisticDto statistic)
    {
        await _dbContext.Statistics.AddAsync(_mapper.Map<Entity.Statistics>(statistic));
        await _dbContext.SaveChangesAsync();
    }
}