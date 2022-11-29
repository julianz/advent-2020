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

        private readonly Regex InputPattern = new Regex(@"(turn on|toggle|turn off) (\d+),(\d+) through (\d+),(\d+)", RegexOptions.Compiled);

        private const int GridRows = 1000;
        private const int GridCols = 1000;

        public override string PartOne(string input) {

            var grid = new HashSet<int>(GridRows * GridCols);

            foreach (var line in input.AsLines()) {
                var match = InputPattern.Match(line);
                if (!match.Success) {
                    throw new ArgumentException(line);
                }

                var fromX = Convert.ToInt32(match.Groups[2].Value);
                var fromY = Convert.ToInt32(match.Groups[3].Value);
                var toX = Convert.ToInt32(match.Groups[4].Value);
                var toY = Convert.ToInt32(match.Groups[5].Value);

                var command = match.Groups[1].Value;

                int cell;
                for (var y = fromY; y <= toY; y++) {
                    for (var x = fromX; x <= toX; x++) {
                        cell = (y * GridCols) + x;

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
            var grid = new Dictionary<int, int>(GridRows * GridCols);

            foreach (var line in input.AsLines()) {
                var match = InputPattern.Match(line);
                if (!match.Success) {
                    throw new ArgumentException(line);
                }

                var fromX = Convert.ToInt32(match.Groups[2].Value);
                var fromY = Convert.ToInt32(match.Groups[3].Value);
                var toX = Convert.ToInt32(match.Groups[4].Value);
                var toY = Convert.ToInt32(match.Groups[5].Value);

                var command = match.Groups[1].Value;

                int cell;
                for (var y = fromY; y <= toY; y++) {
                    for (var x = fromX; x <= toX; x++) {
                        cell = (y * GridCols) + x;

                        switch (command) {
                            case "turn on":
                                // add 1
                                grid[cell] = (grid.Keys.Contains(cell) ? grid[cell] : 0) + 1;
                                break;

                            case "turn off":
                                // subtract 1 to a minimum of 0
                                grid[cell] = Math.Max((grid.Keys.Contains(cell) ? grid[cell] : 0) - 1, 0);
                                break;

                            case "toggle":
                                // add 2
                                grid[cell] = (grid.Keys.Contains(cell) ? grid[cell] : 0) + 2;
                                break;
                        }
                    }
                }
            }

            return grid.Values.Sum().ToString();
        }
    }
}
