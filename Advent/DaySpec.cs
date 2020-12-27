using System;

namespace Advent {
    /// <summary>
    /// Enum for the parts of each day's puzzle
    /// </summary>
    public enum DayPart {
        None,
        PartOne,
        PartTwo
    }

    public class DaySpec {
        public int Year { get; set; }
        public int Day { get; set; } = 0;
        public DayPart Part { get; set; }

        public bool IsSet => (Day >= 1 && Day <= 25);

        public override string ToString() =>
            $"{Year} day {Day}" + 
            ((Part != DayPart.None) ? " part {Part.ToPartNumber()}" : String.Empty);
    }

    public static class DayPartExtensions {
        public static DayPart ToDayPart(this string dayPartLetter) {
            return dayPartLetter.ToLower() switch
            {
                "a" => DayPart.PartOne,
                "b" => DayPart.PartTwo,
                _ => throw new ArgumentOutOfRangeException(nameof(dayPartLetter))
            };
        }

        public static int ToPartNumber(this DayPart part) =>
            part switch
            {
                DayPart.PartOne => 1,
                DayPart.PartTwo => 2,
                _ => 0
            };
    }
}

