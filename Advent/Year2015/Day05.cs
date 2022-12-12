namespace Advent.Year2015 {
    [Day(2015, 5)]
    public class Day05 : DayBase {
        Regex VowelPattern = new Regex(@".*[aeiou].*[aeiou].*[aeiou].*");
        Regex DoublePattern = new Regex(@"([a-z])\1");
        Regex ForbiddenPattern = new Regex(@"(ab|cd|pq|xy)");

        Regex DoubleDoublePattern = new Regex(@"([a-z][a-z]).*\1");
        Regex PiggyPattern = new Regex(@"([a-z])[a-z]\1");

        public override async Task<string> PartOne(string input) {
            var nice = 0;

            foreach (var line in input.AsLines()) {
                if (VowelPattern.IsMatch(line) &&
                    DoublePattern.IsMatch(line) &&
                   !ForbiddenPattern.IsMatch(line)) {

                    nice++;
                }
            }

            return nice.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var nice = 0;

            foreach (var line in input.AsLines()) {
                if (DoubleDoublePattern.IsMatch(line) &&
                    PiggyPattern.IsMatch(line)) {

                    nice++;
                }
            }

            return nice.ToString();
        }
    }
}
