namespace Advent.Year2015 {
    [Day(2015, 2)]
    public class Day02 : DayBase {
        public override async Task<string> PartOne(string input) {
            var total = 0;

            foreach (var line in input.AsLines()) {
                var sides = line.Split('x').Select(s => Int32.Parse(s)).ToList();
                sides.Sort();

                total += (3 * sides[0] * sides[1]) + (2 * sides[1] * sides[2]) + (2 * sides[0] * sides[2]);
            }

            return total.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var total = 0;

            foreach (var line in input.AsLines()) {
                var sides = line.Split('x').Select(s => Int32.Parse(s)).ToList();
                sides.Sort();

                total += (2 * sides[0]) + (2 * sides[1]) + (sides[0] * sides[1] * sides[2]);
            }

            return total.ToString();
        }
    }
}
