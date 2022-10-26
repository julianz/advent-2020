using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2015 {
    [Day(2015, 4)]
    public class Day04 : DayBase {
        const string PuzzleInput = "yzbqklnj";

        public Day04() {
            NeedsInput = false;
        }

        public override string PartOne(string input) {
            var num = 0;
            var found = false;

            using (var md5 = MD5.Create()) {
                while (!found) {
                    var textBytes = Encoding.ASCII.GetBytes(PuzzleInput + num.ToString());
                    var hashBytes = md5.ComputeHash(textBytes);
                    var hash = BitConverter.ToString(hashBytes).Replace("-", "");

                    if (hash.StartsWith("00000")) {
                        found = true;
                        WriteLine(hash);
                        break;
                    }

                    num++;
                }
            }

            return num.ToString();
        }

        public override string PartTwo(string input) {
            var num = 282749; // part 1 answer, it can't be less than that
            var found = false;

            using (var md5 = MD5.Create()) {
                while (!found) {
                    var textBytes = Encoding.ASCII.GetBytes(PuzzleInput + num.ToString());
                    var hashBytes = md5.ComputeHash(textBytes);
                    var hash = BitConverter.ToString(hashBytes).Replace("-", "");

                    if (hash.StartsWith("000000")) {
                        found = true;
                        WriteLine(hash);
                        break;
                    }

                    num++;
                }
            }

            return num.ToString();
        }
    }
}
