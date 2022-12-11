using System.Diagnostics;
using static MoreLinq.Extensions.BatchExtension;

namespace Advent.Year2022 {
    [Day(2022, 3)]
    public class Day03 : DayBase {
        public override async Task<string> PartOne(string input) {
            return input.AsLines()
                .Select(line => {
                    var midpoint = line.Length / 2;

                    foreach (var ch in line.AsSpan(midpoint)) {
                        if (line.AsSpan(0, midpoint).Contains(ch)) {
                            return ScoreLetter(ch);
                        }
                    }

                    return 0;
                })
                .Sum()
                .ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = @"vJrwpWtwJgWrhcsFMMfFFhFp
            //        jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
            //        PmmdzqPrVvPwwTWBwg
            //        wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
            //        ttgJtRGJQctTZtZT
            //        CrZsJsPPZsGzwwsLwLmpwMDw";

            var groups = input.AsLines().Batch(3);
            var score = 0;

            foreach (var group in groups) {
                // Intersect each line with itself to find the single common letter.
                var letters = group.First().Distinct().ToArray();

                foreach (var line in group.Skip(1)) {
                    letters = letters.Intersect(line).ToArray();
                }

                Debug.Assert(letters.Length == 1);
                score += ScoreLetter(letters[0]);
            }

            return score.ToString();
        }

        const int lowerCaseOffset = 96;
        const int upperCaseOffset = 38;

        int ScoreLetter(char letter) {
            return letter switch {
                >= 'a' => (letter - lowerCaseOffset),
                _ => (letter - upperCaseOffset)
            };
        }
    }
}
