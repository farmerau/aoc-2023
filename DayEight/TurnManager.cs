namespace aoc_2023.DayEight;

public class TurnManager
{
    private readonly char[] _turns;
    private int _idx = 0;
    
    public TurnManager(char[] turns)
    {
        _turns = turns;
    }

    public char NextTurn()
    {
        char result = _turns[_idx];
        _idx = (_idx + 1) % _turns.Length;

        return result;
    }

    public void Reset()
    {
        _idx = 0;
    }
}