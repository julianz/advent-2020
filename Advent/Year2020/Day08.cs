namespace Advent.Year2020 {
    [Day(2020, 8)]
    public class Day08 : DayBase {

        public override async Task<string> PartOne(string input) {
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

                WriteLine($"iptr: {iptr}  acc: {accumulator}  instr: {instr}");

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
                    WriteLine($"About to repeat instruction {iptr}, acc is {accumulator}");
                    break;
                }
            }

            return accumulator.ToString();
        }

        public override async Task<string> PartTwo(string input) {
            var program = input.AsLines().ToList();

            var vm = new VirtualMachine(program);
            var result = vm.Run();
            WriteLine(result);
            
            return  vm.Accumulator.ToString();
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

    class VirtualMachine {
        int _accumulator = 0;
        int _iptr = 0;
        int _targetLocation = 0;
        HashSet<int> _visited = new();
        List<string> _program;

        public int Accumulator => _accumulator;

        public VirtualMachine(List<string> program) {
            _program = program;
            _targetLocation = program.Count;
        }

        /// <summary>
        /// Runs the program. Returns true if we jump to mem + 1, false if loop detected.
        /// </summary>
        public bool Run() {
            Reset();
            var hitTargetLocation = false;

            while (true) {
                _visited.Add(_iptr);
                var instr = new Instruction(_program[_iptr]);

                WriteLine($"iptr: {_iptr}  acc: {_accumulator}  instr: {instr}");

                switch (instr.Operation) {
                    case Opcode.ACC:
                        _accumulator += instr.Argument;
                        _iptr += 1;
                        break;
                    case Opcode.JMP:
                        _iptr += instr.Argument;
                        break;
                    case Opcode.NOP:
                        _iptr += 1;
                        break;
                }

                if (_iptr == _targetLocation) {
                    WriteLine($"About to jump past the end of program, acc is {_accumulator}");
                    hitTargetLocation = true;
                    break;
                } else if (_visited.Contains(_iptr)) {
                    WriteLine($"Loop detected: about to repeat instruction {_iptr}");
                    break;
                }
            }

            return hitTargetLocation;
        }

        private void Reset() {
            _accumulator = 0;
            _iptr = 0;
            _visited.Clear();
        }
    }
}
