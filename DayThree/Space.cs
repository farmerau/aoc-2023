namespace aoc_2023.DayThree;

public abstract class Space
{
}

public class NumberValue : Space {
    public int Value { get; private set; }

    public NumberValue(int initialValue)
    {
        Value = initialValue;
    }
    
    public void AddDigit(int digit)
    {
        // Multiply the current value by 10 to add a ones place.
        Value *= 10;
        // Put the digit onto the ones place.
        Value += digit;
    }

    public override string ToString() => Value.ToString();
}

public class SymbolValue : Space
{
    public char Value { get; private set; }

    public SymbolValue(char value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString();
}