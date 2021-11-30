using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MoreLinq;

using Advent.Util;

namespace Advent.Year2015 {
    [Day(2015, 6)]
    public class Day06 : DayBase {
        public override string PartOne(string input) {

            var pattern = new Regex(@"(turn on|toggle|turn off) (\d+),(\d+) through (\d+),(\d+)");

            var gridRows = 1000;
            var gridCols = 1000;
            var grid = new HashSet<int>(gridRows * gridCols);

            foreach (var line in input.AsLines()) {
                var match = pattern.Match(line);
                if (!match.Success) {
                    throw new ArgumentException(line);
                }

                var fromX = Convert.ToInt32(match.Groups[2].Value);
                var fromY = Convert.ToInt32(match.Groups[3].Value);
                var toX = Convert.ToInt32(match.Groups[4].Value);
                var toY = Convert.ToInt32(match.Groups[5].Value);

                var command = match.Groups[1].Value;

                for (var y = fromY; y <= toY; y++) {
                    for (var x = fromX; x <= toX; x++) {
                        var cell = (y * gridCols) + x;

                        switch (command) {
                            case "turn on":
                                grid.Add(cell);
                                break;

                            case "turn off":
                                grid.Remove(cell);
                                break;

                            case "toggle":
                                if (grid.Contains(cell)) {
                                    grid.Remove(cell);
                                } else {
                                    grid.Add(cell);
                                }
                                break;
                        }
                    }
                }
            }

            return grid.Count.ToString();
        }

        public override string PartTwo(string input) {
            throw new PuzzleNotSolvedException();
        }
    }
}
