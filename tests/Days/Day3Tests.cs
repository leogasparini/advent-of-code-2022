using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day3Tests : Day3
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "7716";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "2973";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}