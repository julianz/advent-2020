namespace Advent.Year2020 {
    [Day(2020, 14)]
    public class Day14 : DayBase {
        public override async Task<string> PartOne(string input) {
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

                    //WriteLine($"{addr} - {value}");
                    memory[addr] = value;
                }
            }

            return memory.Values.Sum().ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = @"mask = 000000000000000000000000000000X1001X
            //        mem[42] = 100
            //        mask = 00000000000000000000000000000000X0XX
            //        mem[26] = 1";

            var program = input.AsLines();
            var memory = new Dictionary<long, long>();
            var mask = "";

            foreach (var line in program) {
                var pieces = line.SplitBySeparator("=").ToList();
                if (pieces[0] == "mask") {
                    // reset mask, split it into ones and zeroes masks
                    mask = pieces[1];
                    //WriteLine($"Setting mask to  {mask}");
                } else {
                    // set memory
                    var originalAddress = Int64.Parse(pieces[0].Substring(4, pieces[0].Length - 5));
                    var value = Int64.Parse(pieces[1]);

                    // MAGIC HAPPENS

                    // need to either transform the mask into every variant of itself as a binary
                    // and figure out the operation that leaves 0's alone and overwrites 1's

                    // OR

                    // convert the original address into a binary string and do the transformation
                    // in the string domain (slower but easier to debug)

                    var source = originalAddress.ToBinaryString();
                    //WriteLine($"Original address {source}");

                    // apply the mask to the address 
                    var masked = ApplyMaskToAddress(source, mask);
                    //WriteLine($"Masked           {masked}");

                    // produce the list of actual addresses that will have the value written to them.
                    var addresses = GetAddressesFromMasked(masked);

                    foreach (var addrString in addresses) {
                        var address = Convert.ToInt64(addrString, 2);
                        //WriteLine($"Setting {address} to {value}");
                        memory[address] = value;
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }

        private List<String> GetAddressesFromMasked(string masked) {
            var addresses = new List<String>();
            addresses.Add("");

            foreach (var c in masked) {
                if (c == '0' || c == '1') {
                    // Append the 0/1 to every address in the list.
                    for (var addr = 0; addr < addresses.Count; addr++) {
                        addresses[addr] += c;
                    }
                } else {
                    // An X means we have to double the list and.
                    var copy = new List<String>(addresses);
                    for (var addr = 0; addr < addresses.Count; addr++) {
                        addresses[addr] += '0';
                        copy[addr] += '1';
                    }
                    addresses.AddRange(copy);
                }
            }

            return addresses;
        }

        string ApplyMaskToAddress(string source, string mask) {
            if (source.Length != mask.Length) {
                throw new ArgumentException("Lengths don't match");
            }

            var result = new StringBuilder(source.Length);

            for (var c = 0; c < source.Length; c++) {
                result.Append((mask[c] == '0') ? source[c] : mask[c]);
            }

            return result.ToString();
        }
    }
}
