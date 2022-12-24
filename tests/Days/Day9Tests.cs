using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day9Tests : Day9
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "6503";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "2724";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}