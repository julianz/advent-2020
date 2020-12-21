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
                    if (!NumberIsValid(number, rules)) {
                        Out.Print($"{number} is not valid for any rule");
                        invalidTotal += number;
                    }
                }
            }
            
            return invalidTotal.ToString();
        }

        public override string PartTwo(string input) {
            input = @"class: 0-1 or 4-19
                    row: 0-5 or 8-19
                    seat: 0-13 or 16-19

                    your ticket:
                    11,12,13

                    nearby tickets:
                    3,9,18
                    15,1,5
                    5,14,9";

            var sections = input.SplitOnBlankLines().ToList();
            var rulesData = sections[0].AsLines().ToList();
            var ticketData = sections[1].AsLines().ToList()[1];
            var nearbyData = sections[2].AsLines().Skip(1).ToList();

            var rules = ParseRules(rulesData);
            var nearbyTickets = nearbyData.Select(line => line.SplitBySeparator(",").Select(Int32.Parse)).ToList();
            var validTickets = nearbyTickets.Where(t => TicketIsValid(t, rules)).ToList();

            var fields = new Dictionary<int, string>();
            var fieldCount = rules.Keys.Count;
            var fieldsFound = 0;

            foreach (var t in validTickets) {
                var ticket = t.ToList();
                for (var v = 0; v < ticket.Count; v++) {
                    var validFields = GetValidFieldnames(ticket[v], rules);
                    if (validFields.Count == 1) {
                        // this is the only possible field that fits this value
                        Out.Print($"Field {v} must be {validFields[0]}");
                        fields[v] = validFields[0];
                    }
                    Out.Print($"Field {v} could be {String.Join(",", validFields)}");
                }
            }

            foreach (var field in fields) {
                Out.Print($"{field.Key} => {field.Value}");
            }

            return "nope";
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

        bool NumberIsValid(int value, Dictionary<string, List<int>> rules) {
            foreach (var rule in rules.Values) {
                if ((value >= rule[0] && value <= rule[1]) ||
                    (value >= rule[2] && value <= rule[3])) {

                    return true;
                }
            }
            return false;
        }

        bool TicketIsValid(IEnumerable<int> ticket, Dictionary<string, List<int>> rules) {
            foreach (var value in ticket) {
                if (!NumberIsValid(value, rules)) {
                    return false;
                }
            }
            return true;
        }

        private IList<string> GetValidFieldnames(int value, Dictionary<string, List<int>> rules) {
            var fieldnames = new List<string>();
            foreach (var kv in rules) {
                var rule = kv.Value;
                if ((value >= rule[0] && value <= rule[1]) ||
                    (value >= rule[2] && value <= rule[3])) {

                    fieldnames.Add(kv.Key);
                }
            }
            return fieldnames;
        }
    }
}
