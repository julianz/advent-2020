using System;
using System.Collections.Generic;
using System.Linq;

using MoreLinq;

using Advent.Util;

namespace Advent.Year2021 {
    [Day(2021, 9)]
    public class Day09 : DayBase {
        public override string PartOne(string input) {

            var totalRisk = 0;
            var lines = input.AsLines().ToList();
            var width = lines[0].Length;
            var height = lines.Count;
            var cave = new int[width, height];

            // Create the cave
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    cave[x, y] = Convert.ToInt32(lines[y][x].ToString());
                }
            }

            // Check the cave
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    var neighbours = Neighbours(cave, x, y);
                    if (cave[x, y] < neighbours.Min()) {
                        // This is a low point
                        totalRisk += cave[x, y] + 1;
                    }
                }
            }

            return totalRisk.ToString();
        }

        public override string PartTwo(string input) {

            var lines = input.AsLines().ToList();
            var width = lines[0].Length;
            var height = lines.Count;
            var cave = new int[width, height];

            // Create the cave with only 9's and 0's
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    cave[x, y] = lines[y][x].ToString() == "9" ? 9 : 0;
                }
            }

            //DumpCave(cave);

            var pools = new List<int>();

            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    if (cave[x, y] == 0) {
                        pools.Add(FindPool(cave, x, y));
                    }
                }
            }

            return pools.OrderByDescending(p => p)
                        .Take(3)
                        .Aggregate((a, b) => a * b)
                        .ToString();
        }

        private static IEnumerable<int> Neighbours(int[,] cave, int x, int y) {
            var neighbours = new List<int>();

            if (x > 0)
                neighbours.Add(cave[x - 1, y]);
            if (x < cave.GetUpperBound(0))
                neighbours.Add(cave[x + 1, y]);
            if (y > 0)
                neighbours.Add(cave[x, y - 1]);
            if (y < cave.GetUpperBound(1))
                neighbours.Add(cave[x, y + 1]);

            return neighbours;
        }

        private static int FindPool(int[,] cave, int x, int y) {
            if (x < 0 || y < 0 || 
                x > cave.GetUpperBound(0) ||
                y > cave.GetUpperBound(1) ||
                cave[x, y] != 0) {

                return 0;
            }

            var size = 1;
            cave[x, y] = 1;

            size += FindPool(cave, x + 1, y);
            size += FindPool(cave, x, y + 1);
            size += FindPool(cave, x - 1, y);
            size += FindPool(cave, x, y - 1);

            return size;
        }

        private static void DumpCave(int[,] cave) {
            for (var y = 0; y <= cave.GetUpperBound(1); y++) {
                for (var x = 0; x <= cave.GetUpperBound(0); x++) {
                    Console.Write(cave[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
