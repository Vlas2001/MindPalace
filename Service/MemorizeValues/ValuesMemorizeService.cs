using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext;

namespace Service.MemorizeValues;

public class ValuesMemorizeService
{
    private readonly DataBaseContext _dbContext;
    private static readonly Random Random = new();
    public ValuesMemorizeService(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<string> GetRandomWordsAsync(int count)
    {
        var listOfWords = new List<string>();
        for (var i = 0; i < count; i++)
        {
            var randomId = Random.Next(
                _dbContext.MemorizeItems.Min(item => item.Id),
                _dbContext.MemorizeItems.Max(item => item.Id));
            var word = _dbContext.MemorizeItems.FirstOrDefault(item => item.Id == randomId)?.Item;
            
            if (!listOfWords.Contains(word))
            {
                listOfWords.Add(word);
                continue;
            }
            
            i--;
        }

        return listOfWords;
    }

    public List<int> GetRandomNumbers(int count, int maxNumber)
    {
        var list = new List<int>(count);
        for (var i = 0; i < count; i++)
        {
            list.Add(Random.Next(maxNumber));
        }
        return list;
    }

    public async Task Seed()
    {
        await MemorizeValues.Seed.SeedData(_dbContext);
    }
}