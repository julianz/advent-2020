using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 4)]
    public class Day04 : DayBase {
        public override string PartOne(string input) {
//            input = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
//byr:1937 iyr:2017 cid:147 hgt:183cm

//iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
//hcl:#cfa07d byr:1929

//hcl:#ae17e1 iyr:2013
//eyr:2024
//ecl:brn pid:760753108 byr:1931
//hgt:179cm

//hcl:#cfa07d eyr:2025 pid:166559648
//iyr:2011 ecl:brn hgt:59in
//";

            input = input.Replace("\r\n", "\n");
            var passports = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var valid = 0;
            foreach (var p in passports) {
                var passport = new Passport(p);
                if (passport.IsValidForPartOne()) {
                    valid++;
                }
            }
            return valid.ToString();
        }

        public override string PartTwo(string input) {
            input = @"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007

pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719
";

            var passports = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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

            static readonly Regex HeightPattern = new Regex(@"^(?<height>\d+)(?<units>(in|cm]))$");
            static readonly Regex HairColourPattern = new Regex(@"^#[0-9A-F]{6}$");
            static readonly Regex PassportIdPattern = new Regex(@"^[0-9]{9}$");

            readonly Dictionary<string, string> fields;

            public Passport(string input) {
                fields = new Dictionary<string, string>(ValidKeys.Count);

                var fieldData = input.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var f in fieldData) {
                    Console.WriteLine(f);
                    var bits = f.Split(':', StringSplitOptions.TrimEntries);
                    if (ValidKeys.Contains(bits[0])) {
                        fields[bits[0]] = bits[1];
                    } else {
                        Console.WriteLine($"Discarding invalid field {f}");
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

                Console.WriteLine("Not valid for part 1");
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
                Console.WriteLine($"{input} - {match.Success}");

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
