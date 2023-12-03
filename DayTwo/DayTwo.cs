namespace aoc_2023.DayTwo;

public class DayTwo : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        long result = 0;
        Dictionary<string, int> limits = new()
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };
        
        foreach (string line in input)
        {
            bool gameIsImpossible = false;
            Game game = Game.FromString(line);
            foreach (DrawingSet drawSet in game.DrawingSets)
            {
                foreach ((string? color, int quantity) in drawSet.Drawings)
                {
                    if (quantity > limits[color])
                    {
                        gameIsImpossible = true;
                        break;
                    }
                }

                if (gameIsImpossible)
                {
                    break;
                }
            }

            if (!gameIsImpossible)
            {
                result += game.Id;
            }
        }

        return result;
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        long result = 0;
        List<string> colors = new () {"red", "green", "blue"};
        foreach (string line in input)
        {
            Game game = Game.FromString(line);
            long power = 1;
            foreach (string color in colors)
            {
                int max = game.DrawingSets.Max(d => d.Drawings.TryGetValue(color, out int quantity) ? quantity : 0);
                power *= max;
            }

            result += power;
        }

        return result;
    }
}