﻿using AdventOfCode.Days;

IEnumerable<AdventOfCodeDay> solvedDays = new List<AdventOfCodeDay>()
{
    new Day1(),
    new Day2(),
    new Day3(),
    new Day4(),
    new Day5(),
    new Day6(),
    new Day7(),
    new Day8(),
    new Day9(),
    new Day10(),
    new Day11(),
    new Day12(),
    new Day13(),
    new Day14(),
    new Day15(),
    new Day16(),
    new Day17(),
    new Day18(),
    new Day19(),
    new Day20(),
    new Day21(),
    new Day22(),
    new Day23(),
    new Day24(),
    new Day25(),
};

foreach (AdventOfCodeDay solvedDay in solvedDays)
{
    solvedDay.PrintSolutions();
}