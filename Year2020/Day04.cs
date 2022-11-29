namespace Advent.Year2020 {
    [Day(2020, 4)]
    public class Day04 : DayBase {
        public override async Task<string> PartOne(string input) {
            var passports = input.SplitOnBlankLines();

            var valid = 0;
            foreach (var p in passports) {
                var passport = new Passport(p);
                if (passport.IsValidForPartOne()) {
                    valid++;
                }
            }

            return valid.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var passports = input.SplitOnBlankLines();

            var valid = 0;
            foreach (var p in passports) {
                var passport = new Passport(p);
                if (passport.IsValidForPartTwo()) {
                    valid++;
                }
            }

            return valid.ToString();
        }

        class Passport {
            static readonly List<string> ValidKeys = 
                new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
            static readonly List<string> ValidEyeColours =
                new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            static readonly Regex HeightPattern = new Regex(@"^(?<height>\d+)(?<units>(in|cm))$");
            static readonly Regex HairColourPattern = new Regex(@"^\#[0-9A-F]{6}$", RegexOptions.IgnoreCase);
            static readonly Regex PassportIdPattern = new Regex(@"^[0-9]{9}$");

            readonly Dictionary<string, string> fields;

            public Passport(string input) {
                fields = new Dictionary<string, string>(ValidKeys.Count);

                var fieldData = input.Split(new string[] { "\r\n", "\n", " " }, 
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var f in fieldData) {
                    //Out.Log(f);
                    var bits = f.Split(':', StringSplitOptions.TrimEntries);
                    if (ValidKeys.Contains(bits[0])) {
                        fields[bits[0]] = bits[1];
                    } else {
                        WriteLine($"Discarding invalid field {f}");
                    }
                }
            }

            public bool IsValidForPartOne() {
                if (fields.Keys.Count == ValidKeys.Count) {
                    // all fields are present
                    return true;
                }

                if (fields.Keys.Count == ValidKeys.Count - 1 &&
                    !fields.Keys.Contains("cid")) {
                    // everything but the cid field
                    return true;
                }

                return false;
            }

            public bool IsValidForPartTwo() {
                return IsValidForPartOne()
                       && ValidateInt(fields["byr"], 1920, 2002)
                       && ValidateInt(fields["iyr"], 2010, 2020)
                       && ValidateInt(fields["eyr"], 2020, 2030)
                       && ValidateHeight(fields["hgt"])
                       && ValidateHairColour(fields["hcl"])
                       && ValidateEyeColour(fields["ecl"])
                       && ValidatePassportId(fields["pid"]);
            }

            private bool ValidatePassportId(string input) {
                return PassportIdPattern.IsMatch(input);
            }

            private bool ValidateEyeColour(string input) {
                return ValidEyeColours.Contains(input);
            }

            private bool ValidateHairColour(string input) {
                return HairColourPattern.IsMatch(input);
            }

            private bool ValidateHeight(string input) {
                var match = HeightPattern.Match(input);

                if (match.Success) {
                    var height = Int32.Parse(match.Groups["height"].Value);
                    var units = match.Groups["units"].Value;

                    return units switch {
                        "cm" => (height >= 150 && height <= 193),
                        "in" => (height >= 59 && height <= 76),
                        _ => false
                    };
                }
                return false;
            }

            bool ValidateInt(string input, int min, int max) {
                if (Int32.TryParse(input, out int val)) {
                    if (min <= val && val <= max)
                        return true;
                    Console.WriteLine($"{input} is not between {min} and {max}");
                }
                return false;
            }
        }
    }
}
