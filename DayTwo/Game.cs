using System.Text.RegularExpressions;

namespace aoc_2023.DayTwo;

public class Game
{
    private static Regex GameRegex = new ("Game (?<Id>\\d+): (?<DrawingSets>.+)", RegexOptions.Compiled);
    private static Regex DrawingSetRegex = new ("(?<Quantity>\\d+)\\s(?<Color>[a-z]+)", RegexOptions.Compiled);
    
    public int Id { get; set; }
    public IEnumerable<DrawingSet> DrawingSets { get; set; }

    public static Game FromString(string str)
    {
        Match match = GameRegex.Match(str);
        int id = int.Parse(match.Groups["Id"].Value);
        string[] drawingSetsRaw = match.Groups["DrawingSets"].Value.Split(';');
        IList<DrawingSet> drawingSets = new List<DrawingSet>();
        foreach (string drawingSet in drawingSetsRaw)
        {
            MatchCollection matches = DrawingSetRegex.Matches(drawingSet);
            DrawingSet set = new ();
            foreach (Match m in matches)
            {
                string color = m.Groups["Color"].Value;
                int quantity = int.Parse(m.Groups["Quantity"].Value);
                if (!set.Drawings.ContainsKey(color))
                {
                    set.Drawings.Add(color, 0);
                }

                set.Drawings[color] += quantity;
            }

            drawingSets.Add(set);
        }
        
        return new Game
        {
            Id = id,
            DrawingSets = drawingSets
        };
    }
}