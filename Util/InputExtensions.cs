namespace Advent.Util {
    /// <summary>
    /// Extension methods to help with formatting puzzle input into different forms
    /// </summary>
    public static class InputExtensions {
        public static IEnumerable<string> AsLines(this string input) {
            return input.Trim().Split(new string[] { "\r\n", "\n" }, 
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static IEnumerable<string> AsLines(this string input, bool trim) {
            if (trim) { return input.AsLines(); }

            return input.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SplitOnBlankLines(this string input) {
            return input.Replace("\r\n", "\n")
                        .Split("\n\n", 
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static IEnumerable<string> SplitOnBlankLines(this string input, bool trim) {
            if (trim) { return input.SplitOnBlankLines(); }

            return input.Replace("\r\n", "\n")
                        .Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Split a string by a separator, default is ",".
        /// </summary>
        public static string[] SplitBySeparator(this string input, string separator = ",") {
            return input.Split(separator,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        /// <summary>
        /// Split a string at any whitespace character, including line breaks.
        /// </summary>
        public static IEnumerable<string> SplitOnWhitespace(this string input) {
            return input.Split(new char[] { ' ', '\t', '\r', '\n'},
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static IEnumerable<int> AsInts(this string input) {
            return input.AsLines().Select(Int32.Parse);
        }

        public static IEnumerable<long> AsLongs(this string input) {
            return input.AsLines().Select(Int64.Parse);
        }

        private static char[] Brackets = new char[] { '(', ')', '[', ']' };

        /// <summary>
        /// Convert a string in the form "3,4" or "(3,4)" into a coordinate tuple
        /// with integer values named X and Y.
        /// </summary>
        public static Coords AsCoords(this string input) {
            input = input.Trim(Brackets);
            var values = input.Split(",", count: 2);
            return new Coords { X = Convert.ToInt32(values[0]), Y = Convert.ToInt32(values[1]) };
        }
    }
}
