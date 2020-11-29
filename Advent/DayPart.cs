using System;

namespace Advent {
    /// <summary>
    /// Enum for the parts of each day's puzzle
    /// </summary>
    public enum DayPart {
        PartOne,
        PartTwo
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
    }
}

