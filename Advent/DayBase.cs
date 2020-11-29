using System;
using System.Collections.Generic;
using System.Text;

namespace Advent {
    public abstract class DayBase {
        public bool NeedsInput { get; protected set; } = true;

        public abstract string PartOne(string input);

        public abstract string PartTwo(string input);
    }
}
