namespace AdventOfCode.Days;

public class Day6 : AdventOfCodeDay
{
    protected override int GetDay() => 6;

    protected override string GetTask1Solution()
    {
        return getPacketIndex(4).ToString();
    }

    protected override string GetTask2Solution()
    {
        return getPacketIndex(14).ToString();
    }

    private int getPacketIndex(int length)
    {
        string inputPath = Path.Combine("Assets", "day6.txt");
        string data = File.ReadAllText(inputPath);
        int index = 0;
        
        for (int i = 0; i < data.Length; i++)
        {
            IEnumerable<char> chars = data.Substring(i, Math.Min(length, data.Length - i)).Distinct();
            if (chars.Count() == length)
            {
                index = i + length;
                break;
            }
        }

        return index;
    }
}