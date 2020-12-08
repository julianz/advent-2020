using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 8)]
    public class Day08 : DayBase {

        public override string PartOne(string input) {
            //input = @"nop +0
            //        acc +1
            //        jmp +4
            //        acc +3
            //        jmp -3
            //        acc -99
            //        acc +1
            //        jmp -4
            //        acc +6";

            var program = input.AsLines().ToList();
            var accumulator = 0;
            var iptr = 0;
            var visited = new HashSet<int>();

            while(true) {
                visited.Add(iptr);
                var instr = new Instruction(program[iptr]);

                Out.Print($"iptr: {iptr}  acc: {accumulator}  instr: {instr}");

                switch (instr.Operation) {
                    case Opcode.ACC:
                        accumulator += instr.Argument;
                        iptr += 1;
                        break;
                    case Opcode.JMP:
                        iptr += instr.Argument;
                        break;
                    case Opcode.NOP:
                        iptr += 1;
                        break;
                }

                if (visited.Contains(iptr)) {
                    Out.Print($"About to repeat instruction {iptr}, acc is {accumulator}");
                    break;
                }
            }

            return accumulator.ToString();
        }

        public override string PartTwo(string input) {
            input = @"nop +0
                    acc +1
                    jmp +4
                    acc +3
                    jmp -3
                    acc -99
                    acc +1
                    nop -4
                    acc +6";

            var program = input.AsLines().ToList();
            var accumulator = 0;
            var iptr = 0;
            var visited = new HashSet<int>();

            while (true) {
                visited.Add(iptr);
                var instr = new Instruction(program[iptr]);

                Out.Print($"iptr: {iptr}  acc: {accumulator}  instr: {instr}");

                switch (instr.Operation) {
                    case Opcode.ACC:
                        accumulator += instr.Argument;
                        iptr += 1;
                        break;
                    case Opcode.JMP:
                        iptr += instr.Argument;
                        break;
                    case Opcode.NOP:
                        iptr += 1;
                        break;
                }

                if (iptr == program.Count) {
                    Out.Print($"About to jump past the end of program, acc is {accumulator}");
                    break;
                }
            }

            return accumulator.ToString();
        }
    }

    public enum Opcode {
        NOP,
        ACC,
        JMP
    }

    class Instruction {

        public Opcode Operation { get; private set; }

        public int Argument { get; private set; }

        public Instruction(string text) {
            var bits = text.Split(" ", StringSplitOptions.TrimEntries);

            Operation = bits[0] switch
            {
                "nop" => Opcode.NOP,
                "acc" => Opcode.ACC,
                "jmp" => Opcode.JMP,
                _ => throw new InvalidOperationException($"No such opcode {bits[0]}")
            };

            Argument = Int32.Parse(bits[1]);
        }

        public override string ToString() => $"Instruction ({Operation}, {Argument})";
    }
}
