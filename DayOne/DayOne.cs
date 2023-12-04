using System.Text;

namespace aoc_2023.DayOne;

public class DayOne : ISolution
{
    public long SolvePartOne(IEnumerable<string> input)
    {
        long sum = 0;
        foreach (string line in input)
        {
            int calibrationValue = GetCalibrationValue(line);

            sum += calibrationValue;
        }

        return sum;
    }

    public long SolvePartTwo(IEnumerable<string> input)
    {
        long sum = 0;
        
        // These word values are explicitly allowed. Notice: not zero.
        Dictionary<string, string> wordsToDigits = new()
        {
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };

        // Build a search tree
        DayOneSearchTree dayOneSearchTree = new();
        foreach ((string word, string result) in wordsToDigits)
        {
            dayOneSearchTree.AddWord(word, result);
        }
        
        foreach (string line in input)
        {
            string parsedLine = ParseDigits(line, dayOneSearchTree);
            int calibrationValue = GetCalibrationValue(parsedLine);
            sum += calibrationValue;
        }

        return sum;
    }

    private string ParseDigits(string subject, DayOneSearchTree searchTree)
    {
        int cursor = 0;
        StringBuilder result = new();

        while (cursor < subject.Length)
        {
            char c = subject[cursor];
            if (IsDigit(c))
            {
                result.Append(c);
            }
            else if (cursor + 1 < subject.Length)
            {
                string? searchResult = FindWordRecursive(subject, searchTree, cursor);
                if (searchResult is not null)
                {
                    result.Append(searchResult);
                }
            }

            cursor++;
        }

        return result.ToString();
    }

    private string? FindWordRecursive(string subject, DayOneSearchTree searchTree, int pos)
    {
        string? result = null;
        if (searchTree.NextLetters.Count == 0 && searchTree.Result is not null)
        {
            result = searchTree.Result;
            
            return result;
        }

        if (pos < subject.Length)
        {
            char c = subject[pos];
            if (searchTree.NextLetters.TryGetValue(c, out DayOneSearchTree branch))
            {
                return FindWordRecursive(subject, branch, pos + 1);
            }
        }

        return result;
    }
    
    private int GetCalibrationValue(string str)
    {
        int? firstDigit = null, lastDigit = null;
        foreach (char c in str)
        {
            if (IsDigit(c))
            {
                int digit = ToDigit(c);
                lastDigit = digit;
                if (firstDigit is null)
                {
                    firstDigit = digit;
                }
            }
        }

        return (firstDigit.Value * 10) + lastDigit.Value;
    }

    private bool IsDigit(char c) => CharUtils.IsDigit(c);
    private int ToDigit(char c) => CharUtils.ToDigit(c);
}