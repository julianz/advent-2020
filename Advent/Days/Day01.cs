using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days {
    [Day(2020, 1)]
    public class Day01 : DayBase {
        const char Up = '(';
        const char Down = ')';

        public Day01() {
            NeedsInput = true;
        }

        public override string PartOne(string input) {
            int ups = input.Where(c => c == Up).Count();
            int downs = input.Where(c => c == Down).Count();

            return (ups - downs).ToString();
        }

        public override string PartTwo(string input) {
            throw new NotImplementedException();
        }
    }
}
