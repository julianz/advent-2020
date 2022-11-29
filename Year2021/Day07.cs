namespace Advent.Year2021 {
    [Day(2021, 7)]
    public class Day07 : DayBase {
        public override async Task<string> PartOne(string input) {
            //input = "16,1,2,0,4,2,7,1,2,14";
            var crabs = input.SplitBySeparator().Select(Int32.Parse).ToList();

            var minCrab = crabs.Min();
            var maxCrab = crabs.Max();
            var leastFuel = Int32.MaxValue;
            var bestCrab = 0;

            for (int pos = minCrab; pos <= maxCrab; pos++) {
                var fuelCost = crabs.Select(c => Math.Abs(c - pos)).Sum();

                if (fuelCost < leastFuel) {
                    leastFuel = fuelCost;
                    bestCrab = pos;
                }
            }

            return leastFuel.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = "16,1,2,0,4,2,7,1,2,14";
            var crabs = input.SplitBySeparator().Select(Int32.Parse).ToList();

            var minCrab = crabs.Min();
            var maxCrab = crabs.Max();
            var leastFuel = Int32.MaxValue;
            var bestCrab = 0;

            // Memoize the fuel calculation for speed
            Func<int, int> triangularSum = (val) => Enumerable.Range(1, val).Sum();
            var fuelCalc = triangularSum.Memoize();

            for (int pos = minCrab; pos <= maxCrab; pos++) {
                var fuelCost = crabs.Select(c => fuelCalc(Math.Abs(c - pos))).Sum();

                if (fuelCost < leastFuel) {
                    leastFuel = fuelCost;
                    bestCrab = pos;
                }
            }

            return leastFuel.ToString();
        }
    }
}
