using System.Text.RegularExpressions;

namespace aoc_2023.DayEight;

public class NodeManager
{
    public static Regex FromStrExp = new Regex(@"(?<Identity>.+) = \((?<Left>.+), (?<Right>.+)\)", RegexOptions.Compiled);
    /// <summary>
    /// Key: Node.Identity
    /// Value: Node
    /// </summary>
    public IReadOnlyDictionary<string, Node> Nodes => _nodes;
    private Dictionary<string, Node> _nodes { get; } = new ();

    public Node CreateNewNodeFromStr(string str)
    {
        Match match = FromStrExp.Match(str);
        string identity = match.Groups["Identity"].Value;
        string left = match.Groups["Left"].Value;
        string right = match.Groups["Right"].Value;

        Node node = GetOrAddNode(identity);
        Node leftNode = GetOrAddNode(left);
        Node rightNode = GetOrAddNode(right);
        node.Left = leftNode;
        node.Right = rightNode;

        return node;
    }

    private Node GetOrAddNode(string identity)
    {
        if (!_nodes.TryGetValue(identity, out Node node))
        {
            node = new Node(identity);
            _nodes[identity] = node;
        }

        return node;
    }
}