using System;

namespace Advent {
    public class PuzzleNotSolvedException : Exception {
        public PuzzleNotSolvedException() {
        }

        public PuzzleNotSolvedException(string message)
            : base(message) {
        }

        public PuzzleNotSolvedException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
