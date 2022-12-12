namespace Advent.Year2022 {
    [Day(2022, 2)]
    public class Day02 : DayBase {
        public override async Task<string> PartOne(string input) {
            //input = @"A Y
            //          B X
            //          C Z";

            return input.AsLines()
                .Select(ScorePart1)
                .Sum()
                .ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = @"A Y
            //          B X
            //          C Z";

            return input.AsLines()
                .Select(ScorePart2)
                .Sum()
                .ToString();
        }

        const int Rock = 1;
        const int Paper = 2;
        const int Scissors = 3;

        const int Loss = 0;
        const int Draw = 3;
        const int Win = 6;

        static int ScorePart1(string play) {
            // A and X are Rock which scores 1
            // B and Y are Paper which scores 2
            // C and Z are Scissors which scores 3
            // 0 for a loss, 3 for a draw, 6 for a win

            return play switch
            {
                "A X" => Rock + Draw,
                "A Y" => Paper + Win,
                "A Z" => Scissors + Loss,

                "B X" => Rock + Loss,
                "B Y" => Paper + Draw,
                "B Z" => Scissors + Win,

                "C X" => Rock + Win,
                "C Y" => Paper + Loss,
                "C Z" => Scissors + Draw,

                _ => throw new ArgumentOutOfRangeException($"Invalid play '{play}'")
            };
        }

        static int ScorePart2(string play) {
            // A is Rock, B Paper, C Scissors
            // X means lose, Y means draw, Z means win
            // 0 for a loss, 3 for a draw, 6 for a win

            return play switch
            {
                "A X" => Scissors + Loss,
                "A Y" => Rock + Draw,
                "A Z" => Paper + Win,

                "B X" => Rock + Loss,
                "B Y" => Paper + Draw,
                "B Z" => Scissors + Win,

                "C X" => Paper + Loss,
                "C Y" => Scissors + Draw,
                "C Z" => Rock + Win,

                _ => throw new ArgumentOutOfRangeException($"Invalid play '{play}'")
            };
        }
    }
}
