namespace aoc_2023.DayThree;
        
/*
 * Use a dictionary instead of a multidimensional or jagged array.
 *
 * This saves space for empty cells and eliminates need for OOB checks on adjacency lookups.
 */
public class Engine : Dictionary<(int x, int y), Space>
{
    public static Engine Build(IEnumerable<string> input)
    {
        Engine engine = new();
        // Fully enumerate here so we can access things like count, we'll just eat the cost.
        IList<string> lines = input.ToList();
        int x = 0, y = 0, xLimit = lines[0].Length, yLimit = lines.Count;

        // Construct the engine.
        while (y < yLimit)
        {
            while (x < xLimit)
            {
                char c = lines[y][x];
                if (c == '.')
                {
                    x++;
                    continue;
                }
                
                if (CharUtils.TryGetDigit(c, out int? digit))
                {
                    NumberValue partNumber = new (digit.Value);
                    engine[(x, y)] = partNumber;
                    // Look for consecutive digits to add to the part number.
                    while (x < xLimit)
                    {
                        x++;
                        if (x < xLimit)
                        {
                            c = lines[y][x];
                            if (CharUtils.TryGetDigit(c, out digit))
                            {
                                partNumber.AddDigit(digit.Value);
                                // For the sake of adjacency / hashing, use the same part number across multiple spaces.
                                engine[(x, y)] = partNumber;
                            }
                            else
                            {
                                // If it is not a digit, decrement x and let the incrementor lower down handle it.
                                x--;
                                break;
                            }
                        }

                    }
                }
                else
                {
                    engine[(x, y)] = new SymbolValue(c);
                    engine.Symbols.Add((x, y));
                }
                x++;
            }
            // Reset horizontal, increment vertical.
            x = 0;
            y++;
        }

        return engine;
    }

    public IList<(int x, int y)> Symbols { get; set; } = new List<(int x, int y)>();
}