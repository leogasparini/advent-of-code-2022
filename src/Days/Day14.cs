using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day14 : AdventOfCodeDay
{
    protected override int GetDay() => 14;

    protected override string GetTask1Solution()
    {
        List<List<(int x, int y)>> traces = GetTraces();
        Cave cave = new(traces);

        int filledUnits = cave.FillWithSand();
        cave.ShowMap();

        return filledUnits.ToString();
    }

    protected override string GetTask2Solution()
    {
        return base.GetTask2Solution();
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

    private sealed class Cave
    {
        private int _width;
        private int _height;
        private int _minX;
        private int _maxX;
        private (int x, int y) _sandPouringPoint;
        private List<List<(int x, int y)>> _traces;
        private int[,] _map;

        public Cave(List<List<(int x, int y)>> traces)
        {
            ComputeCaveSize(traces);
            StoreInternalTraces(traces);
            InitMap();
            InitSandPouringPoint();
            DrawRocks();
            DrawSandPouringPoint();
        }

        private void ComputeCaveSize(List<List<(int x, int y)>> traces)
        {
            _minX = traces.Min(t => t.Min(p => p.x));
            _maxX = traces.Max(t => t.Max(p => p.x));
            _width = _maxX - _minX + 1;
            _height = traces.Max(t => t.Max(p => p.y)) + 1;
        }

        private void StoreInternalTraces(List<List<(int x, int y)>> traces)
        {
            _traces = traces
                .Select(t =>
                    t.Select(p => (p.x - _minX, p.y))
                        .ToList())
                .ToList();
        }
        
        private void InitMap()
        {
            _map = new int[_height, _width];
        }

        private void InitSandPouringPoint()
        {
            _sandPouringPoint = (0, 500 - _minX);
        }

        private void DrawRocks()
        {
            foreach (List<(int x, int y)> trace in _traces)
            {
                List<(int x, int y)>.Enumerator e = trace.GetEnumerator();
                (int prevX, int prevY) = e.Current;

                while (e.MoveNext())
                {
                    if (prevX == e.Current.x)
                    {
                        for (int i = Math.Min(prevY, e.Current.y); i <= Math.Max(prevY, e.Current.y); i++)
                        {
                            _map[i, e.Current.x] = '#';
                        }
                    }
                    else if (prevY == e.Current.y)
                    {
                        for (int i = Math.Min(prevX, e.Current.x); i <= Math.Max(prevX, e.Current.x); i++)
                        {
                            _map[e.Current.y, i] = '#';
                        }
                    }

                    (prevX, prevY) = e.Current;
                }
            }
        }

        private void DrawSandPouringPoint()
        {
            _map[_sandPouringPoint.x, _sandPouringPoint.y] = '+';
        }

        public int FillWithSand()
        {
            int pouredCount = 0;
            Queue<(int x, int y)> queue = new();
            HashSet<(int x, int y)> visited = new();
            queue.Enqueue(_sandPouringPoint);

            while (queue.Any())
            {
                (int x, int y) = queue.Dequeue();

                visited.Add((x, y));
                
                if (x + 1 == _height)
                    break;
                
                (int dx, int dy) = (Math.Min(x + 1, _height - 1), y);
                (int dlx, int dly) = (Math.Min(x + 1, _height - 1), Math.Max(y - 1, 0));
                (int drx, int dry) = (Math.Min(x + 1, _height - 1), Math.Min(y + 1, _width - 1));

                if (_map[dx, dy] == 0)
                {
                    queue.Enqueue((dx, dy));
                }
                else if (_map[dlx, dly] == 0)
                {
                    queue.Enqueue((dlx, dly));
                }
                else if (_map[drx, dry] == 0)
                {
                    queue.Enqueue((drx, dry));
                }
                else
                {
                    pouredCount++;
                    _map[x, y] = 'o';
                    queue.Enqueue(_sandPouringPoint);
                    visited.Clear();
                }
            }

            foreach ((int vx, int vy) in visited)
            {
                _map[vx, vy] = '~'; 
            }
            
            DrawSandPouringPoint();

            return pouredCount;
        }

        public void ShowMap()
        {
            string output = "";
            int heightLength = _height.ToString().Length;
            
            for (int h = 0; h < _height; h++)
            {
                output += h.ToString().PadLeft(heightLength).PadRight(heightLength + 1);
                
                for (int w = 0; w < _width; w++)
                {
                    output += _map[h, w] == 0 ? "." : (char)_map[h, w];
                }

                output += Environment.NewLine;
            }

            Console.WriteLine(output);
        }
    }
}