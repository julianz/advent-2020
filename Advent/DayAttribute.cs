using System;

namespace Advent {
    public class DayAttribute : Attribute {

        public int Year { get; private set; }

        public int Day { get; private set; }

        public DayAttribute(int year, int day) {
            Year = year;
            Day = day;
        }

        public override string ToString() {
            return $"Year {Year}, Day {Day}";
        }
    }
}
