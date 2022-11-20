using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataContext;
using Dto.Statistics;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

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
        return _mapper.Map<List<StatisticDto>>(await _dbContext.Statistics.ToListAsync());
    }

    public async Task AddTestResultAsync(StatisticDto statistic)
    {
        await _dbContext.Statistics.AddAsync(_mapper.Map<Entity.Statistics>(statistic));
        await _dbContext.SaveChangesAsync();
    }
}