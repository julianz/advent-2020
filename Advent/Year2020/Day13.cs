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
                        WriteLine($"Bus {bus} arrives at timestamp {timestamp}, {timestamp - earliest} min wait");
                        return ((timestamp - earliest) * bus).ToString();
                    }
                }

                timestamp++;
            }
        }

        public override string PartTwo(string input) {
            /*
             * The earliest timestamp that matches the list 17,x,13,19 is 3417.
             * 67,7,59,61 first occurs at timestamp 754018.
             * 67,x,7,59,61 first occurs at timestamp 779210.
             * 67,7,x,59,61 first occurs at timestamp 1261476.
             * 1789,37,47,1889 first occurs at timestamp 1202161486.
             */

            var departures = input.AsLines().Skip(1).First().SplitBySeparator(",").ToList();

            var buses = new List<int>();
            var busOffsets = new Dictionary<int, int>();

            // Get rid of the x's, store the offsets
            for (var offset = 0; offset < departures.Count; offset++) {
                if (departures[offset] == "x")
                    continue;
                var bus = Int32.Parse(departures[offset]);
                buses.Add(bus);
                busOffsets[bus] = offset;
            }

            var currentBus = buses[0];
            var nextBusIndex = 1;
            var nextBus = buses[nextBusIndex];
            var nextOffset = busOffsets[nextBus];

            long timestamp = 0;
            long stepSize = currentBus;

            var lastBus = false;
            var found = false;

            while (!found) {
                timestamp += stepSize;

                WriteLine($"Trying {timestamp} with bus {currentBus}");
                if ((timestamp + nextOffset) % nextBus != 0) {
                    continue;
                }

                if (!lastBus) {
                    stepSize *= nextBus;
                    WriteLine("Changing step size to {stepSize}");

                    currentBus = nextBus;
                    nextBusIndex += 1;
                    nextBus = buses[nextBusIndex];
                    nextOffset = busOffsets[nextBus];

                    lastBus = (nextBusIndex == buses.Count - 1);
                    if (lastBus)
                        WriteLine("Last bus!!");
                } else {
                    found = true;
                }
            }

            return timestamp.ToString();
        }
    }
}
