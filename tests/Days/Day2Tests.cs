using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day2Tests : Day2
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "13009";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "10398";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}