using System.Diagnostics;

namespace aoc_2023;

public class SolutionRunner
{
    public static void Run(ISolution solution, Parts partsToRun = Parts.One | Parts.Two, bool runSample = false)
    {
        string solutionName = solution.GetType().Name;
        string sampleInputFileName = $"{solutionName}-Sample.txt";
        string inputFileName = $"{solutionName}.txt";
        string fileName = runSample ? sampleInputFileName : inputFileName;
        
        foreach (Parts part in Enum.GetValues<Parts>())
        {
            if (partsToRun.HasFlag(part))
            {
                RunPart(solution, solutionName, fileName, part);
            }
        }
    }

    private static void RunPart(ISolution solution, string name, string fileName, Parts part)
    {
        IEnumerable<string> lines = ReadAllInputLines(fileName);
        Stopwatch timer = Stopwatch.StartNew();
        long result = part switch
        {
            Parts.One => solution.SolvePartOne(lines),
            Parts.Two => solution.SolvePartTwo(lines),
            _ => throw new NotImplementedException()
        };
        timer.Stop();
        Console.WriteLine($"{name} Part {part}: `{result}`. Completed in {timer.Elapsed.TotalMilliseconds}ms.");
    }
    
    private static IEnumerable<string> ReadAllInputLines(string fileName)
    {
        return File.ReadLines(Path.Join("Inputs", fileName));
    }

    [Flags]
    public enum Parts
    {
        One,
        Two
    }
}