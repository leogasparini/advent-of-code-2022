namespace AdventOfCode.Days;

public class Day3 : AdventOfCodeDay
{
    protected override int GetDay() => 3;

    protected override string GetTask1Solution()
    {
        return GetItemsToArrange()
            .Select(ToPriority)
            .Sum()
            .ToString();
    }
    
    private static IEnumerable<char> GetItemsToArrange()
    {
        string inputPath = Path.Combine("Assets", "day3.txt");
        
        return File.ReadLines(inputPath)
            .Select(rucksack =>
            {
                int compartmentSize = rucksack.Length / 2;
                string compartment1 = rucksack.Substring(0, compartmentSize);
                string compartment2 = rucksack.Substring(compartmentSize, compartmentSize);

                IEnumerable<char> compartment1Items = compartment1.Distinct();
                IEnumerable<char> compartment2Items = compartment2.Distinct();
                
                return compartment1Items.Intersect(compartment2Items).Single();
            });
    }

    private int ToPriority(char item)
    {
        return item >= 'a' ? item - 96 : item - 38;
    }
}