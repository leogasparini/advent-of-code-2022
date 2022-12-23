using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day4Tests : Day4
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "569";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "936";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}