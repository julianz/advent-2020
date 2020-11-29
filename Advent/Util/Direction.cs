using System;

namespace Advent.Util {
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions {
        public static Direction TurnLeft(this Direction direction) {
            return direction switch
            {
                Direction.Up => Direction.Left,
                Direction.Left => Direction.Down,
                Direction.Down => Direction.Right,
                Direction.Right => Direction.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };
        }

        public static Direction TurnRight(this Direction direction) {
            return direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Left => Direction.Up,
                Direction.Down => Direction.Left,
                Direction.Right => Direction.Down,
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };
        }
    }
}
