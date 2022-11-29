namespace Advent.Year2020 {
    [Day(2020, 1)]
    public class Day01 : DayBase {

        public override async Task<string> PartOne(string input) {
            var nums = input.AsInts().ToList();
            nums.Sort();

            foreach(var n1 in nums) {
                if (FindMatch(nums, 2020, n1, out var n2)) {
                    var product = n1 * n2;
                    Console.WriteLine($"Found: {n1} * {n2} = {product}");

                    return product.ToString();
                }
            }

            return "Not found";
        }

        public override async Task<string> PartTwo(string input) {
            var product = 0;
            var nums = input.AsInts().ToList();

            foreach (var n1 in nums) {
                foreach (var n2 in nums) {
                    foreach (var n3 in nums) {
                        if (n1 + n2 + n3 == 2020) {
                            product = n1 * n2 * n3;
                            Console.WriteLine("{0} + {1} + {2} = 2020, product is {3}", n1, n2, n3, product);

                            return product.ToString();
                        }
                    }
                }
            }

            return "Not found";
        }

        /// <summary>
        /// Looks through nums to find the difference between sum and n1,
        /// returns true and that number in out n2. False if not found.
        /// </summary>
        private bool FindMatch(List<int> nums, int sum, int n1, out int n2) {
            var target = sum - n1;

            if (nums.Contains(target)) {
                n2 = target;
                return true;
            }

            n2 = 0;
            return false;
        }
    }
}

