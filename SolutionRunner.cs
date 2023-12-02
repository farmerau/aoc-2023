using System.Diagnostics;

namespace aoc_2023;

public class SolutionRunner
{
    public static void Run(ISolution solution, bool runSample = false)
    {
        string solutionName = solution.GetType().Name;
        string sampleInputFileName = $"{solutionName}-Sample.txt";
        string inputFileName = $"{solutionName}.txt";
        string fileName = runSample ? sampleInputFileName : inputFileName;
        
        Stopwatch partOneTimer = Stopwatch.StartNew();
        long partOneResult = solution.SolvePartOne(ReadAllInputLines(fileName));
        partOneTimer.Stop();
        Console.WriteLine($"{solutionName} Part One: `{partOneResult}`. Completed in {partOneTimer.Elapsed.TotalMilliseconds}ms.");

        Stopwatch partTwoTimer = Stopwatch.StartNew();
        long partTwoResult = solution.SolvePartTwo(ReadAllInputLines(fileName));
        partTwoTimer.Stop();
        Console.WriteLine($"{solutionName} Part Two: `{partTwoResult}`. Completed in {partTwoTimer.Elapsed.TotalMilliseconds}ms.");
    }
    
    private static IEnumerable<string> ReadAllInputLines(string fileName)
    {
        return File.ReadLines(Path.Join("Inputs", fileName));
    }
}