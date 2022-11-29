using System.Security.Cryptography;

namespace Advent.Year2015 {
    [Day(2015, 4)]
    public class Day04 : DayBase {
        const string PuzzleInput = "yzbqklnj";

        public Day04() {
            NeedsInput = false;
        }

        public override async Task<string> PartOne(string input) {
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

        public override async Task<string> PartTwo(string input) {
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
