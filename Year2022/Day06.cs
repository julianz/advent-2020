namespace Advent.Year2022 {
    [Day(2022, 6)]
    public class Day06 : DayBase {
        public override async Task<string> PartOne(string input) {
            return FindFirstUniqueToken(input, 4).ToString();
        }

        public override async Task<string> PartTwo(string input) {
            return FindFirstUniqueToken(input, 14).ToString();
        }

        int FindFirstUniqueToken(string input, int tokenLength) {
            for (var pos = 0; pos < (input.Length - tokenLength); pos++) {
                if (input.Slice(pos, tokenLength).Distinct().Count() == tokenLength) {
                    return (pos + tokenLength);
                }
            }

            return -1;
        }
    }
}
