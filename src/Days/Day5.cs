using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day5 : AdventOfCodeDay
{
    protected override int GetDay() => 5;

    protected override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets", "day5.txt");
        const string template = "move {0} from {1} to {2}";
        List<Stack<char>> supplyStacks = GetSupplyStacks()
            .Select(s => new Stack<char>(s))
            .ToList();

        foreach (string line in File.ReadLines(inputPath))
        {
            (int moveQty, int from, int to) = ParseInstructions(template, line);

            Stack<char> source = supplyStacks.ElementAt(from - 1);
            Stack<char> destination = supplyStacks.ElementAt(to - 1);

            for (int i = 0; i < moveQty; i++)
            {
                destination.Push(source.Pop());
            }
        }

        return supplyStacks.Aggregate("", (acc, cur) => acc + cur.Peek());
    }

    protected override string GetTask2Solution()
    {
        string inputPath = Path.Combine("Assets", "day5.txt");
        const string template = "move {0} from {1} to {2}";
        List<List<char>> supplyStacks = GetSupplyStacks();

        foreach (string line in File.ReadLines(inputPath))
        {
            (int moveQty, int from, int to) = ParseInstructions(template, line);

            List<char> source = supplyStacks.ElementAt(from - 1);
            List<char> destination = supplyStacks.ElementAt(to - 1);
            destination.AddRange(source.TakeLast(moveQty));
            source.RemoveRange(source.Count - moveQty, moveQty);
        }

        return supplyStacks.Aggregate("", (acc, cur) => acc + cur.Last());
    }

    private (int moveQty, int from, int to) ParseInstructions(string template, string value)
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

    private List<List<char>> GetSupplyStacks() => new()
    {
        new() { 'B', 'W', 'N' },
        new() { 'L', 'Z', 'S', 'P', 'T', 'D', 'M', 'B' },
        new() { 'Q', 'H', 'Z', 'W', 'R' },
        new() { 'W', 'D', 'V', 'J', 'Z', 'R' },
        new() { 'S', 'H', 'M', 'B' },
        new() { 'L', 'G', 'N', 'J', 'H', 'V', 'P', 'B' },
        new() { 'J', 'Q', 'Z', 'F', 'H', 'D', 'L', 'S' },
        new() { 'W', 'S', 'F', 'J', 'G', 'Q', 'B' },
        new() { 'Z', 'W', 'M', 'S', 'C', 'D', 'J' },
    };
}