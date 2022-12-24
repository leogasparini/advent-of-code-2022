using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day10Tests : Day10
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "14760";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = @"
####.####..##..####.###..#..#.###..####.
#....#....#..#.#....#..#.#..#.#..#.#....
###..###..#....###..#..#.#..#.#..#.###..
#....#....#.##.#....###..#..#.###..#....
#....#....#..#.#....#.#..#..#.#.#..#....
####.#.....###.####.#..#..##..#..#.####.
";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}