using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MoreLinq;

using Advent.Util;

namespace Advent.Year2021 {
    [Day(2021, 3)]
    public class Day03 : DayBase {
        public override string PartOne(string input) {
            var lineLength = 12;
            var values = Enumerable.Repeat(0, lineLength).ToList();

            foreach (var line in input.AsLines()) {
                for (var i = 0; i < lineLength; i++) {
                    values[i] = (line[i] == '1') ? values[i] + 1 : values[i] - 1;
                }
            }

            // values > 0 mean 1 is the most common digit, otherwise 0, and the reverse for epsilon
            var gammas = values.Select(v => (v > 0) ? 1 : 0).ToList();
            var epsilons = gammas.Select(v => (v == 0) ? 1 : 0).ToList();

            var gamma = Convert.ToInt32(String.Join("", gammas), 2);
            var epsilon = Convert.ToInt32(String.Join("", epsilons), 2);
            
            return (gamma * epsilon).ToString();
        }

        public override string PartTwo(string input) {
            var lines = input.AsLines().ToList();
            var lineLength = lines[0].Length;
            var colIndex = 0;

            // calc O2 rating
            while (lines.Count > 1 && colIndex < lineLength) {
                var column = VerticalSlice(lines, colIndex).ToList();
                var mcc = MostCommonChar(column);
                
                lines = lines.Where(line => line[colIndex] == mcc).ToList();

                colIndex++;
            }

            var o2rating = Convert.ToInt32(String.Join("", lines.First()), 2);

            // start again, calc CO2 rating
            lines = input.AsLines().ToList();
            colIndex = 0;

            while (lines.Count > 1 && colIndex < lineLength) {
                var column = VerticalSlice(lines, colIndex).ToList();
                var lcc = LeastCommonChar(column);

                lines = lines.Where(line => line[colIndex] == lcc).ToList();

                colIndex++;
            }

            var co2rating = Convert.ToInt32(String.Join("", lines.First()), 2);

            return (o2rating * co2rating).ToString();
        }

        // Return a vertical slice of the input array. NO ERROR HANDLING.
        internal IEnumerable<char> VerticalSlice(IEnumerable<string> input, int index) {
            return input.Select(s => s[index]);
        }

        internal char MostCommonChar(IReadOnlyCollection<char> input) {
            var length = input.Count;
            var ones = input.Count(c => c == '1');

            return ((ones * 1.0) / length >= 0.5) ? '1' : '0';
        }

        internal char LeastCommonChar(IReadOnlyCollection<char> input) {
            var length = input.Count;
            var zeroes = input.Count(c => c == '0');

            return ((zeroes * 1.0) / length <= 0.5) ? '0' : '1';
        }
    }
}
