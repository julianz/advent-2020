using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 12)]
    public class Day12 : DayBase {
        const char North = 'N';
        const char South = 'S';
        const char East = 'E';
        const char West = 'W';
        const char Forward = 'F';
        const char Left = 'L';
        const char Right = 'R';

        public override string PartOne(string input) {
            var instructions = input.AsLines();

            var coords = (x: 0, y: 0);
            var direction = Direction.Right;

            foreach (var inst in instructions) {
                var dir = inst[0];
                var amount = Int32.Parse(inst.Substring(1));

                switch (dir) {
                    case North:
                        coords.y += amount;
                        break;
                    case South:
                        coords.y -= amount;
                        break;
                    case East:
                        coords.x += amount;
                        break;
                    case West:
                        coords.x -= amount;
                        break;
                    case Left:
                        direction = direction.TurnLeft(amount / 90);
                        break;
                    case Right:
                        direction = direction.TurnRight(amount / 90);
                        break;
                    case Forward:
                        switch (direction) {
                            case Direction.Up:
                                coords.y += amount;
                                break;
                            case Direction.Down:
                                coords.y -= amount;
                                break;
                            case Direction.Right:
                                coords.x += amount;
                                break;
                            case Direction.Left:
                                coords.x -= amount;
                                break;
                        }
                        break;
                }
            }

            var manhattan = Math.Abs(coords.x) + Math.Abs(coords.y);
            return manhattan.ToString();
        }

        public override string PartTwo(string input) {
            var instructions = input.AsLines();

            var coords = (x: 0, y: 0);
            var waypoint = (x: 10, y: 1);

            foreach (var inst in instructions) {
                var dir = inst[0];
                var amount = Int32.Parse(inst.Substring(1));
                int times;

                switch (dir) {
                    case North:
                        waypoint.y += amount;
                        break;
                    case South:
                        waypoint.y -= amount;
                        break;
                    case East:
                        waypoint.x += amount;
                        break;
                    case West:
                        waypoint.x -= amount;
                        break;
                    case Left:
                        times = amount / 90;
                        for (var t = 0; t < times; t++) {
                            var newx = -waypoint.y;
                            var newy = waypoint.x;
                            waypoint.x = newx;
                            waypoint.y = newy;
                        }
                        break;
                    case Right:
                        times = amount / 90;
                        for (var t = 0; t < times; t++) {
                            var newx = waypoint.y;
                            var newy = -waypoint.x;
                            waypoint.x = newx;
                            waypoint.y = newy;
                        }
                        break;
                    case Forward:
                        coords.x += (waypoint.x * amount);
                        coords.y += (waypoint.y * amount);
                        break;
                }
            }

            var manhattan = Math.Abs(coords.x) + Math.Abs(coords.y);
            return manhattan.ToString();
        }
    }
}
