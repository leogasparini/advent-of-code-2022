using AdventOfCode.Days;

namespace AdventOfCode.Tests.Days;

public class Day5Tests : Day5
{
    [Fact]
    public void GetTask1Solution_ShouldReturnExpectedSolution()
    {
        string expected = "MQSHJMWNH";
        string actual = GetTask1Solution();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetTask2Solution_ShouldReturnExpectedSolution()
    {
        string expected = "";
        string actual = GetTask2Solution();
        
        Assert.Equal(expected, actual);
    }
}