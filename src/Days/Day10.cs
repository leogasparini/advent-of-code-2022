namespace AdventOfCode.Days;

public class Day10 : AdventOfCodeDay
{
    protected override int GetDay() => 10;

    protected override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets", "day10.txt");
        int X = 1;
        int currentCycle = 1;
        int[] interestingCycles = { 20, 60, 100, 140, 180, 220 };
        List<int> signalStrengths = new();

        foreach (string line in File.ReadLines(inputPath))
        {
            foreach (int value in GetValuesToAdd(line))
            {
                if (interestingCycles.Contains(currentCycle))
                {
                    signalStrengths.Add(X * currentCycle);
                }

                X += value;
                currentCycle++;
            }
        }

        return signalStrengths.Sum().ToString();
    }

    protected override string GetTask2Solution()
    {
        string inputPath = Path.Combine("Assets", "day10.txt");
        const int height = 6;
        const int width = 40;
        CrtScreen crtScreen = new(height, width);
        int X = 1;
        int currentCycle = 0;

        foreach (string line in File.ReadLines(inputPath))
        {
            foreach (int value in GetValuesToAdd(line))
            {
                int[] spritePositions = { X - 1, X, X + 1 };
                int currentCrtCol = currentCycle % width;
                int currentCrtRow = currentCycle / width;
                char pixelValue = spritePositions.Contains(currentCrtCol) ? '#' : '.';

                crtScreen.WritePixel(currentCrtRow, currentCrtCol, pixelValue);

                X += value;
                currentCycle++;
            }
        }

        return Environment.NewLine + crtScreen.Draw();
    }

    private IEnumerable<int> GetValuesToAdd(string line)
    {
        string[] instructions = line.Split(' ');

        if (instructions.First() == "noop")
        {
            yield return 0;
        }
        else
        {
            yield return 0;
            yield return int.Parse(instructions.Last());
        }
    }

    private sealed class CrtScreen
    {
        private readonly char[][] _screen = Array.Empty<char[]>();

        public CrtScreen(int height, int width)
        {
            Array.Resize(ref _screen, height);
            for (int i = 0; i < _screen.Length; i++)
            {
                Array.Resize(ref _screen[i], width);
            }
        }

        public void WritePixel(int row, int col, char value)
        {
            _screen[row][col] = value;
        }

        public string Draw() =>
            _screen.Aggregate("", (acc, cur) => acc + new string(cur) + Environment.NewLine);
    }
}