﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Util {
    /// <summary>
    /// Extension methods to help with formatting puzzle input into different forms
    /// </summary>
    public static class InputExtensions {
        public static IEnumerable<string> AsLines(this string input) {
            return input.Trim().Split(Environment.NewLine, StringSplitOptions.None);
        }

        public static IEnumerable<int> AsInts(this string input) {
            return input.AsLines().Select(n => Int32.Parse(n));
        }

        public static IEnumerable<long> AsLongs(this string input) {
            return input.AsLines().Select(n => Int64.Parse(n));
        }
    }
}
