namespace aoc_2023.DayOne;

public class DayOneSearchTree
{
    public string? Result;
    public Dictionary<char, DayOneSearchTree> NextLetters { get; } = new();

    public void AddWord(string word, string result)
    {
        char firstLetter = word[0];
        NextLetters.TryAdd(firstLetter, new DayOneSearchTree());
        if (word.Length > 1)
        {
            NextLetters[firstLetter].AddWord(word[1..], result);
        }
        else
        {
            NextLetters[firstLetter].Result = result;
        }
    }
}