using System;
using System.Collections.Generic;

namespace Advent.Util {
    public static class Out {
        public static void Print(string message) {
            Console.WriteLine(message);
        }

        public static void Print(int value) {
            Console.WriteLine(value);
        }

        public static void Print(bool value) {
            Console.WriteLine(value);
        }

        public static void NewLine(int count = 1) {
            for (var n = 0; n < count; n++) {
                Console.WriteLine();
            }
        }

        public static void PrintList(IEnumerable<int> items) {
            foreach (var o in items) {
                Console.WriteLine(o.ToString());
            }
        }

        public static void PrintList(IEnumerable<object> items) {
            foreach (var o in items) {
                Console.WriteLine(o.ToString());
            }
        }
    }
}
