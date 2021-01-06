using System;
using System.Collections.Generic;

using Advent.Util;

namespace Advent.Year2020 {
    [Day(2020, 18)]
    public class Day18 : DayBase {
        const string Digits = "0123456789";
        const string Operators = "+*";

        public override string PartOne(string input) {
            var lines = input.AsLines();
            long total = 0;

            foreach (var line in lines) {
                var spaced = line.Replace("(", "( ");
                spaced = spaced.Replace(")", " )");
                var split = spaced.Split();
                var expr = new Stack<String>(split.Length); // to avoid capacity increases

                foreach (var token in split) {
                    if (Digits.Contains(token) || Operators.Contains(token) || token == "(") {
                        expr.Push(token);
                    } else if (token == ")") {
                        // Handle the sub-expression
                        var subexpr = new Stack<String>(split.Length);
                        
                        while (expr.Peek() != "(") {
                            subexpr.Push(expr.Pop());
                        }
                        expr.Pop(); // pop the '('

                        // This stack is already in reverse order
                        expr.Push(Simplify(subexpr));
                    }
                }

                // Reverse the stack so we eval in the right order
                var reversed = new Stack<String>(expr);
                var linetotal = Int64.Parse(Simplify(reversed));
                //Out.Print($"{line} = {linetotal}");

                total += linetotal;
            }

            return total.ToString();
        }

        public override string PartTwo(string input) {
            //input = "2 * 3 + (4 * 5)";

            var lines = input.AsLines();
            long total = 0;

            foreach (var line in lines) {
                var spaced = line.Replace("(", "( ");
                spaced = spaced.Replace(")", " )");
                var split = spaced.Split();
                var expr = new Stack<String>(split.Length); // to avoid capacity increases

                foreach (var token in split) {
                    if (Digits.Contains(token) || Operators.Contains(token) || token == "(") {
                        expr.Push(token);
                    } else if (token == ")") {
                        // Handle the sub-expression
                        var subexpr = new Stack<String>(split.Length);

                        while (expr.Peek() != "(") {
                            subexpr.Push(expr.Pop());
                        }
                        expr.Pop(); // pop the '('

                        // This stack is already in reverse order
                        expr.Push(SimplifyWithPriority(subexpr));
                    }
                }

                // Reverse the stack so we eval in the right order
                //var reversed = new Stack<String>(expr);
                var linetotal = Int64.Parse(SimplifyWithPriority(expr));
                //Out.Print($"{line} = {linetotal}");

                total += linetotal;
            }

            return total.ToString();
        }

        private string Simplify(Stack<string> expr) {
            string op;
            long value1, value2;

            while (expr.Count > 1) {
                value1 = Int64.Parse(expr.Pop());
                op = expr.Pop();
                value2 = Int64.Parse(expr.Pop());

                expr.Push(Evaluate(value1, value2, op));
            }

            return expr.Pop();
        }

        /// <summary>
        /// Simplify expression, prioritising addition
        /// </summary>
        private string SimplifyWithPriority(Stack<string> expr) {
            var reversed = new Stack<String>(expr.Count);

            string op;
            long value1, value2;

            // Perform the additions first
            while (expr.Count > 0) {
                var token = expr.Pop();

                if (token != "+") {
                    reversed.Push(token);
                } else {
                    op = token;
                    value1 = Int64.Parse(expr.Pop());
                    value2 = Int64.Parse(reversed.Pop());

                    reversed.Push(Evaluate(value1, value2, op));
                }
            }

            while (reversed.Count > 1) {
                value1 = Int64.Parse(reversed.Pop());
                op = reversed.Pop();
                value2 = Int64.Parse(reversed.Pop());

                reversed.Push(Evaluate(value1, value2, op));
            }

            return reversed.Pop();
        }


        private string Evaluate(long value1, long value2, string op) {
            return op switch
            {
                "+" => (value1 + value2).ToString(),
                "*" => (value1 * value2).ToString(),
                _ => throw new ArgumentException($"Bad operator '{op}'")
            };
        }
    }
}
