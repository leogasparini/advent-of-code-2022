using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day6Tests : Day6
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "1912";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "2122";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}