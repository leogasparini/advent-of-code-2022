namespace AdventOfCode.Days;

public abstract class AdventOfCodeDay
{
    protected abstract int GetDay();
    protected virtual string GetTask1Solution() => "TODO";
    protected virtual string GetTask2Solution() => "TODO";

    public void PrintSolutions()
    {
        int day = GetDay();
        Console.WriteLine("Day {0}: https://adventofcode.com/2022/day/{0}", day);
        Console.WriteLine("  Task 1: {0}", GetTask1Solution());
        Console.WriteLine("  Task 2: {0}", GetTask2Solution());
        Console.WriteLine();
    }
}