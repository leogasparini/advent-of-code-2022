using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day12Tests : Day12
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "517";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "512";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}