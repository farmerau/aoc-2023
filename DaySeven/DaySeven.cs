namespace aoc_2023.DaySeven;

public class DaySeven : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        long result = 0;
        (Hand Hand, int Wager)[] game = input
            .Select(l => ParseLine(l))
            .OrderBy(h => h.Hand)
            .ToArray();

        for (int i = 0; i < game.Length; i++)
        {
            int rank = i + 1;
            (Hand Hand, int Wager) hand = game[i];
            result += (hand.Wager * rank);
        }

        return result;
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        long result = 0;
        (Hand Hand, int Wager)[] game = input
            .Select(l => ParseLine(l, true))
            .OrderBy(h => h.Hand)
            .ToArray();

        for (int i = 0; i < game.Length; i++)
        {
            int rank = i + 1;
            (Hand Hand, int Wager) hand = game[i];
            result += (hand.Wager * rank);
        }

        return result;
    }

    private (Hand Hand, int Wager) ParseLine(string line, bool jokersAreWild = false)
    {
        string[] parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        Hand hand = new(parts[0], jokersAreWild);
        int wager = int.Parse(parts[1]);

        return (hand, wager);
    }
}