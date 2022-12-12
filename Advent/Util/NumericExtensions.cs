namespace Advent.Util {
    /// <summary>
    /// Extension methods for working with numbers.
    /// </summary>
    public static class NumericExtensions {

        /// <summary>
        /// Convert a long int to a string of binary digits.
        /// Make sure the length is long enough for your number,
        /// there is no checking and you'll just get 1's if not.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="length">The length of the string</param>
        public static string ToBinaryString(this long value, int length = 36) {
            var position = length - 1;
            var result = new StringBuilder(length);

            while (position >= 0) {
                var bit = Convert.ToInt64(Math.Pow(2, position));
                if (value / bit == 0) {
                    result.Append('0');
                } else {
                    result.Append('1');
                    value -= bit;
                }
                position--;
            }

            return result.ToString();
        }

    }
}
