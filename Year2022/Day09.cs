namespace Advent.Year2022 {
    [Day(2022, 9)]
    public class Day09 : DayBase {
        public override async Task<string> PartOne(string input) {
            //input = @"R 4
            //          U 4
            //          L 3
            //          D 1
            //          R 4
            //          D 1
            //          L 5
            //          R 2";

            var head = new Coords(0, 0);
            var tail = head;
            var visited = new HashSet<Coords> { tail };

            foreach (var line in input.AsLines()) {
                var distance = int.Parse(line[2..].ToString());

                var delta = line[0] switch
                {
                    Right => new Coords(1, 0),
                    Left => new Coords(-1, 0),
                    Up => new Coords(0, 1),
                    Down => new Coords(0, -1),

                    _ => throw new ArgumentOutOfRangeException("Mystery direction")
                };

                for (var i = 0; i < distance; i++) {
                    head += delta;
                    tail = tail.Follow(head);
                    visited.Add(tail);
                }
            }

            return visited.Count.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = @"R 5
            //          U 8
            //          L 8
            //          D 3
            //          R 17
            //          D 10
            //          L 25
            //          U 20";

            var rope = Enumerable.Range(1, 10).Select(i => new Coords(0, 0)).ToList();
            var head = 0;
            var tail = 9;
            var visited = new HashSet<Coords> { rope[tail] };

            foreach (var line in input.AsLines()) {
                var distance = int.Parse(line[2..].ToString());

                var delta = line[0] switch
                {
                    Right => new Coords(1, 0),
                    Left => new Coords(-1, 0),
                    Up => new Coords(0, 1),
                    Down => new Coords(0, -1),

                    _ => throw new ArgumentOutOfRangeException("Mystery direction")
                };

                for (var i = 0; i < distance; i++) {
                    // Move the head
                    rope[head] += delta;

                    // Move the rest of the rope
                    for (var n = 1; n <= tail; n++) {
                        rope[n] = rope[n].Follow(rope[n - 1]);
                    }

                    visited.Add(rope[tail]);
                }
            }

            return visited.Count.ToString();
        }

        const char Right = 'R';
        const char Left = 'L';
        const char Up = 'U';
        const char Down = 'D';
    }

    public static class CoordsExtensions {
        public static Coords Follow(this Coords tail, Coords head) {
            var xdelta = head.X - tail.X;
            var ydelta = head.Y - tail.Y;

            // Horizontal and vertical movement

            if (xdelta == 0) {
                if (Math.Abs(ydelta) > 1) {
                    return new Coords(tail.X, tail.Y + (ydelta / Math.Abs(ydelta)));
                }
                else {
                    return tail;
                }
            }

            if (ydelta == 0) {
                if (Math.Abs(xdelta) > 1) {
                    return new Coords(tail.X + (xdelta / Math.Abs(xdelta)), tail.Y);
                }
                else {
                    return tail;
                }
            }

            // 45 degree diagonal, no tail move
            if (Math.Abs(xdelta) == 1 && Math.Abs(ydelta) == 1) {
                return tail;
            }

            // Move the tail in a diagonal direction
            return new Coords(tail.X + (xdelta / Math.Abs(xdelta)), tail.Y + (ydelta / Math.Abs(ydelta)));
        }
    }
}
