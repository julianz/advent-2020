namespace Advent.Util {
    /// <summary>
    /// Extension methods for working with strings.
    /// </summary>
    public static class StringExtensions {
        /// <summary>
        /// Return the input string, sorted in character order
        /// </summary>
        public static string SortChars(this string input) {
            char[] chars = input.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
}
