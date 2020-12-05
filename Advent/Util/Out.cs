using System;

namespace Advent.Util {
    public static class Out {
        public static void Log(string message) {
            Console.WriteLine(message);
        }

        public static void NewLine(int count = 1) {
            for (var n = 0; n < count; n++) {
                Console.WriteLine();
            }
        }
    }
}
