using System.Diagnostics;

namespace AdventOfCode.Days;

public abstract class AdventOfCodeDay
{
    protected abstract int GetDay();
    protected virtual string GetTask1Solution() => "TODO";
    protected virtual string GetTask2Solution() => "TODO";

    public void PrintSolutions()
    {
        (string solution1, long elapsedTicks1) = GetSolutionAndElapsedTicks(GetTask1Solution);
        (string solution2, long elapsedTicks2) = GetSolutionAndElapsedTicks(GetTask2Solution);
        
        int day = GetDay();
        Console.WriteLine("Day {0}: https://adventofcode.com/2022/day/{0}", day);
        Console.WriteLine("  Task 1: {0} (elapsed ticks: {1})", solution1, elapsedTicks1);
        Console.WriteLine("  Task 2: {0} (elapsed ticks: {1})", solution2, elapsedTicks2);
        Console.WriteLine();
    }

    private (string, long) GetSolutionAndElapsedTicks(Func<string> solutionFunction)
    {
        Stopwatch watch = Stopwatch.StartNew();
        string solution = solutionFunction();
        watch.Stop();
        long elapsedTicks = watch.ElapsedTicks;

        return (solution, elapsedTicks);
    }
}