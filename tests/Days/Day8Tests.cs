using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day8Tests : Day8
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "1782";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "474606";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}