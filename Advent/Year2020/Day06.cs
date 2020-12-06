using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 6)]
    public class Day06 : DayBase {
        public override string PartOne(string input) {
            input = input.Replace("\r\n", "\n");
            var groups = input.Split("\n\n", 
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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

        public override string PartTwo(string input) {
            input = input.Replace("\r\n", "\n");
            var groups = input.Split("\n\n",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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
