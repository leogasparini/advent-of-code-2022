namespace AdventOfCode.Days;

public class Day9 : AdventOfCodeDay
{
    protected override int GetDay() => 9;

    protected override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets", "day9.txt");
        HashSet<Knot> visitedPositions = new();
        Rope rope = new(2);

        foreach (string line in File.ReadLines(inputPath))
        {
            (string direction, int steps) = ParseInstructions(line);

            for (int i = 0; i < steps; i++)
            {
                rope.MoveHead(direction);
                visitedPositions.Add(rope.Tail);
            }
        }

        return visitedPositions.Count.ToString();
    }

    protected override string GetTask2Solution()
    {
        string inputPath = Path.Combine("Assets", "day9.txt");
        HashSet<Knot> visitedPositions = new();
        Rope rope = new(10);

        foreach (string line in File.ReadLines(inputPath))
        {
            (string direction, int steps) = ParseInstructions(line);

            for (int i = 0; i < steps; i++)
            {
                rope.MoveHead(direction);
                visitedPositions.Add(rope.Tail);
            }
        }

        return visitedPositions.Count.ToString();
    }

    private (string direction, int steps) ParseInstructions(string line)
    {
        string[] instructions = line.Split(' ');

        return (instructions.First(), int.Parse(instructions.Last()));
    }

    private sealed record Knot
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    private sealed class Rope
    {
        private readonly Queue<Knot> _knots = new();
        
        public Knot Tail => _knots.Last();

        public Rope(int knotsCount)
        {
            for (int i = 0; i < knotsCount; i++)
            {
                _knots.Enqueue(new Knot());
            }
        }

        public void MoveHead(string direction)
        {
            Knot head = _knots.First();
            
            switch (direction)
            {
                case "U":
                    head.Y++;
                    break;
                case "R":
                    head.X++;
                    break;
                case "D":
                    head.Y--;
                    break;
                case "L":
                    head.X--;
                    break;
                default:
                    throw new InvalidOperationException("Invalid move");
            }

            MoveNextKnot(head, 1);
        }

        private void MoveNextKnot(Knot movedKnot, int nextIndex)
        {
            Knot nextKnot = _knots.ElementAt(nextIndex);
            int distanceX = Math.Abs(movedKnot.X - nextKnot.X);
            int distanceY = Math.Abs(movedKnot.Y - nextKnot.Y);

            if (distanceX > 1)
            {
                nextKnot.X += distanceX / (movedKnot.X - nextKnot.X);

                if (distanceY == 1)
                {
                    nextKnot.Y += distanceY / (movedKnot.Y - nextKnot.Y);
                }
            }

            if (distanceY > 1)
            {
                nextKnot.Y += distanceY / (movedKnot.Y - nextKnot.Y);

                if (distanceX == 1)
                {
                    nextKnot.X += distanceX / (movedKnot.X - nextKnot.X);
                }
            }

            if (_knots.Count - 1 > nextIndex)
                MoveNextKnot(nextKnot, nextIndex + 1);
        }
    }
}