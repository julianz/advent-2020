using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 14)]
    public class Day14 : DayBase {
        public override string PartOne(string input) {
            var program = input.AsLines();
            var memory = new Dictionary<long, long>();
            var mask = "";

            long onesMask = 0;
            long zeroesMask = 0;

            foreach (var line in program) {
                var pieces = line.SplitBySeparator("=").ToList();
                if (pieces[0] == "mask") {
                    // reset mask, split it into ones and zeroes masks
                    mask = pieces[1];

                    var ones = String.Join("", mask.Select(c => (c == '1') ? '1' : '0'));
                    onesMask = Convert.ToInt64(ones, 2);

                    var zeroes = String.Join("", mask.Select(c => (c == '0') ? '0' : '1'));
                    zeroesMask = Convert.ToInt64(zeroes, 2);
                } else {
                    // set memory
                    var addr = Int64.Parse(pieces[0].Substring(4, pieces[0].Length - 5));
                    var value = Int64.Parse(pieces[1]);

                    // OR the value with the ones mask, then AND it with the zeroes mask
                    value |= onesMask;
                    value &= zeroesMask;

                    //Out.Print($"{addr} - {value}");
                    memory[addr] = value;
                }
            }

            return memory.Values.Sum().ToString();
        }

        public override string PartTwo(string input) {
            input = @"mask = 000000000000000000000000000000X1001X
                    mem[42] = 100
                    mask = 00000000000000000000000000000000X0XX
                    mem[26] = 1";

            throw new PuzzleNotSolvedException();
        }
    }
}
