using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 13)]
    public class Day13 : DayBase {
        public override string PartOne(string input) {
            var bits = input.AsLines().ToList();

            var earliest = Int32.Parse(bits[0]);
            var buses = bits[1].SplitBySeparator(",")
                .Where(x => x != "x")
                .Select(n => Int32.Parse(n))
                .ToList();

            var timestamp = earliest;
            while (true) {
                foreach (var bus in buses) {
                    if (timestamp % bus == 0) {
                        Out.Print($"Bus {bus} arrives at timestamp {timestamp}, {timestamp - earliest} min wait");
                        return ((timestamp - earliest) * bus).ToString();
                    }
                }

                timestamp++;
            }
        }

        public override string PartTwo(string input) {
            input = @"1000186
                    17,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,907,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,x,x,x,x,x,x,23,x,x,x,x,x,29,x,653,x,x,x,x,x,x,x,x,x,41,x,x,13";

            input = @"939
                    7,13,x,x,59,x,31,19";

            /**
             * find the biggest number in the list (59) => b
             * work through multiples of b and then work back and forwards through the 
             * list to satisfy the other conditions
             * 
             * e.g. at t = 59, is there a multiple of 31 at 61 and a multiple of 19 at 62? (no)
             * - should probably check the multiples in descending order as they're less likely to coincide
             * so find largest with offset 0, then work down the list and build a list of n and relative offset to check
             **/
            var departures = input.AsLines().Skip(1).First().SplitBySeparator(",").ToList();
            var buses = departures.Where(x => x != "x").Select(n => Int32.Parse(n)).ToList();
            buses.Sort(new DescendingComparer<int>());

            var busesToCheck = new Dictionary<int, int>(); // bus number and offset from highest bus num
            foreach (var bus in buses) {
                busesToCheck[bus] = departures.IndexOf(bus.ToString());
            }

            int highestBus = buses[0];
            int highestBusIndex = busesToCheck[highestBus];

            // now loop through multiples of highestBus until we find one that satisfies the criteria

            return "";
        }
    }

    public class DescendingComparer<T> : IComparer<T> where T : IComparable<T> {
        public int Compare(T x, T y) {
            return y.CompareTo(x);
        }
    }
}
