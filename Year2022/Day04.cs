namespace Advent.Year2022 {
    [Day(2022, 4)]
    public class Day04 : DayBase {
        public override async Task<string> PartOne(string input) {
            return input.AsLines()
                .Where(line =>
                {
                    // Extract 4 ints from a line formatted "1-2,3-4"
                    var nums = line.Split(new char[] { '-', ',' }, 4)
                        .Select(n => Int32.Parse(n))
                        .ToArray();

                    // Return True where one range completely contains another
                    return ((nums[0] >= nums[2] && nums[1] <= nums[3]) ||
                            (nums[2] >= nums[0] && nums[3] <= nums[1]));
                })
                .Count()
                .ToString();
        }

        public override async Task<string> PartTwo(string input) {
            return input.AsLines()
                .Where(line =>
                {
                    // Extract 4 ints from a line formatted "1-2,3-4"
                    var nums = line.Split(new char[] { '-', ',' }, 4)
                        .Select(n => Int32.Parse(n))
                        .ToArray();

                    // return True where one range overlaps with another
                    return (nums[1] >= nums[2] && nums[0] <= nums[3]);
                })
                .Count()
                .ToString();
        }
    }
}
