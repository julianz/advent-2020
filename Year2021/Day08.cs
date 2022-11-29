namespace Advent.Year2021 {
    [Day(2021, 8)]
    public class Day08 : DayBase {
        public override async Task<string> PartOne(string input) {
            var simpleDigits = new List<int> { 2, 3, 4, 7 };

            var count = input.AsLines()
                .Sum(line => line.SplitBySeparator("|")[1].SplitBySeparator(" ")
                    .Count(d => simpleDigits.Contains(d.Length)));

            return count.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var total = 0;

            foreach (var line in input.AsLines()) {
                var numbers = new Dictionary<string, int>();

                var bits = line.SplitBySeparator("|");
                var clue = bits[0].SplitBySeparator(" ").Select(w => w.SortChars()).ToList();
                var signal = bits[1].SplitBySeparator(" ").Select(w => w.SortChars()).ToList();

                // Find the easy ones
                numbers[clue.Where(w => w.Length == 2).First()] = 1;
                numbers[clue.Where(w => w.Length == 4).First()] = 4;
                numbers[clue.Where(w => w.Length == 3).First()] = 7;
                numbers[clue.Where(w => w.Length == 7).First()] = 8;

                var fives = clue.Where(w => w.Length == 5).ToList();
                var sixes = clue.Where(w => w.Length == 6).ToList();

                // Overlay the 1 and 4 segments onto the remaining patterns
                // to figure out each remaining digit
                var oneSegments = numbers.First(kv => kv.Value == 1).Key.ToArray();
                var fourSegments = numbers.First(kv => kv.Value == 4).Key.ToArray();

                numbers[fives.First(w => oneSegments.Count(c => w.Contains(c)) == 2)] = 3;
                fives.Remove(numbers.First(kv => kv.Value == 3).Key);
                numbers[fives.First(w => fourSegments.Count(c => w.Contains(c)) == 2)] = 2;
                numbers[fives.First(w => fourSegments.Count(c => w.Contains(c)) == 3)] = 5;

                numbers[sixes.First(w => fourSegments.Count(c => w.Contains(c)) == 4)] = 9;
                sixes.Remove(numbers.First(kv => kv.Value == 9).Key);
                numbers[sixes.First(w => oneSegments.Count(c => w.Contains(c)) == 2)] = 0;
                numbers[sixes.First(w => oneSegments.Count(c => w.Contains(c)) == 1)] = 6;

                // Now look up the words in the signal to decode the answer

                var output = 0;
                foreach (var digit in signal) {
                    output = (output * 10) + numbers[digit];
                }

                total += output;
            }

            return total.ToString();
        }
    }
}
