using System.Numerics;

namespace aoc_2023;

public static class MathUtils
{
    public static T LeastCommonMultiple<T>(IEnumerable<T> numbers) where T : INumber<T>
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
    {
        return a * b / GreatestCommonDivisor(a, b);
    }

    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            T temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}