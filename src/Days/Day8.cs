namespace AdventOfCode.Days;

public class Day8 : AdventOfCodeDay
{
    protected override int GetDay() => 8;

    protected override string GetTask1Solution()
    {
        int[][] trees = GetTreesMatrix();
        int rowsCount = trees.Length;
        int colsCount = trees.First().Length;
        int row;
        int col;

        int visibleTreesCount = rowsCount * 2 + colsCount * 2 - 4;

        for (row = 1; row < rowsCount - 1; row++)
        {
            for (col = 1; col < colsCount - 1; col++)
            {
                if (IsVisibleFromTop() || IsVisibleFromRight() || IsVisibleFromBottom() || IsVisibleFromLeft())
                    visibleTreesCount++;
            }
        }

        return visibleTreesCount.ToString();

        bool IsVisibleFromTop()
        {
            for (int i = 0; i < row; i++)
            {
                if (trees[row][col] <= trees[i][col])
                    return false;
            }

            return true;
        }

        bool IsVisibleFromRight()
        {
            for (int i = col + 1; i < colsCount; i++)
            {
                if (trees[row][col] <= trees[row][i])
                    return false;
            }

            return true;
        }

        bool IsVisibleFromBottom()
        {
            for (int i = row + 1; i < rowsCount; i++)
            {
                if (trees[row][col] <= trees[i][col])
                    return false;
            }

            return true;
        }

        bool IsVisibleFromLeft()
        {
            for (int i = 0; i < col; i++)
            {
                if (trees[row][col] <= trees[row][i])
                    return false;
            }

            return true;
        }
    }

    protected override string GetTask2Solution()
    {
        int[][] trees = GetTreesMatrix();
        int rowsCount = trees.Length;
        int colsCount = trees.First().Length;
        int row;
        int col;

        int highestScenicScore = 0;

        for (row = 0; row < rowsCount; row++)
        {
            for (col = 0; col < colsCount; col++)
            {
                int scenicScore = GetTopViewingDistance() * GetRightViewingDistance() * GetBottomViewingDistance() * GetLeftViewingDistance();
                highestScenicScore = Math.Max(highestScenicScore, scenicScore);
            }
        }

        return highestScenicScore.ToString();

        int GetTopViewingDistance()
        {
            int distance = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                distance++;
                if (trees[row][col] <= trees[i][col])
                    break;
            }

            return distance;
        }

        int GetRightViewingDistance()
        {
            int distance = 0;
            for (int i = col + 1; i < colsCount; i++)
            {
                distance++;
                if (trees[row][col] <= trees[row][i])
                    break;
            }

            return distance;
        }

        int GetBottomViewingDistance()
        {
            int distance = 0;
            for (int i = row + 1; i < rowsCount; i++)
            {
                distance++;
                if (trees[row][col] <= trees[i][col])
                    break;
            }

            return distance;
        }

        int GetLeftViewingDistance()
        {
            int distance = 0;
            for (int i = col - 1; i >= 0; i--)
            {
                distance++;
                if (trees[row][col] <= trees[row][i])
                    break;
            }

            return distance;
        }
    }

    private int[][] GetTreesMatrix()
    {
        string inputPath = Path.Combine("Assets", "day8.txt");
        return File.ReadLines(inputPath)
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();
    }
}