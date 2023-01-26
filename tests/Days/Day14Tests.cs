using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day14Tests : Day14
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "610";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "27194";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}