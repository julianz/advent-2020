namespace Advent.Year2015 {
    [Day(2015, 3)]
    public class Day03 : DayBase {

        public override async Task<string> PartOne(string input) {
            var locations = new HashSet<Location>();
            var loc = new Location(0, 0);

            // Store the initial location
            locations.Add(loc);

            foreach (var direction in input) {
                loc = loc.Move(direction);

                locations.Add(loc);
            }
            return locations.Count.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var locations = new HashSet<Location>();

            var santaLoc = new Location(0, 0);
            var robotLoc = new Location(0, 0);

            // Store the initial location
            locations.Add(santaLoc);

            for (var n = 0; n < input.Length; n += 2) {
                santaLoc = santaLoc.Move(input[n]);
                robotLoc = robotLoc.Move(input[n + 1]);

                locations.Add(santaLoc);
                locations.Add(robotLoc);
            }

            return locations.Count.ToString();
        }
    }

    record Location {
        const char North = '^';
        const char South = 'v';
        const char East = '>';
        const char West = '<';

        public int x;
        public int y;

        public Location(int x, int y) => 
            (this.x, this.y) = (x, y);

        public Location Move(char direction) {
            return direction switch
            {
                North => new Location(x, y + 1),
                South => new Location(x, y - 1),
                East => new Location(x + 1, y),
                West => new Location(x - 1, y),
                _ => throw new ArgumentOutOfRangeException($"No such direction '{direction}'")
            };
        }

        public override string ToString() =>
            $"Location ({x},{y})";
    }
}
