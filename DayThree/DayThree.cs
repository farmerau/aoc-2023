namespace aoc_2023.DayThree;

public class DayThree : ISolution
{
    private static readonly int[] AdjacencyMatrix = {-1, 0, 1};
    
    public long SolvePartOne(IEnumerable<string> input)
    {
        Engine engine = Engine.Build(input);
        HashSet<NumberValue> partNumbers = new();

        foreach ((int x, int y) symbol in engine.Symbols)
        {
            foreach (Space adjacentSpace in GetAdjacentSpaces(engine, symbol))
            {
                if (adjacentSpace is NumberValue partNumber)
                {
                    partNumbers.Add(partNumber);
                }
            }
        }

        return partNumbers.Select(n => n.Value).Sum();
    }

    private static IEnumerable<Space> GetAdjacentSpaces(Engine engine, (int x, int y) coordinate)
    {
        foreach (int xAdd in AdjacencyMatrix)
        {
            foreach (int yAdd in AdjacencyMatrix)
            {
                if (engine.TryGetValue((coordinate.x + xAdd, coordinate.y + yAdd), out Space space) && space is not null)
                {
                    yield return space;
                }
            }
        }
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        long sumOfGearRatios = 0;
        Engine engine = Engine.Build(input);
        foreach ((int x, int y) possibleGear in engine.Symbols)
        {
            if (engine[possibleGear] is SymbolValue {Value: '*'})
            {
                HashSet<NumberValue> numbers = GetAdjacentSpaces(engine, possibleGear)
                    .Where(s => s is NumberValue)
                    .Select(s => s as NumberValue)
                    .ToHashSet();

                if (numbers.Count == 2)
                {
                    long gearRatio = numbers.Select(n => n.Value).Aggregate((n1, n2) => n1 * n2);
                    sumOfGearRatios += gearRatio;
                }
            }
        }

        return sumOfGearRatios;
    }
}