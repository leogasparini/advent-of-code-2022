using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day5 : AdventOfCodeDay
{
    protected override int GetDay() => 5;

    private List<Stack<char>> _supplyStacks = new()
    {
        new Stack<char>(new[] { 'B', 'W', 'N' }),
        new Stack<char>(new[] { 'L', 'Z', 'S', 'P', 'T', 'D', 'M', 'B' }),
        new Stack<char>(new[] { 'Q', 'H', 'Z', 'W', 'R' }),
        new Stack<char>(new[] { 'W', 'D', 'V', 'J', 'Z', 'R' }),
        new Stack<char>(new[] { 'S', 'H', 'M', 'B' }),
        new Stack<char>(new[] { 'L', 'G', 'N', 'J', 'H', 'V', 'P', 'B' }),
        new Stack<char>(new[] { 'J', 'Q', 'Z', 'F', 'H', 'D', 'L', 'S' }),
        new Stack<char>(new[] { 'W', 'S', 'F', 'J', 'G', 'Q', 'B' }),
        new Stack<char>(new[] { 'Z', 'W', 'M', 'S', 'C', 'D', 'J' }),
    };

    protected override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets", "day5.txt");
        const string template = "move {0} from {1} to {2}";

        foreach (string line in File.ReadLines(inputPath))
        {
            (int moveQty, int fromStack, int toStack) = ParseInstructions(template, line);

            for (int i = 0; i < moveQty; i++)
            {
                char crate = _supplyStacks.ElementAt(fromStack - 1).Pop();
                _supplyStacks.ElementAt(toStack - 1).Push(crate);
            }
        }

        return _supplyStacks.Aggregate("", (acc, cur) => acc + cur.Peek());
    }

    private (int moveQty, int fromStack, int toStack) ParseInstructions(string template, string value)
    {
        string pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

        Regex regexp = new Regex(pattern);
        Match match = regexp.Match(value);

        List<int> values = new();
        for (int i = 1; i < match.Groups.Count; i++)
        {
            values.Add(int.Parse(match.Groups[i].Value));
        }

        return (values.ElementAt(0), values.ElementAt(1), values.ElementAt(2));
    }
}