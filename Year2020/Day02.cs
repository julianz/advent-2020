namespace Advent.Year2020 {
    [Day(2020, 2)]
    public class Day02 : DayBase {
        public override async Task<string> PartOne(string input) {
            var valid = 0;

            foreach (var line in input.AsLines()) {
                var policy = GetPasswordPolicy(line);

                var count = policy.Password.Count(c => c == policy.Letter);
                if (count >= policy.Min && count <= policy.Max) {
                    valid++;
                }
            }
            
            return valid.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var valid = 0;

            foreach (var line in input.AsLines()) {
                var policy = GetPasswordPolicy(line);

                // Exclusive OR
                if (policy.Password[policy.Min - 1] == policy.Letter 
                    ^ policy.Password[policy.Max - 1] == policy.Letter) {

                    valid++;
                }
            }

            return valid.ToString();
        }

        Policy GetPasswordPolicy(string line) {
            var bits = line.Split(':', StringSplitOptions.TrimEntries);
            var policyBits = bits[0].Split(' ');
            var minmax = policyBits[0].Split('-');
            var min = Int32.Parse(minmax[0]);
            var max = Int32.Parse(minmax[1]);
            char ch = policyBits[1][0];
            var password = bits[1];

            return new Policy
            {
                Min = min,
                Max = max,
                Letter = ch,
                Password = password
            };
        }

        class Policy {
            public int Min { get; set; }
            public int Max { get; set; }
            public char Letter { get; set; }
            public string Password { get; set; }

            public override string ToString() {
                return $"{Min}-{Max} {Letter}: {Password}";
            }
        }
    }
}
