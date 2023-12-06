namespace aoc_2023;

public static class ListExtensions
{
    public static IList<IGrouping<int, TSource>> GroupBy<TSource>(this IList<TSource> source, int itemsPerGroup)
    {
        return source.Select((s, i) => new { Group = i / itemsPerGroup, Item = s })
            .GroupBy(i => i.Group, g => g.Item)
            .ToList();
    }
}