using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext;
using Microsoft.EntityFrameworkCore;

namespace Service.MemorizeValues;

public class ValuesMemorizeService
{
    private readonly DataBaseContext _dbContext;

    public ValuesMemorizeService(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<string>> GetRandomWordsAsync(int count)
    {
        var idsList = MemorizeHelper.GetRandomIds(count, _dbContext.MemorizeItems.Min(item => item.Id),
            _dbContext.MemorizeItems.Max(item => item.Id));
        return  await _dbContext.MemorizeItems.Where(val => idsList.Contains(val.Id)).Select(item => item.Item).ToListAsync();
    }

    public List<int> GetRandomNumbers(int count, int maxNumber)
    {
        return MemorizeHelper.GetRandomNumbers(count, maxNumber);
    }

    public async Task Seed()
    {
        await MemorizeValues.Seed.SeedData(_dbContext);
    }
}