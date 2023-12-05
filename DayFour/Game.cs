using System.Text.RegularExpressions;

namespace aoc_2023.DayFour;

public class Game
{
    private static readonly Regex ScratchOffRoot = new Regex("Card\\s+(?<Id>\\d+): (?<Numbers>.+)", RegexOptions.Compiled);
    public int Id { get; set; }
    public HashSet<int> WinningNumbers { get; set; }
    public HashSet<int> YourNumbers { get; set; }

    public static Game FromString(string str)
    {
        Match match = ScratchOffRoot.Match(str);
        int id = int.Parse(match.Groups["Id"].Value);
        string numbers = match.Groups["Numbers"].Value;
        string[] halves = numbers.Split('|');
        Game game = new Game
        {
            Id = id,
            WinningNumbers = ParseHalf(halves[0]),
            YourNumbers = ParseHalf(halves[1])
        };
        HashSet<int> winningNumbers = ParseHalf(halves[0]);
        HashSet<int> numbersIHave = ParseHalf(halves[1]);

        return game;
    }
    
    private static HashSet<int> ParseHalf(string half)
    {
        return half
            .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToHashSet();
    }
}