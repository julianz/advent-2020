namespace Advent.Year2021 {
    [Day(2021, 2)]
    public class Day02 : DayBase {
        public override async Task<string> PartOne(string input) {

            var pos = 0;
            var depth = 0;

            foreach (var line in input.AsLines()) {
                var bits = line.Split(' ');
                switch (bits[0]) {
                    case "forward":
                        pos += Convert.ToInt32(bits[1]);
                        break;

                    case "down":
                        depth += Convert.ToInt32(bits[1]);
                        break;

                    case "up":
                        depth -= Convert.ToInt32(bits[1]);
                        break;
                }
            }

            return (pos * depth).ToString();
        }

        public override async Task<string> PartTwo(string input) {

            var pos = 0;
            var depth = 0;
            var aim = 0;

            foreach (var line in input.AsLines()) {
                var bits = line.Split(' ');
                switch (bits[0]) {
                    case "forward":
                        pos += Convert.ToInt32(bits[1]);
                        depth += (aim * Convert.ToInt32(bits[1]));
                        break;

                    case "down":
                        aim += Convert.ToInt32(bits[1]);
                        break;

                    case "up":
                        aim -= Convert.ToInt32(bits[1]);
                        break;
                }
            }

            return (pos * depth).ToString();
        }
    }
}
