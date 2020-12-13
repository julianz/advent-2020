using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 11)]
    public class Day11 : DayBase {
       
        public override string PartOne(string input) {
            //input = @"L.LL.LL.LL
            //            LLLLLLL.LL
            //            L.L.L..L..
            //            LLLL.LL.LL
            //            L.LL.LL.LL
            //            L.LLLLL.LL
            //            ..L.L.....
            //            LLLLLLLLLL
            //            L.LLLLLL.L
            //            L.LLLLL.LL";

            var grid = new Grid(input.AsLines());
            //grid.Print();

            while (grid.ChangedCells > 0) {
                grid.Step(grid.GetNeighbours, crowdedThreshold: 4);
                //grid.Print();
            }

            grid.Print(summary: true);
            return grid.OccupiedSeats.ToString();
        }

        public override string PartTwo(string input) {
            var grid = new Grid(input.AsLines());
            //grid.Print();

            while (grid.ChangedCells > 0) {
                grid.Step(grid.CountSeatsSeenFrom, crowdedThreshold: 5);
                //grid.Print();
            }

            grid.Print(summary: true);
            return grid.OccupiedSeats.ToString();
        }
    }

    class Grid {
        readonly List<string> _grid = new List<string>();
        int _width;
        int _height;

        const char Empty = 'L';
        const char Full = '#';
        const char Floor = '.';

        public int ChangedCells { get; private set; } = 999; // magic start value so we don't trip a > 0 check

        public int Generation { get; private set; }

        public int OccupiedSeats => _grid.Sum(row => row.Count(seat => seat == Full));

        public Grid(IEnumerable<String> input) {
            _grid.Clear();
            _grid.AddRange(input);
            _width = _grid[0].Length;
            _height = _grid.Count;
        }

        public void Print(bool summary = false) {
            Out.Print($"Generation: { Generation }\nChanged: { ((Generation > 0) ? ChangedCells : 0) }\nOccupied: { OccupiedSeats }");
            Out.NewLine();

            if (!summary) {
                _grid.ForEach(Out.Print);
                Out.NewLine();
            }
        }

        public int Step(Func<int, int, int> getNeighbours, int crowdedThreshold) {
            Generation++;

            var newGrid = new List<String>();
            var changes = 0;

            for (int y = 0; y < _height; y++) {
                var row = "";
                for (int x = 0; x < _width; x++) {
                    var cell = _grid[y][x];
                    var neighbours = getNeighbours(x, y);

                    var newCell = cell switch
                    {
                        Floor => Floor,
                        Empty => (neighbours == 0) ? Full : Empty,
                        Full => (neighbours < crowdedThreshold) ? Full : Empty,
                        _ => throw new ArgumentOutOfRangeException("Unexpected item in the gridding area: " + cell)
                    };

                    if (cell != newCell) {
                        changes += 1;
                    }

                    row += newCell;
                }
                newGrid.Add(row);
            }

            _grid.Clear();
            _grid.AddRange(newGrid);

            ChangedCells = changes;

            return changes;
        }

        public int GetNeighbours(int x, int y) {
            // Shortcut - we don't care how many neighbours a floor tile has.
            if (_grid[y][x] == Floor) {
                return 0;
            }

            var neighbours = 0;

            for (int dy = -1; dy <= 1; dy++) {
                for (int dx = -1; dx <= 1; dx++) {
                    if (dx == 0 && dy == 0) {
                        // Don't count the central cell as a neighbour
                        continue;
                    }

                    var nx = x + dx;
                    var ny = y + dy;

                    if (nx < 0 || ny < 0 || nx >= _width || ny >= _height) {
                        // Don't count cells off the edge of the grid
                        continue;
                    }

                    switch (_grid[ny][nx]) {
                        case Full:
                            neighbours++;
                            break;

                        case Empty:
                        case Floor:
                            // nothing
                            break;

                        default:
                            // um no
                            throw new ArgumentOutOfRangeException("Unexpected item in the gridding area: " + _grid[ny][nx]);
                    }
                }
            }

            return neighbours;
        }
        public int CountSeatsSeenFrom(int x, int y) {
            // Shortcut - we don't care how many neighbours a floor tile has.
            if (_grid[y][x] == Floor) {
                return 0;
            }

            var seen = FindFirstSeat(x, y, 1, 0)
                + FindFirstSeat(x, y, 1, 1)
                + FindFirstSeat(x, y, 0, 1)
                + FindFirstSeat(x, y, -1, 1)
                + FindFirstSeat(x, y, -1, 0)
                + FindFirstSeat(x, y, -1, -1)
                + FindFirstSeat(x, y, 0, -1)
                + FindFirstSeat(x, y, 1, -1);

            return seen;
        }

        /// <summary>
        /// Move in a direction until we find a seat or the edge.
        /// Return 1 if the seat is full, 0 otherwise.
        /// </summary>
        int FindFirstSeat(int x, int y, int dx, int dy) {
            while (true) {
                x += dx;
                y += dy;

                // if we found the edge, return
                if (x < 0 || y < 0 || x >= _width || y >= _height) {
                    return 0;
                }

                if (_grid[y][x] == Empty) {
                    return 0;
                } else if (_grid[y][x] == Full) {
                    return 1;
                }

                // must be a floor tile... move one step further
            }
        }
    }
}

