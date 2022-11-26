using System;
using System.Collections.Generic;

namespace Service.MemorizeValues;

public static class MemorizeHelper
{
    private static readonly Random Random = new();
    public static List<int> GetRandomIds(int count, int minId, int maxId)
    {
        var list = new List<int>(count);
        for (var i = 0; i < count; i++)
        {
            var val = Random.Next(minId, maxId);
            if (!list.Contains(val))
            {
                list.Add(val);
                continue;
            }
            i--;
        }

        return list;
    }

    public static List<int> GetRandomNumbers(int count, int maxValue)
    {
        var list = new List<int>(count);
        for (var i = 0; i < count; i++)
        {
            list.Add(Random.Next(maxValue));
        }

        return list;
    }
}