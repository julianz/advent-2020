using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MoreLinq;

using Advent.Util;

namespace Advent.Year2021 {
    [Day(2021, 4)]
    public class Day04 : DayBase {
        public override string PartOne(string input) {
            var bingo = new SquidBingo(input);
            var gameWon = false;
            int draw = 0;

            while (!gameWon) {
                draw = bingo.DrawNextNumber();
                gameWon = bingo.CheckForAWin();
            }

            return (draw * bingo.WinningBoard.Score()).ToString();
        }

        public override string PartTwo(string input) {
            var bingo = new SquidBingo(input);
            var gameWon = false;
            int draw = 0;

            while (!gameWon) {
                draw = bingo.DrawNextNumber();
                bingo.CheckForAWin();
                gameWon = bingo.Boards.All(b => b.HasWon);
            }

            return (draw * bingo.WinningBoard.Score()).ToString();
        }
    }

    internal class SquidBingo {
        private List<int> Draw { get; set; } = new();

        public List<BingoBoard> Boards { get; private set; } = new();

        public BingoBoard WinningBoard { get; private set; }

        public SquidBingo(string input) {
            var lines = input.SplitOnBlankLines();

            // Read the list of numbers to draw
            Draw.AddRange(lines.First().SplitBySeparator(",").Select(n => Convert.ToInt32(n)));

            // Read the list of boards
            foreach (var line in lines.Skip(1)) {
                Boards.Add(
                    new BingoBoard(
                        line.SplitOnWhitespace().Select(n => Convert.ToInt32(n))));
            }
        }

        public int DrawNextNumber() {
            drawIndex++;
            var draw = Draw[drawIndex];

            foreach (var board in Boards) {
                board.MarkNumberOff(draw);
            }

            return draw;
        }

        public bool CheckForAWin() {
            var result = false;
            foreach (var board in Boards.Where(b => !b.HasWon)) {
                if (board.CheckWin()) {
                    WriteLine($"Board {Boards.IndexOf(board)} has won");
                    WinningBoard = board;
                    result = true;
                }
            }
            return result;
        }

        private int drawIndex = -1;
    }

    internal class BingoBoard {
        private const int BoardSize = 5;

        private List<int> Numbers { get; set; } = new();
        private List<bool> Drawn { get; set; } = new();
        public bool HasWon { get; private set; }

        public BingoBoard(IEnumerable<int> numbers) {
            Numbers.AddRange(numbers);
            Drawn.AddRange(Enumerable.Repeat(false, Numbers.Count));
        }

        public void MarkNumberOff(int number) {
            var pos = Numbers.IndexOf(number);
            if (pos > -1) {
                Drawn[pos] = true;
            }
        }

        public bool CheckWin() {
            HasWon = (CheckRows() || CheckCols());
            return HasWon;
        }

        // Sum the numbers on the board that were not drawn
        public int Score() {
            return Numbers.Zip(Drawn, (n, d) => d ? 0 : n).Sum();
        }

        internal bool CheckRows() {
            foreach (var row in Drawn.Chunk(BoardSize)) {
                if (row.All(r => r)) {
                    return true;
                }
            }
            return false;
        }

        internal bool CheckCols() {
            for (int n = 0; n < BoardSize; n++) {
                var col = Drawn.Skip(n).TakeEvery(BoardSize);
                if (col.All(c => c)) {
                    return true;
                }
            }
            return false;
        }
    }
}
