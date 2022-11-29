namespace Advent.Year2018 {
    [Day(2018, 1)]
    public class Day01 : DayBase {
        public override async Task<string> PartOne(string input) {

            foreach (var reading in input.AsInts()) {

            }

            return "foo";
        }

        public override async Task<string> PartTwo(string input) {
            throw new PuzzleNotSolvedException();
        }
    }
}
