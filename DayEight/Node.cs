namespace aoc_2023.DayEight;

public class Node
{
    public string Identity { get; }

    public Node? Left { get; set; } = null;
    public Node? Right { get; set; } = null;

    public Node(string identity)
    {
        Identity = identity;
    }
}