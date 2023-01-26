namespace AdventOfCode.Days;

public class Day4 : AdventOfCodeDay
{
    protected override int GetDay() => 4;

    public override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets", "day4.txt");

        int count = 0;
        foreach (string lines in File.ReadLines(inputPath))
        {
            IEnumerable<int[]> sections = lines.Split(',')
                .Select(s => s.Split('-').Select(int.Parse).ToArray());

            int[] group1 = sections.First();
            int[] group2 = sections.Last();

            if ((group1[0] <= group2[0] && group1[1] >= group2[1])
                || (group2[0] <= group1[0] && group2[1] >= group1[1]))
            {
                count++;
            }
        }

        return count.ToString();
    }

    public override string GetTask2Solution()
    {
        string inputPath = Path.Combine("Assets", "day4.txt");

        int count = 0;
        foreach (string lines in File.ReadLines(inputPath))
        {
            IEnumerable<int[]> sections = lines.Split(',')
                .Select(s => s.Split('-').Select(int.Parse).ToArray());

            int[] group1 = sections.First();
            int[] group2 = sections.Last();

            if (group1[0] <= group2[1] && group2[0] <= group1[1])
            {
                count++;
            }
        }

        return count.ToString();
    }
}