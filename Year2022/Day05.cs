using static MoreLinq.Extensions.ForEachExtension;

namespace Advent.Year2022 {
    [Day(2022, 5)]
    public class Day05 : DayBase {
        public override async Task<string> PartOne(string input) {
            /**
             * Don't trim input because whitespace is important in the top section
             * Split up the input at the blank line
             * Parse the line above that to get the number of stacks, create that many stacks
             * Work backwards from there to the top and push letters onto the stacks
             **/
            var inputSections = input.SplitOnBlankLines(trim: false);
            var setupSection = inputSections.First().AsLines(trim: false).Reverse();

            var stackCountLine = setupSection.First().Trim();
            var stackCount = Int32.Parse(stackCountLine.AsSpan(stackCountLine.LastIndexOf(' ')));
            var stacks = Enumerable.Range(1, stackCount).Select(n => new Stack<char>()).ToArray();

            // Fill the stacks with their initial state
            foreach (var line in setupSection.Skip(1)) {
                for (var stackIndex = 0; stackIndex < stackCount; stackIndex++) {
                    var pos = (stackIndex * 4) + 1; // positional parsing
                    if (line.Length >= pos && line[pos] != Blank) {
                        stacks[stackIndex].Push(line[pos]);
                    }
                }
            }

            // Perform the move instructions
            foreach (var line in inputSections.Last().AsLines()) {
                var bits = line.SplitOnWhitespace().ToList();
                var howMany = Int32.Parse(bits[1]);
                var from = Int32.Parse(bits[3]) - 1; // switch to zero-indexing
                var to = Int32.Parse(bits[5]) - 1;

                for (var move = 0; move < howMany; move++) {
                    stacks[to].Push(stacks[from].Pop());
                }
            }

            // Pop the stacks to get the answer
            return string.Join("", stacks.Select(st => st.Pop()));
        }

        public override async Task<string> PartTwo(string input) {

            /**
             * Don't trim input because whitespace is important in the top section
             * Split up the input at the blank line
             * Parse the line above that to get the number of stacks, create that many stacks
             * Work backwards from there to the top and push letters onto the stacks
             **/
            var inputSections = input.SplitOnBlankLines(trim: false);
            var setupSection = inputSections.First().AsLines(trim: false).Reverse();

            var stackCountLine = setupSection.First().Trim();
            var stackCount = Int32.Parse(stackCountLine.AsSpan(stackCountLine.LastIndexOf(' ')));
            var stacks = Enumerable.Range(1, stackCount).Select(n => new Stack<char>()).ToArray();

            // Fill the stacks with their initial state
            foreach (var line in setupSection.Skip(1)) {
                for (var stackIndex = 0; stackIndex < stackCount; stackIndex++) {
                    var pos = (stackIndex * 4) + 1; // positional parsing
                    if (line.Length >= pos && line[pos] != Blank) {
                        stacks[stackIndex].Push(line[pos]);
                    }
                }
            }

            // Perform the move instructions
            foreach (var line in inputSections.Last().AsLines()) {
                var bits = line.SplitOnWhitespace().ToList();
                var howMany = Int32.Parse(bits[1]);
                var from = Int32.Parse(bits[3]) - 1; // switch to zero-indexing
                var to = Int32.Parse(bits[5]) - 1;

                // Pop the crates off the "from" stack
                var crates = Enumerable.Range(1, howMany).Select(n => stacks[from].Pop());
                // ...and onto the "to" stack in reverse order
                crates.Reverse().ForEach(c => stacks[to].Push(c));
            }

            // Pop the stacks to get the answer
            return string.Join("", stacks.Select(st => st.Pop()));
        }

        const char Blank = ' ';
    }
}
