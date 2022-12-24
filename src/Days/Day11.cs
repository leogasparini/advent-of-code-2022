using System.Collections.Immutable;

namespace AdventOfCode.Days;

public class Day11 : AdventOfCodeDay
{
    protected override int GetDay() => 11;

    protected override string GetTask1Solution()
    {
        MonkeyInTheMiddle monkeyInTheMiddle = new(GetMonkeys());

        for (int i = 0; i < 20; i++)
        {
            monkeyInTheMiddle.PlayRound();
        }

        IEnumerable<Monkey> mostActiveMonkeys = monkeyInTheMiddle
            .Monkeys
            .OrderByDescending(m => m.InspectedItems)
            .Take(2);

        int monkeyBusiness = mostActiveMonkeys.First().InspectedItems * mostActiveMonkeys.Last().InspectedItems;

        return monkeyBusiness.ToString();
    }

    private List<Monkey> GetMonkeys() => new()
    {
        new Monkey(
            new List<int> { 56, 52, 58, 96, 70, 75, 72 },
            old => old * 17,
            item => item % 11 == 0 ? 2 : 3
        ),
        new Monkey(
            new List<int> { 75, 58, 86, 80, 55, 81 },
            old => old + 7,
            item => item % 3 == 0 ? 6 : 5
        ),
        new Monkey(
            new List<int> { 73, 68, 73, 90 },
            old => old * old,
            item => item % 5 == 0 ? 1 : 7
        ),
        new Monkey(
            new List<int> { 72, 89, 55, 51, 59 },
            old => old + 1,
            item => item % 7 == 0 ? 2 : 7
        ),
        new Monkey(
            new List<int> { 76, 76, 91 },
            old => old * 3,
            item => item % 19 == 0 ? 0 : 3
        ),
        new Monkey(
            new List<int> { 88 },
            old => old + 4,
            item => item % 2 == 0 ? 6 : 4
        ),
        new Monkey(
            new List<int> { 64, 63, 56, 50, 77, 55, 55, 86 },
            old => old + 8,
            item => item % 13 == 0 ? 4 : 0
        ),
        new Monkey(
            new List<int> { 79, 58 },
            old => old + 6,
            item => item % 17 == 0 ? 1 : 5
        )
    };

    protected override string GetTask2Solution()
    {
        return base.GetTask2Solution();
    }

    private sealed record Monkey(
        List<int> Items,
        Func<int, int> OperationFunc,
        Func<int, int> TestFunc
    )
    {
        public int InspectedItems { get; set; }
    }

    private sealed class MonkeyInTheMiddle
    {
        private readonly List<Monkey> _monkeys;

        public ImmutableList<Monkey> Monkeys => _monkeys.ToImmutableList();

        public MonkeyInTheMiddle(List<Monkey> monkeys)
        {
            _monkeys = monkeys;
        }

        public void PlayRound()
        {
            foreach (Monkey monkey in _monkeys)
            {
                int[] monkeyItems = monkey.Items.ToArray();

                foreach (int item in monkeyItems)
                {
                    int newItem = monkey.OperationFunc(item) / 3;
                    int newMonkeyIndex = monkey.TestFunc(newItem);

                    monkey.InspectedItems++;
                    monkey.Items.Remove(item);
                    _monkeys.ElementAt(newMonkeyIndex).Items.Add(newItem);
                }
            }
        }
    }
}