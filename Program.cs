// See https://aka.ms/new-console-template for more information

using aoc_2023;
using aoc_2023.DayFive;
using aoc_2023.DayFour;
using aoc_2023.DayOne;
using aoc_2023.DayThree;
using aoc_2023.DayTwo;

SolutionRunner.Run(new DayOne());
SolutionRunner.Run(new DayTwo());
SolutionRunner.Run(new DayThree());
SolutionRunner.Run(new DayFour());
// Only run Part 1 because Part Two is _slow_.
SolutionRunner.Run(new DayFive(), SolutionRunner.Parts.One, false);