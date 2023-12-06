using System.Collections.Concurrent;

namespace aoc_2023.DayFive;

public class DayFive : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        IList<string> lines = input.ToList();
        IList<long> seeds = lines[0]
            .Split(':', StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        IDictionary<string, AlmanacMap> maps = GenerateMaps(lines);

        return LowestLocationForSeeds(seeds, maps);
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        IList<string> lines = input.ToList();
        IList<long> seedRanges = lines[0]
            .Split(':', StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        IEnumerable<long> seeds = seedRanges
            .GroupBy(2)
            .Select(range => Range(range.ElementAt(0), range.ElementAt(1)))
            .SelectMany(l => l)
            .Distinct();

        IDictionary<string, AlmanacMap> maps = GenerateMaps(lines);

        return LowestLocationForSeeds(seeds, maps);
    }

    private static IEnumerable<long> Range(long start, long amount)
    {
        long end = start + amount;
        long value = start;
        while (value < end)
        {
            yield return value;
            value++;
        }
    }

    private static IDictionary<string, AlmanacMap> GenerateMaps(IList<string> lines)
    {
        IDictionary<string, AlmanacMap> maps = new Dictionary<string, AlmanacMap>();

        // Skip 0 since it is handled above.
        bool createNewMap = false;
        AlmanacMap currentMap = null;
        for (int i = 1; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                createNewMap = true;
                continue;
            }

            if (createNewMap)
            {
                currentMap = new AlmanacMap(currentMap, line);
                maps.Add(currentMap.SourceName, currentMap);
                createNewMap = false;
                continue;
            }
            
            currentMap.AddRules(line);
        }

        return maps;
    }

    private static long LowestLocationForSeeds(IEnumerable<long> seeds, IDictionary<string, AlmanacMap> maps)
    {
        long minLocation = long.MaxValue;
        object _lock = new();
        
        Parallel.ForEach(seeds, seed =>
        {
            string? mapKey = "seed";
            long currentSource = seed;
            while (mapKey is not null)
            {
                AlmanacMap map = maps[mapKey];
                currentSource = map.GetDestinationForSource(currentSource);
                if (maps.ContainsKey(map.DestinationName))
                {
                    mapKey = map.DestinationName;
                }
                else
                {
                    mapKey = null;
                }
            }
        
            lock (_lock)
            {
                minLocation = Math.Min(currentSource, minLocation);
            }
        });
        
        return minLocation;
    }
}