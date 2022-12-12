namespace Advent.Year2020 {
    [Day(2020, 6)]
    public class Day06 : DayBase {
        public override async Task<string> PartOne(string input) {
            var groups = input.SplitOnBlankLines();
            var total = 0;

            foreach (var group in groups) {
                var letters = String.Join("", group.Split());
                var hash = new HashSet<char>();

                foreach (var c in letters) {
                    hash.Add(c);
                }

                total += hash.Count;
            }

            return total.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var groups = input.SplitOnBlankLines();
            var total = 0;

            foreach (var group in groups) {
                var members = group.Split('\n').Count();
                var letters = String.Join("", group.Split());
                var stats = new Dictionary<char, int>();

                foreach (var c in letters) {
                    stats[c] = stats.GetValueOrDefault(c) + 1;
                }

                total += stats.Count(kv => kv.Value == members);
            }

            return total.ToString();
        }
    }
}
