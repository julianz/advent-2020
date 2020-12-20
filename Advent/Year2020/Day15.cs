using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 15)]
    public class Day15 : DayBase {
        public override string PartOne(string input) {
            //input = "0,3,6"; // expecting 0 3 6 0 3 3 1 0 4 0, 2020th is 436

            var numbers = input.SplitBySeparator(",").Select(Int32.Parse).ToList();
            var lastSpoken = PlaySpeakingGame(numbers, turns: 2020);

            return lastSpoken.ToString();
        }

        public override string PartTwo(string input) {
            var numbers = input.SplitBySeparator(",").Select(Int32.Parse).ToList();
            var lastSpoken = PlaySpeakingGame(numbers, turns: 30000000);

            return lastSpoken.ToString();
        }

        long PlaySpeakingGame(IList<int> startNumbers, long turns) {
            var spoken = new Dictionary<int, int>();

            for (var t = 0; t < startNumbers.Count - 1; t++) {
                spoken[startNumbers[t]] = t + 1; // 1-based turns.
            }

            var turn = startNumbers.Count;
            var lastSpoken = startNumbers.Last();

            while (turn < turns) {
                if (!spoken.ContainsKey(lastSpoken)) {
                    spoken[lastSpoken] = turn;
                    lastSpoken = 0;
                } else {
                    var turnsSinceLastSpoken = turn - spoken[lastSpoken];
                    spoken[lastSpoken] = turn;
                    lastSpoken = turnsSinceLastSpoken;
                }

                turn++;
                //Out.Print($"Turn {turn} spoke {lastSpoken}");
            }

            return lastSpoken;
        }
    }
}
