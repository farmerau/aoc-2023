namespace aoc_2023.DayFour;

public class DayFour : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        long result = 0;
        foreach (string card in input)
        {
            Game game = Game.FromString(card);
            game.YourNumbers.IntersectWith(game.WinningNumbers);
            int cardScore = game.YourNumbers.Any() ? 1 : 0;
            foreach (int _ in game.YourNumbers.Skip(1))
            {
                cardScore *= 2;
            }
            result += (cardScore);
        }

        return result;
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        IDictionary<int, int> copies = new Dictionary<int, int>();
        foreach (string s in input)
        {
            Game game = Game.FromString(s);
            if (!copies.ContainsKey(game.Id))
            {
                copies[game.Id] = 0;
            }

            copies[game.Id]++;
            game.YourNumbers.IntersectWith(game.WinningNumbers);
            for (int i = 1; i <= game.YourNumbers.Count; i++)
            {
                if (!copies.ContainsKey(game.Id + i))
                {
                    copies[game.Id + i] = 0;
                }
                copies[game.Id + i] += copies[game.Id];
            }
        }

        return copies.Values.Sum();
    }
}