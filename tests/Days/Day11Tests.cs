using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day11Tests : Day11
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "98280";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "TODO";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}