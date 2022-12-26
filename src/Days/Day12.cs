namespace AdventOfCode.Days;

public class Day12 : AdventOfCodeDay
{
    protected override int GetDay() => 12;

    protected override string GetTask1Solution()
    {
        (int[,] map, (int x, int y) start, (int x, int y) end) = BuildHeightMap();

        return GetShortestPathSteps(map, new[] { start }, end).ToString();
    }

    protected override string GetTask2Solution()
    {
        (int[,] map, _, (int x, int y) end) = BuildHeightMap();
        List<(int x, int y)> starts = new();
        
        int mapHeight = map.GetLength(0);
        int mapWidth = map.GetLength(1);

        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                if(map[x ,y] == 1)
                    starts.Add((x, y));
            }
        }
        
        return GetShortestPathSteps(map, starts, end).ToString();
    }

    private (int[,] map, (int x, int y) start, (int x, int y) end) BuildHeightMap()
    {
        string inputPath = Path.Combine("Assets", "day12.txt");
        string[] input = File.ReadAllLines(inputPath);
        int mapHeight = input.Length;
        int mapWidth = input.First().Trim().Length;
        (int x, int y) start = (0, 0);
        (int x, int y) end = (0, 0);
        int[,] heightMap = new int[mapHeight, mapWidth];

        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                switch (input[x][y])
                {
                    case 'S':
                        start = (x, y);
                        heightMap[x, y] = 1;
                        break;
                    case 'E':
                        end = (x, y);
                        heightMap[x, y] = 26;
                        break;
                    default:
                        heightMap[x, y] = input[x][y] - 'a' + 1;
                        break;
                }
            }
        }

        return (heightMap, start, end);
    }

    private int GetShortestPathSteps(int[,] map, IEnumerable<(int x, int y)> starts, (int x, int y) end)
    {
        Queue<((int x, int y) coords, int step)> queue = new();
        HashSet<(int x, int y)> visited = new();
        (int x, int y)[] nearNodes = { (0, 1), (1, 0), (0, -1), (-1, 0) };
        int mapHeight = map.GetLength(0);
        int mapWidth = map.GetLength(1);

        foreach ((int x, int y) start in starts)
        {
            queue.Enqueue(((start.x, start.y), 0));
        }

        while (queue.Any())
        {
            ((int x, int y), int step) = queue.Dequeue();

            if (!visited.Add((x, y)))
                continue;

            if (end.x == x && end.y == y)
                return step;

            foreach ((int relX, int relY) in nearNodes)
            {
                int neighborX = x + relX;
                int neighborY = y + relY;
                bool isInsideMap = neighborX >= 0 && neighborX < mapHeight && neighborY >= 0 && neighborY < mapWidth;

                if (isInsideMap)
                {
                    int currentValue = map[x, y];
                    int neighborValue = map[neighborX, neighborY];

                    if (neighborValue - currentValue <= 1)
                        queue.Enqueue(((neighborX, neighborY), step + 1));
                }
            }
        }

        return 0;
    }
}