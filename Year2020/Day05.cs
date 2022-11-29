namespace Advent.Year2020 {
    [Day(2020, 5)]
    public class Day05 : DayBase {
        public override async Task<string> PartOne(string input) {
            var passes = input.AsLines();

            var highest = GetPassIds(passes).Max();

            return highest.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var passes = input.AsLines();

            var passids = GetPassIds(passes).ToList();
            passids.Sort();

            for (var i = 1; i < passids.Count; i++) {
                if (passids[i] - passids[i - 1] == 2) {
                    return (passids[i] - 1).ToString();
                }
            }

            return "Not found";
        }

        IEnumerable<int> GetPassIds(IEnumerable<string> passes) {
            return passes.Select(p => {
                var rowb = ToBinaryDigits(p.Substring(0, 7), zero: 'F', one: 'B');
                var seatb = ToBinaryDigits(p.Substring(7, 3), zero: 'L', one: 'R');

                var row = Convert.ToInt32(rowb, 2);
                var seat = Convert.ToInt32(seatb, 2);
                
                return row * 8 + seat;
            });
        }

        string ToBinaryDigits(string input, char zero, char one) {
            return String.Join("",
                input.Select(c => {
                    if (c == one)
                        return '1';
                    else if (c == zero)
                        return '0';
                    else
                        throw new ArgumentException();
                }));
        }
    }
}
