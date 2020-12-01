using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days {
    [Day(2020, 1)]
    public class Day01 : DayBase {

        public Day01() {
            NeedsInput = true;
        }

        public override string PartOne(string input) {
            var nums = input.Split('\n').Select(n => Int32.Parse(n)).ToList();
            foreach(var n1 in nums) {
                var n2 = 2020 - n1;
                if (nums.Contains(n2)) {
                    var product = n1 * n2;
                    Console.WriteLine($"Found: {n1} * {n2} = {product}");
                    return product.ToString();
                }
            }

            return "Not found";
        }

        public override string PartTwo(string input) {
            var product = 0;
            var nums = input.Split('\n').Select(n => Int32.Parse(n)).ToList();

            foreach (var n1 in nums) {
                foreach (var n2 in nums) {
                    foreach (var n3 in nums) {
                        if (n1 + n2 + n3 == 2020) {
                            product = n1 * n2 * n3;
                            Console.WriteLine("{0} + {1} + {2} = 2020, product is {3}", n1, n2, n3, product);
                        }
                    }
                }
            }

            return product.ToString();
        }
    }
}

