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
            //input = @"class: 0-1 or 4-19
            //        row: 0-5 or 8-19
            //        seat: 0-13 or 16-19

            //        your ticket:
            //        11,12,13

            //        nearby tickets:
            //        3,9,18
            //        15,1,5
            //        5,14,9";

            var sections = input.SplitOnBlankLines().ToList();
            var rulesData = sections[0].AsLines().ToList();
            var ticketData = sections[1].AsLines().ToList()[1];
            var nearbyData = sections[2].AsLines().Skip(1).ToList();
            nearbyData.Add(ticketData); // add our ticket to the rest of the data.

            var rules = ParseRules(rulesData);
            var nearbyTickets = nearbyData.Select(line => line.SplitBySeparator(",").Select(Int32.Parse).ToList()).ToList();
            var validTickets = nearbyTickets.Where(t => TicketIsValid(t, rules)).ToList();

            // Now find all the possible field names for each field.

            var fields = new Dictionary<int, List<string>>();

            for (var field = 0; field < validTickets[0].Count(); field++) {
                var possibleFields = rules.Keys.ToList();

                for (var line = 0; line < validTickets.Count; line++) {
                    var value = validTickets[line][field];
                    var validFields = GetValidFieldnames(value, rules);

                    // Filter out candidates from possible that aren't in validFields
                    possibleFields = possibleFields.Where(name => validFields.Contains(name)).ToList();
                }

                fields[field] = possibleFields;
            }

            // Now repeatedly filter the list by assigning fields that have only one possible candidate.

            var fieldnames = new Dictionary<int, string>();

            while (fieldnames.Count < rules.Count) {
                var field = fields.First(f => f.Value.Count == 1).Key;
                var fieldname = fields[field][0];
                fieldnames[field] = fieldname;

                // Remove that field from consideration
                foreach (var item in fields) {
                    item.Value.Remove(fieldname);
                }
            }

            // Now we just need the "departure" fields from our ticket

            long total = 1;
            var ourTicket = validTickets.Last();

            foreach (var field in fieldnames.Where(k => k.Value.StartsWith("departure"))) {
                Out.Print($"{field.Value} is field {field.Key} which is {ourTicket[field.Key]}");
                total *= ourTicket[field.Key];
            }

            return total.ToString();
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
