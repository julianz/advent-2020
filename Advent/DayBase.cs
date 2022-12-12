namespace Advent {
    public abstract class DayBase {
        public bool NeedsInput { get; protected set; } = true;

        public abstract Task<string> PartOne(string input);

        public abstract Task<string> PartTwo(string input);
    }
}
