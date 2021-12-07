using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Util {
    /// <summary>
    /// Extension methods to help with formatting puzzle input into different forms
    /// </summary>
    public static class InputExtensions {
        public static IEnumerable<string> AsLines(this string input) {
            return input.Trim().Split(new string[] { "\r\n", "\n" }, 
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static IEnumerable<string> SplitOnBlankLines(this string input) {
            input = input.Replace("\r\n", "\n");
            return input.Split("\n\n",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        /// <summary>
        /// Split a string by a separator, default is ",".
        /// </summary>
        public static IEnumerable<string> SplitBySeparator(this string input, string separator = ",") {
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
    }
}
