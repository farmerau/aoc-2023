namespace aoc_2023.DaySix;

public class DaySix : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        long result = 1;
        string[] raceInfo = input.ToArray();
        int[] timings = ParseLinePartOne(raceInfo[0]);
        int[] distances = ParseLinePartOne(raceInfo[1]);

        for (int i = 0; i < timings.Length; i++)
        {
            int waysToWin = 0;
            int timing = timings[i];
            int distanceToBeat = distances[i];
            for (int j = 0; j <= timing; j++)
            {
                int distanceTraveled = (timing - j) * j;
                if (distanceTraveled > distanceToBeat)
                {
                    waysToWin++;
                }
                else if (waysToWin > 0)
                {
                    break;
                }
            }

            result *= waysToWin;
        }

        return result;
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        string[] raceInfo = input.ToArray();
        long duration = ParseLinePartTwo(raceInfo[0]);
        long distanceToBeat = ParseLinePartTwo(raceInfo[1]);
        long lastDistanceTraveled = 0;
        long waysToWin = 0;
        for (long i = 0; i < duration; i++)
        {
            long distanceTraveled = (duration - i) * i;
            if (distanceTraveled > distanceToBeat)
            {
                waysToWin++;
                lastDistanceTraveled = distanceTraveled;
            }
            else if (distanceTraveled < lastDistanceTraveled)
            {
                break;
            }
        }

        return waysToWin;
    }
    
    private int[] ParseLinePartOne(string line) => line
        .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        .Skip(1)
        .Select(int.Parse)
        .ToArray();

    private long ParseLinePartTwo(string line) => long.Parse(line
        .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        .Skip(1)
        .Aggregate("", (s, _s) => $"{s}{_s}"));

}