using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 16)]
    public class Day16 : DayBase {
        public override string PartOne(string input) {
            var sections = input.SplitOnBlankLines().ToList();
            var rulesData = sections[0].AsLines().ToList();
            var nearbyData = sections[2].AsLines().Skip(1).ToList();

            var rules = ParseRules(rulesData);
            long invalidTotal = 0;
            
            foreach (var line in nearbyData) {
                foreach (var number in line.Split(",").Select(Int32.Parse)) {
                    if (!IsValidNumber(number, rules)) {
                        Out.Print($"{number} is not valid for any rule");
                        invalidTotal += number;
                    }
                }
            }
            
            return invalidTotal.ToString();
        }

        public override string PartTwo(string input) {
            throw new PuzzleNotSolvedException();
        }

		Dictionary<string, List<int>> ParseRules(List<string> lines) {
            var rules = new Dictionary<string, List<int>>(lines.Count);
            foreach (var line in lines) {
                var bits = line.Split(":", StringSplitOptions.TrimEntries);
                var name = bits[0];
                var numbers = new List<int>(4);
                var pairs = bits[1].Split("or", StringSplitOptions.TrimEntries);
                foreach (var pair in pairs) {
                    numbers.AddRange(pair.Split("-").Select(Int32.Parse));
                }
                rules[name] = numbers;
            }
            return rules;
        }

        bool IsValidNumber(int number, Dictionary<string, List<int>> rules) {
            var valid = false;
            foreach (var rule in rules.Values) {
                if ((number >= rule[0] && number <= rule[1]) ||
                    (number >= rule[2] && number <= rule[3])) {
                    valid = true;
                    break;
                }
            }
            return valid;
        }
	}
}
