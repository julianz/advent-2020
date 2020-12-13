using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 10)]
    public class Day10 : DayBase {
        public override string PartOne(string input) {
            var nums = input.AsLongs().ToList();
            nums.Sort();

            nums.Insert(0, 0);
            nums.Add(nums.Last() + 3);

            var ones = 0;
            var threes = 0;

            for (var i = 1; i < nums.Count(); i++) {
                var diff = nums[i] - nums[i - 1];
                switch (diff) {
                    case 1:
                        ones++;
                        break;
                    case 3:
                        threes++;
                        break;
                    default:
                        throw new Exception($"Unexpected difference {diff} at position {i}");
                }
            }

            return (ones * threes).ToString();
        }

        public override string PartTwo(string input) {
            var test1 = @"16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4";
            var test2 = @"28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3";

            //var nums = input.AsInts().ToList();
            var nums = test1.Split(", ").Select(n => Int32.Parse(n)).ToList();
            nums.Sort();

            nums.Insert(0, 0);
            nums.Add(nums.Last() + 3);

            var result = CountPaths(0, nums);

            throw new PuzzleNotSolvedException();
        }

        long CountPaths(int fromIndex, List<int> list) {
            var cache = new Dictionary<int, long>();
            
            if (!cache.ContainsKey(fromIndex)) {
                cache[fromIndex] = CountPathsInner(fromIndex, list);
                Out.Print($"{fromIndex}: {cache[fromIndex]}");
            }

            return cache[fromIndex];
        }

        long CountPathsInner(int fromIndex, List<int> list) {
            if (fromIndex == list.Count - 1) {
                return 1;
            }

            return 0;
        }
    }
}
