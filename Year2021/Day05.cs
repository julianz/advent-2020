using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MoreLinq;

using Advent.Util;

namespace Advent.Year2021 {
    [Day(2021, 5)]
    public class Day05 : DayBase {
        public override string PartOne(string input) {

            var grid = new Dictionary<Coords, int>();
            var pairs = input.AsLines()
                .Select(s => s.SplitBySeparator("->"))
                .ToList();

            foreach (var p in pairs) {
                var start = p[0].AsCoords();
                var end = p[1].AsCoords();

                foreach (var coord in PlotStraightPath(start, end)) {
                    grid[coord] = (grid.ContainsKey(coord) ? grid[coord] + 1 : 1);
                }
            }

            return grid.Values.Count(v => v > 1).ToString();
        }

        public override string PartTwo(string input) {

            var grid = new Dictionary<Coords, int>();
            var pairs = input.AsLines()
                .Select(s => s.SplitBySeparator("->"))
                .ToList();

            foreach (var p in pairs) {
                var start = p[0].AsCoords();
                var end = p[1].AsCoords();

                foreach (var coord in PlotStraightPath(start, end, allowDiagonals: true)) {
                    grid[coord] = (grid.ContainsKey(coord) ? grid[coord] + 1 : 1);
                }
            }

            return grid.Values.Count(v => v > 1).ToString();
        }

        /// <summary>
        /// Iterator yields a stream of coords from start to end point, inclusive.
        /// </summary>
        private IEnumerable<Coords> PlotStraightPath(Coords start, Coords end, bool allowDiagonals = false) {

            var xdist = end.X - start.X;
            var ydist = end.Y - start.Y;

            // break if we're limiting to vertical/horizontal only
            if (!allowDiagonals && xdist != 0 && ydist != 0)
                yield break;

            var x = start.X;
            var y = start.Y;

            var xstep = Math.Clamp(xdist, -1, 1);
            var ystep = Math.Clamp(ydist, -1, 1);

            while (true) {
                var curr = new Coords(x, y);
                yield return curr;

                if (curr == end) {
                    yield break;
                }
                else {
                    x += xstep;
                    y += ystep;
                }
            }
        }
    }
}
