using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day7Tests : Day7
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "1325919";
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