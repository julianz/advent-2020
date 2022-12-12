namespace Advent.Year2022 {
    [Day(2022, 1)]
    public class Day01 : DayBase {
        public override async Task<string> PartOne(string input) {
            // Find the elf carrying the highest total calories

            return input.SplitOnBlankLines()        // group the input into a list of lines per elf
                .Select(elf => elf.AsInts().Sum())  // sum each elf's lines
                .Max()
                .ToString();
        }

        public override async Task<string> PartTwo(string input) {
            // Return the sum of the top 3 elves by total calories

            return input.SplitOnBlankLines()
                .Select(elf => elf.AsInts().Sum())
                .OrderByDescending(calories => calories)
                .Take(3)
                .Sum()
                .ToString();
        }
    }
}
