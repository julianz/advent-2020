namespace Advent.Year2020 {
    [Day(2020, 3)]
    public class Day03 : DayBase {
        public override async Task<string> PartOne(string input) {

            var map = input.AsLines().ToList();
            var height = map.Count;
            var col = 0;
            var row = 0;
            var trees = 0;

            while (row < height - 1) {
                col += 3;
                row += 1;
                Console.WriteLine($"Moving to {col}, {row}");
                if (IsTreeAt(map, col, row)) {
                    trees++;
                    Console.WriteLine($"Tree!");
                }
            }

            return trees.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var map = input.AsLines().ToList();
            var height = map.Count;
            var slopes = new List<(int across, int down)>
            {
                (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)
            };

            var totalTrees = 1; // so the first multiplication works

            foreach (var slope in slopes) {
                var col = 0;
                var row = 0;
                var trees = 0;

                while (row < height - 1) {
                    col += slope.across;
                    row += slope.down;
                    if (IsTreeAt(map, col, row)) {
                        trees++;
                    }
                }

                Console.WriteLine($"Moving {slope.across}x{slope.down} found {trees} trees");
                totalTrees *= trees;
            }

            return totalTrees.ToString();
        }

        private bool IsTreeAt(List<string> map, int col, int row) {
            var rowData = map[row];
            var colWrapped = col % rowData.Length;
            return (rowData[colWrapped] == '#');
        }
    }
}
