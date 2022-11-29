namespace Advent.Year2015 {
    [Day(2015, 1)]
    public class Day01: DayBase {
        const char Up = '(';
        const char Down = ')';

        public override async Task<string> PartOne(string input) {
            var ups = input.Count(c => c == Up);
            var downs = input.Count(c => c == Down);

            return (ups - downs).ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var floor = 0;
            var step = 0;

            foreach (var c in input) {
                step++;
                floor += (c == Up) ? 1 : -1;

                if (floor == -1) {
                    return step.ToString();
                }
            }

            return "Not found";
        }
    }
}
