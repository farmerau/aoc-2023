using System.Diagnostics.CodeAnalysis;

namespace aoc_2023;

public static class CharUtils
{
    public static bool IsDigit(char c) => c > 47 && c < 58;

    public static bool TryGetDigit(char c, [NotNullWhen(true)] out int? digit)
    {
        digit = null;
        if (IsDigit(c))
        {
            digit = ToDigit(c);
            return true;
        }

        return false;
    }

    public static int ToDigit(char c) => c - 48;
}