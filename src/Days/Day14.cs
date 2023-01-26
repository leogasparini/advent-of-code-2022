using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day14 : AdventOfCodeDay
{
    protected override int GetDay() => 14;
    private readonly (int, int) _sandPouringPoint = (500, 0);

    protected override string GetTask1Solution()
    {
        List<List<(int x, int y)>> traces = GetTraces();
        FramedCave cave = new(traces, _sandPouringPoint);

        int filledUnits = cave.FillWithSand();
        cave.Display();

        return filledUnits.ToString();
    }

    protected override string GetTask2Solution()
    {
        List<List<(int x, int y)>> traces = GetTraces();

        int minX = traces.Min(t => t.Min(p => p.x));
        int maxX = traces.Max(t => t.Max(p => p.x));
        int maxY = traces.Max(t => t.Max(p => p.y));

        traces.Add(new List<(int x, int y)>
        {
            (minX, maxY + 2),
            (maxX, maxY + 2)
        });

        EndlessCave cave = new(traces, _sandPouringPoint);

        int filledUnits = cave.FillWithSand();
        cave.Display();

        return filledUnits.ToString();
    }

    private List<List<(int x, int y)>> GetTraces()
    {
        const string coordsPattern = @"(?<x>\-?\d+),(?<y>\-?\d+)";
        string inputPath = Path.Combine("Assets", "day14.txt");
        List<List<(int x, int y)>> traces = new();
        Regex regex = new(coordsPattern, RegexOptions.Compiled);

        foreach (string line in File.ReadAllLines(inputPath))
        {
            List<(int x, int y)> tracePoints = new();
            Match match = regex.Match(line);
            while (match.Success)
            {
                tracePoints.Add((int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value)));
                match = match.NextMatch();
            }

            traces.Add(tracePoints);
        }

        return traces;
    }

    private abstract class Cave
    {
        private readonly List<List<(int x, int y)>> _traces;

        protected readonly (int x, int y) SandPouringPoint;
        protected readonly Dictionary<(int x, int y), char> Map = new();
        protected int Height => GetHeight();
        protected int Width => GetWidth();
        protected int TracesMinX => GetTracesMinX();
        protected int TracesMaxX => GetTracesMaxX();
        protected int TracesMinY => GetTracesMinY();
        protected int TracesMaxY => GetTracesMaxY();

        protected Cave(
            List<List<(int x, int y)>> traces,
            (int x, int y) sandPouringPoint)
        {
            SandPouringPoint = sandPouringPoint;
            _traces = traces;

            DrawRocks();
        }

        private int GetHeight()
        {
            return Map.Max(i => i.Key.y);
        }

        private int GetWidth()
        {
            return Map.Max(i => i.Key.x);
        }

        private int GetTracesMinX()
        {
            return _traces.Min(t => t.Min(p => p.x));
        }

        private int GetTracesMaxX()
        {
            return _traces.Max(t => t.Max(p => p.x));
        }

        private int GetTracesMinY()
        {
            return _traces.Min(t => t.Min(p => p.y));
        }

        private int GetTracesMaxY()
        {
            return _traces.Max(t => t.Max(p => p.y));
        }

        private void DrawSandPouringPoint()
        {
            Map[SandPouringPoint] = '+';
        }

        private void DrawRocks()
        {
            foreach (List<(int x, int y)> trace in _traces)
            {
                (int prevX, int prevY) = trace.First();

                foreach ((int currX, int currY) in trace.Skip(1))
                {
                    if (prevX == currX)
                    {
                        for (int i = Math.Min(prevY, currY); i <= Math.Max(prevY, currY); i++)
                        {
                            Map[(currX, i)] = '#';
                        }
                    }
                    else if (prevY == currY)
                    {
                        for (int i = Math.Min(prevX, currX); i <= Math.Max(prevX, currX); i++)
                        {
                            Map[(i, currY)] = '#';
                        }
                    }

                    (prevX, prevY) = (currX, currY);
                }
            }
        }

        public void Display(bool highlightSandPouringPoint = true)
        {
            string output = "";
            int heightLength = Height.ToString().Length;

            if (highlightSandPouringPoint)
                DrawSandPouringPoint();

            for (int y = 0; y <= TracesMaxY; y++)
            {
                output += y.ToString().PadLeft(heightLength).PadRight(heightLength + 1);

                for (int x = TracesMinX; x <= TracesMaxX; x++)
                {
                    (int, int) point = (x, y);
                    output += !Map.ContainsKey(point) || Map[point] == 0 ? '.' : Map[point];
                }

                output += Environment.NewLine;
            }

            Console.WriteLine(output);
        }
    }

    private sealed class FramedCave : Cave
    {
        public FramedCave(
            List<List<(int x, int y)>> traces,
            (int x, int y) sandPouringPoint) : base(traces, sandPouringPoint)
        {
        }

        public int FillWithSand()
        {
            int pouredCount = 0;
            Queue<(int, int)> queue = new();
            HashSet<(int, int)> visited = new();
            queue.Enqueue(SandPouringPoint);

            while (queue.Any())
            {
                (int x, int y) = queue.Dequeue();

                visited.Add((x, y));

                if (y == TracesMaxY)
                    break;

                (int, int) bottom = (x, Math.Min(y + 1, TracesMaxY));
                (int, int) bottomLeft = (Math.Max(x - 1, 0), Math.Min(y + 1, TracesMaxY));
                (int, int) bottomRight = (Math.Min(x + 1, TracesMaxX), Math.Min(y + 1, TracesMaxY));

                if (!Map.ContainsKey(bottom))
                    Map[bottom] = (char)0;

                if (!Map.ContainsKey(bottomLeft))
                    Map[bottomLeft] = (char)0;

                if (!Map.ContainsKey(bottomRight))
                    Map[bottomRight] = (char)0;

                if (Map[bottom] == 0)
                {
                    queue.Enqueue(bottom);
                }
                else if (Map[bottomLeft] == 0)
                {
                    queue.Enqueue(bottomLeft);
                }
                else if (Map[bottomRight] == 0)
                {
                    queue.Enqueue(bottomRight);
                }
                else
                {
                    pouredCount++;
                    Map[(x, y)] = 'o';
                    queue.Enqueue(SandPouringPoint);
                    visited.Clear();
                }
            }

            foreach ((int, int) point in visited)
            {
                Map[point] = '~';
            }

            return pouredCount;
        }
    }

    private sealed class EndlessCave : Cave
    {
        public EndlessCave(
            List<List<(int x, int y)>> traces,
            (int x, int y) sandPouringPoint) : base(traces, sandPouringPoint)
        {
        }

        public int FillWithSand()
        {
            int pouredCount = 0;
            Queue<(int, int)> queue = new();
            queue.Enqueue(SandPouringPoint);

            while (queue.Any())
            {
                (int x, int y) = queue.Dequeue();
                int bottomY = Math.Min(y + 1, TracesMaxY);
                bool isTouchingBottom = bottomY == TracesMaxY;

                (int, int) bottom = (x, bottomY);
                (int, int) bottomLeft = (x - 1, bottomY);
                (int, int) bottomRight = (x + 1, bottomY);

                if (!Map.ContainsKey(bottom))
                    Map[bottom] = isTouchingBottom ? '#' : (char)0;

                if (!Map.ContainsKey(bottomLeft))
                    Map[bottomLeft] = isTouchingBottom ? '#' : (char)0;

                if (!Map.ContainsKey(bottomRight))
                    Map[bottomRight] = isTouchingBottom ? '#' : (char)0;

                if (Map[bottom] == 0)
                {
                    queue.Enqueue(bottom);
                }
                else if (Map[bottomLeft] == 0)
                {
                    queue.Enqueue(bottomLeft);
                }
                else if (Map[bottomRight] == 0)
                {
                    queue.Enqueue(bottomRight);
                }
                else
                {
                    pouredCount++;
                    Map[(x, y)] = 'o';
                    
                    if(y == 0)
                        break;
                    
                    queue.Enqueue(SandPouringPoint);
                }
            }

            return pouredCount;
        }
    }
}