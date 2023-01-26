namespace AdventOfCode.Days;

public class Day1 : AdventOfCodeDay
{
    protected override int GetDay() => 1;

    public override string GetTask1Solution()
    {
        return GetTotalCaloriesPerElf()
            .Max(i => i)
            .ToString();
    }

    public override string GetTask2Solution()
    {
        return GetTotalCaloriesPerElf()
            .OrderByDescending(i => i)
            .Take(3)
            .Sum()
            .ToString();
    }

    private static IEnumerable<int> GetTotalCaloriesPerElf()
    {
        string inputPath = Path.Combine("Assets", "day1.txt");
        int elfCalories = 0;
        
        return File.ReadLines(inputPath)
            .Aggregate(new List<int>(), (acc, cur) =>
            {
                if (String.IsNullOrWhiteSpace(cur))
                {
                    acc.Add(elfCalories);
                    elfCalories = 0;
                }
                else
                {
                    elfCalories += int.Parse(cur);
                }
                return acc;
            });
    }
}