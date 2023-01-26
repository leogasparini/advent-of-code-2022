namespace AdventOfCode.Days;

public class Day3 : AdventOfCodeDay
{
    protected override int GetDay() => 3;

    public override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets", "day3.txt");

        return File.ReadLines(inputPath)
            .Select(rucksack => rucksack.Take(rucksack.Length / 2)
                .Intersect(rucksack.Skip(rucksack.Length / 2))
                .Single())
            .Select(ToPriority)
            .Sum()
            .ToString();
    }

    public override string GetTask2Solution()
    {
        string inputPath = Path.Combine("Assets", "day3.txt");

        return File.ReadLines(inputPath)
            .Chunk(3)
            .Select(group => group.First()
                .Intersect(group.ElementAt(1))
                .Intersect(group.ElementAt(2))
                .Single())
            .Select(ToPriority)
            .Sum()
            .ToString();
    }

    private static int ToPriority(char item)
    {
        return char.IsLower(item)
            ? item - 96
            : item - 38;
    }
}