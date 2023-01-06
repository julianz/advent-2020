namespace Advent.Year2022 {
    [Day(2022, 8)]
    public class Day08 : DayBase {
        public override async Task<string> PartOne(string input) {
            var lines = input.AsLines().ToList();
            var width = lines[0].Length;
            var height = lines.Count;
            var forest = new int[height, width];

            // Load input as a grid of ints
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    forest[y, x] = int.Parse(lines[y][x].ToString());
                }
            }

            // All the outside trees are visible
            var visible = (2 * width) + (2 * (height - 2));

            int tree;

            for (var y = 1; y < height - 1; y++) {
                for (var x = 1; x < width - 1; x++) {
                    tree = forest[y, x];

                    // Find the tallest trees in every direction
                    var west = Enumerable.Range(0, x).Select(n => forest[y, n]).Max();
                    var east = Enumerable.Range(x + 1, width - (x + 1)).Select(n => forest[y, n]).Max();
                    var north = Enumerable.Range(0, y).Select(n => forest[n, x]).Max();
                    var south = Enumerable.Range(y + 1, height - (y + 1)).Select(n => forest[n, x]).Max();

                    // If the tree is visible in any direction then it's visible
                    var directions = new int[] { north, south, east, west };
                    if (directions.Any(d => d < tree)) {
                        visible++;
                    }
                }
            }

            return visible.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = @"30373
            //          25512
            //          65332
            //          33549
            //          35390";

            var lines = input.AsLines().ToList();
            var width = lines[0].Length;
            var height = lines.Count;
            var forest = new int[height, width];

            // Load input as a grid of ints
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    forest[y, x] = int.Parse(lines[y][x].ToString());
                }
            }

            int current;
            int score;
            int distance;
            int highscore = 0;

            // don't count any edge locations as they will score 0
            for (var y = 1; y < height - 1; y++) {
                for (var x = 1; x < width - 1; x++) {
                    current = forest[y, x];

                    // get the list of trees in all four directions heading away from the current tree

                    var north = Enumerable.Range(0, y).Select(n => forest[n, x]).Reverse();
                    var south = Enumerable.Range(y + 1, height - (y + 1)).Select(n => forest[n, x]);
                    var east = Enumerable.Range(x + 1, width - (x + 1)).Select(n => forest[y, n]);
                    var west = Enumerable.Range(0, x).Select(n => forest[y, n]).Reverse();

                    List<IEnumerable<int>> fourwinds = new List<IEnumerable<int>>
                    {
                        north, south, east, west
                    };

                    score = 1; // (start with 1 because we're multiplying)

                    // calculate the view scores in each direction

                    foreach (var direction in fourwinds) {
                        distance = 0;

                        foreach (var tree in direction) {
                            distance++;
                            if (tree >= current) {
                                break;
                            }
                        }

                        score *= distance;
                    }

                    if (score > highscore) {
                        highscore = score;
                    }
                }
            }

            return highscore.ToString();
        }
    }
}
