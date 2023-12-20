namespace aoc_2023.DayEight;

public class DayEight : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        string start = "AAA", end = "ZZZ";
        NodeManager nodeMan = new();
        TurnManager turnMan = new(input.First().ToArray());

        foreach (string line in input.Skip(2))
        {
            nodeMan.CreateNewNodeFromStr(line);
        }
        
        return DetermineStepsToAnEnd(nodeMan, turnMan, start, new []{ end });
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        HashSet<string> starts = new();
        HashSet<string> ends = new();
        List<long> steps = new();
        NodeManager nodeMan = new();
        TurnManager turnMan = new(input.First().ToArray());

        foreach (string line in input.Skip(2))
        {
            Node created = nodeMan.CreateNewNodeFromStr(line);
            if (created.Identity.EndsWith('A'))
            {
                starts.Add(created.Identity);
            }
            else if (created.Identity.EndsWith('Z'))
            {
                ends.Add(created.Identity);
            }
        }

        string[] endsArr = ends.ToArray();
        foreach (string start in starts)
        {
            turnMan.Reset();
            steps.Add(DetermineStepsToAnEnd(nodeMan, turnMan, start, endsArr));
        }

        return MathUtils.LeastCommonMultiple(steps);
    }

    private long DetermineStepsToAnEnd(NodeManager nodeMan, TurnManager turnMan, string start, string[] ends)
    {
        long steps = 0;
        Node? currentNode = nodeMan.Nodes[start];
        while (!ends.Contains(currentNode?.Identity))
        {
            steps++;
            char turn = turnMan.NextTurn();
            currentNode = turn switch
            {
                'L' => currentNode?.Left,
                _ => currentNode?.Right
            };
        }

        return steps;
    }
}