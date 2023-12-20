namespace aoc_2023.DaySeven;

public class Hand : IComparable<Hand>
{
    private readonly IDictionary<char, int> _cardMap = new Dictionary<char, int>();

    private static readonly Lazy<IDictionary<char, int>> CardValues = new (() =>
    {
        char[] cards = {'2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'};
        IDictionary<char, int> result = new Dictionary<char, int>(cards.Length);
        for (int i = 0; i < cards.Length; i++)
        {
            result[cards[i]] = i;
        }

        return result;
    });

    private static readonly Lazy<IDictionary<char, int>> CardValuesJokersWild = new(() =>
    {
        char[] cards = {'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'};
        IDictionary<char, int> result = new Dictionary<char, int>(cards.Length);
        for (int i = 0; i < cards.Length; i++)
        {
            result[cards[i]] = i;
        }

        return result;
    });

    private readonly bool JokersAreWild = false;

    private string OriginalValue { get; }
    private HandType HandType { get; }
    
    public Hand(string original, bool jokersAreWild = false)
    {
        OriginalValue = original;
        JokersAreWild = jokersAreWild;
        foreach (char card in original)
        {
            if (!_cardMap.ContainsKey(card))
            {
                _cardMap[card] = 0;
            }

            _cardMap[card]++;
        }

        if (jokersAreWild && _cardMap.ContainsKey('J'))
        {
            HandType = ComputeTypeJokersAreWild(_cardMap);
        }
        else
        {
            HandType = ComputeType(_cardMap);
        }
    }

    private static HandType ComputeType(IDictionary<char, int> cardMap)
    {
        IList<int> values = cardMap.Values.ToList();
        
        return values switch
        {
            {Count: 1} => HandType.FiveOfAKind,
            {Count: 2} and [4 or 1, 1 or 4] => HandType.FourOfAKind,
            {Count: 2} => HandType.FullHouse,
            {Count: 3} and ([3, _, _] or [_, 3, _] or [_, _, 3]) => HandType.ThreeOfAKind,
            {Count: 3} and ([2, 2, 1] or [2, 1, 2] or [1, 2, 2]) => HandType.TwoPair,
            {Count: 4} and ([2, _, _, _] or [_, 2, _, _] or [_, _, 2, _] or [_, _, _, 2]) => HandType.OnePair,
            _ => HandType.HighCard
        };
    }

    private static HandType ComputeTypeJokersAreWild(IDictionary<char, int> cardMap)
    {
        if (cardMap.Count == 1)
        {
            return HandType.FiveOfAKind;
        }
        
        int jokerCount = cardMap['J'];

        KeyValuePair<char, int> highestPresentCard = cardMap.Select(kvp => kvp).Where(kvp => kvp.Key != 'J').MaxBy(kvp => kvp.Value);

        IDictionary<char, int> newCardMap = cardMap.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        newCardMap.Remove('J');

        newCardMap[highestPresentCard.Key] += jokerCount;

        return ComputeType(newCardMap);
    }

    public int CompareTo(Hand? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (HandType > other.HandType)
        {
            return 1;
        }
        
        if (HandType < other.HandType)
        {
            return -1;
        }

        IDictionary<char, int> cardValues = JokersAreWild ? CardValuesJokersWild.Value : CardValues.Value;

        // Same hand types require secondary evaluation:
        int i = 0;
        while (i < OriginalValue.Length)
        {
            if (OriginalValue[i] != other.OriginalValue[i])
            {
                if (cardValues[OriginalValue[i]] > cardValues[other.OriginalValue[i]])
                {
                    return 1;
                }

                return -1;
            }

            i++;
        }

        return 0;
    }
}