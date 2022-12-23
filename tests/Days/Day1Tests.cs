using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day1Tests : Day1
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "71924";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "210406";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}