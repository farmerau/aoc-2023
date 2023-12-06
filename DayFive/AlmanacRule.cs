using System.Text.Json;

namespace aoc_2023.DayFive;

public record AlmanacRule(long DestinationRangeStart, long SourceRangeStart, long RangeLength)
{
    //public Range SourceRange = new Range {Start = SourceRangeStart, End = SourceRangeStart + (RangeLength - 1)};
    public Lazy<long> DestinationRangeEnd = new Lazy<long>(() => DestinationRangeStart + (RangeLength - 1));
    public Lazy<long> SourceRangeEnd = new Lazy<long>(() => SourceRangeStart + (RangeLength - 1));

    public long GetDestinationForSource(long source)
    {
        return DestinationRangeStart + (source - SourceRangeStart);
    }
    
    public static AlmanacRule FromString(string str)
    {
        long[] parts = str
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        return new AlmanacRule(parts[0], parts[1], parts[2]);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}