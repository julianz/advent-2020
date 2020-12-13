using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 9)]
    public class Day09 : DayBase {
        public override string PartOne(string input) {
            var nums = input.AsLongs().ToList();
            var preamble = 25;
            var distance = 25;
            long target = 0;

            for (var i = preamble; i < nums.Count; i++) {
                target = nums[i];
                var found = false;
                for (var m = 1; m <= distance; m++) {
                    for (var n = 1; n <= distance; n++) {
                        if (m == n) continue;
                        if (nums[i - m] + nums[i - n] == target) {
                            //Out.Print($"{nums[i - m]} + {nums[i - n]} = {target}");
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }
                if (!found) break;            
            }

            return target.ToString();
        }

        public override string PartTwo(string input) {
            var nums = input.AsLongs().ToList();
            long target = 50047984;
            var pos = nums.IndexOf(target);
            long result = 0;

            for (var end = pos - 1; end > 0; end--) {
                for (var start = end - 1; start >= 0; start--) {
                    //Out.Print($"{start} - {end}");
                    long sum = 0;
                    for (var i = start; i <= end; i++) {
                        sum += nums[i];
                    }
                    if (sum == target) {
                        // here have to find the lowest and highest nums within the range.
                        var high = nums.GetRange(start, (end - start)).Max();
                        var low = nums.GetRange(start, (end - start)).Min();
                        result = high + low;
                        Out.Print($"Found: start: {start} end: {end} low: {low} high: {high} result: {result}");
                        break;
                    }
                    if (result > 0) break;
                }
                if (result > 0) break;
            }

            return result.ToString();
        }
    }
}
