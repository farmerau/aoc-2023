namespace aoc_2023.DayFive;

public class AlmanacMap
{
    public AlmanacMap SourceMap { get; }
    public string SourceName { get; }
    public string DestinationName { get; set; }

    public SortedList<long, AlmanacRule> Rules = new ();
    public AlmanacMap(AlmanacMap previousMap, string heading)
    {
        SourceMap = previousMap;
        string descriptor = heading.Split(' ')[0];
        string[] mapParts = descriptor.Split('-');
        SourceName = mapParts[0];
        DestinationName = mapParts[2];
    }

    public void AddRules(string ruleSet)
    {
        AlmanacRule rule = AlmanacRule.FromString(ruleSet);
        Rules.Add(rule.SourceRangeStart, rule);
    }

    public long GetDestinationForSource(long source)
    {
        long result = source;
        if (source < Rules.Values[0].SourceRangeStart || source > Rules.Values[Rules.Count - 1].SourceRangeEnd.Value)
        {
            return source;
        }
        
        AlmanacRule?
            rule = //Rules.Values.FirstOrDefault(r => r.SourceRangeStart <= source && r.SourceRangeEnd.Value >= source);
        GetApplicableRule(0, Rules.Count - 1, source);
        if (rule is not null)
        {
            result = rule.GetDestinationForSource(source);
        }

        return result;
    }

    private AlmanacRule? GetApplicableRule(int lower, int upper, long search)
    {
        HashSet<int> exploredMiddles = new();
        while(upper >= 0 && lower >= 0 && upper < Rules.Count && lower < Rules.Count)
        {
            int middle = lower + (upper - lower) / 2;
            if (!exploredMiddles.Add(middle))
            {
                return null;
            }
            AlmanacRule currentRule = Rules.ElementAt(middle).Value;
            if (currentRule.SourceRangeStart <= search)
            {
                if (currentRule.SourceRangeStart == search || currentRule.SourceRangeEnd.Value >= search)
                {
                    return currentRule;
                }
            
                if (currentRule.SourceRangeEnd.Value < search)
                {
                    lower = middle + 1;
                    continue;
                }
            }
            else if (currentRule.SourceRangeStart > search)
            {
                upper = middle - 1;
                continue;
            }

            return null;
        }

        return null;
    }

    public override string ToString()
    {
        return $"{SourceName} to {DestinationName}: \n" + Rules.Aggregate(string.Empty, (s, s1) => $"{(s == string.Empty ? s1 : ($"{s}\n{s1}"))}") + "\n";
    }
}