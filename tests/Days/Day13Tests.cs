using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day13Tests : Day13
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "6235";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "22866";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}