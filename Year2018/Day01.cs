using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Advent.Util;

namespace Advent.Year2018 {
    [Day(2018, 1)]
    public class Day01 : DayBase {
        public override string PartOne(string input) {

            foreach (var reading in input.AsInts()) {

            }

            return "foo";
        }

        public override string PartTwo(string input) {
            throw new PuzzleNotSolvedException();
        }
    }
}
