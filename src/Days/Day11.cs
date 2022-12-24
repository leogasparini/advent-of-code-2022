using System.Collections.Immutable;

namespace AdventOfCode.Days;

public class Day11 : AdventOfCodeDay
{
    protected override int GetDay() => 11;

    protected override string GetTask1Solution()
    {
        const int rounds = 20;
        MonkeyInTheMiddle monkeyInTheMiddle = new(GetMonkeys(), n => n / 3);
        monkeyInTheMiddle.PlayRounds(rounds);

        IEnumerable<Monkey> mostActiveMonkeys = GetMostActiveMonkeys(monkeyInTheMiddle, 2);

        long monkeyBusiness = mostActiveMonkeys.First().InspectedItems * mostActiveMonkeys.Last().InspectedItems;

        return monkeyBusiness.ToString();
    }

    protected override string GetTask2Solution()
    {
        const int rounds = 10_000;
        long divider = 11 * 3 * 5 * 7 * 19 * 2 * 13 * 17;
        MonkeyInTheMiddle monkeyInTheMiddle = new(GetMonkeys(), n => n % divider);
        monkeyInTheMiddle.PlayRounds(rounds);

        IEnumerable<Monkey> mostActiveMonkeys = GetMostActiveMonkeys(monkeyInTheMiddle, 2);

        long monkeyBusiness = mostActiveMonkeys.First().InspectedItems * mostActiveMonkeys.Last().InspectedItems;

        return monkeyBusiness.ToString();
    }

    private List<Monkey> GetMonkeys() => new()
    {
        new Monkey(
            new List<long> { 56, 52, 58, 96, 70, 75, 72 },
            old => old * 17,
            item => item % 11 == 0 ? 2 : 3
        ),
        new Monkey(
            new List<long> { 75, 58, 86, 80, 55, 81 },
            old => old + 7,
            item => item % 3 == 0 ? 6 : 5
        ),
        new Monkey(
            new List<long> { 73, 68, 73, 90 },
            old => old * old,
            item => item % 5 == 0 ? 1 : 7
        ),
        new Monkey(
            new List<long> { 72, 89, 55, 51, 59 },
            old => old + 1,
            item => item % 7 == 0 ? 2 : 7
        ),
        new Monkey(
            new List<long> { 76, 76, 91 },
            old => old * 3,
            item => item % 19 == 0 ? 0 : 3
        ),
        new Monkey(
            new List<long> { 88 },
            old => old + 4,
            item => item % 2 == 0 ? 6 : 4
        ),
        new Monkey(
            new List<long> { 64, 63, 56, 50, 77, 55, 55, 86 },
            old => old + 8,
            item => item % 13 == 0 ? 4 : 0
        ),
        new Monkey(
            new List<long> { 79, 58 },
            old => old + 6,
            item => item % 17 == 0 ? 1 : 5
        )
    };

    private IEnumerable<Monkey> GetMostActiveMonkeys(MonkeyInTheMiddle game, int count) =>
        game
            .Monkeys
            .OrderByDescending(m => m.InspectedItems)
            .Take(count);

    private sealed record Monkey(
        List<long> Items,
        Func<long, long> OperationFunc,
        Func<long, int> TestFunc
    )
    {
        public long InspectedItems { get; set; }
    }

    private sealed class MonkeyInTheMiddle
    {
        private readonly List<Monkey> _monkeys;
        private readonly Func<long, long>? _worryDecreasingFunc;

        public ImmutableList<Monkey> Monkeys => _monkeys.ToImmutableList();

        public MonkeyInTheMiddle(
            List<Monkey> monkeys,
            Func<long, long>? worryDecreasingFunc = null
            )
        {
            _monkeys = monkeys;
            _worryDecreasingFunc = worryDecreasingFunc;
        }

        public void PlayRounds(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                PlayRound();
            }
        }
        
        private void PlayRound()
        {
            foreach (Monkey monkey in _monkeys)
            {
                long[] monkeyItems = monkey.Items.ToArray();

                foreach (long item in monkeyItems)
                {
                    long newItem = monkey.OperationFunc(item);

                    if (_worryDecreasingFunc is not null)
                        newItem = _worryDecreasingFunc(newItem);
                    
                    int newMonkeyIndex = monkey.TestFunc(newItem);

                    monkey.InspectedItems++;
                    monkey.Items.Remove(item);
                    _monkeys.ElementAt(newMonkeyIndex).Items.Add(newItem);
                }
            }
        }
    }
}