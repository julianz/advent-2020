using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MoreLinq;

using Advent.Util;

namespace Advent.Year2021 {
    [Day(2021, 6)]
    public class Day06 : DayBase {
        public override string PartOne(string input) {
            input = "3,4,3,1,2";
            var school = input.SplitBySeparator().Select(Int32.Parse).ToList();
            var newFish = 9;
            var freshFish = new List<int>();

            //WriteLine($"Initial state: {String.Join(",", school)}");

            for (var day = 1; day <= 256; day++) {
                school.AddRange(freshFish);
                freshFish.Clear();

                for (var fish = 0; fish < school.Count; fish++) {
                    school[fish]--;
                    if (school[fish] == 0) {
                        freshFish.Add(newFish);
                    }
                    if (school[fish] < 0) { 
                        school[fish] = 6;
                    }
                }

                //WriteLine($"After {day,2} day{(day > 1 ? 's' : ' ')}: {String.Join(",", school)}");
            }

            return school.Count.ToString();
        }

        public override string PartTwo(string input) {
            //input = "3,4,3,1,2";
            var initialState = input.SplitBySeparator().Select(Int32.Parse).ToList();

            // Dictionary of <timer value, number of fish with that value>
            var school = new Dictionary<Int32, Int64>();
            foreach (var i in Enumerable.Range(0, 9)) {
                school[i] = 0;
            }

            foreach (var fish in initialState) {
                school[fish]++;
            }

            var freshFish = new List<int>();

            for (var day = 1; day <= 256; day++) {
                var breeders = school[0];
                for (var i = 1; i < school.Keys.Count; i++) {
                    school[i - 1] = school[i];
                }
                school[8] = breeders; // fresh fish
                school[6] += breeders; // beginning new cycle
            }

            return school.Values.Sum().ToString();
        }
    }
}
