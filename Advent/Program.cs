using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Advent {
    class Program {
        static readonly Regex DayPattern = new Regex(@"^(?<daynum>\d+)(?<daypart>[ab])$", RegexOptions.IgnoreCase);

        static void Main(string[] args) {
            if (args.Length == 0) {
                UsageAndExit();
            }

            // Load configuration
            var builder = new ConfigurationBuilder()
                .AddJsonFile("settings.json");
            var config = builder.Build().Get<Config>();

            Console.WriteLine(config.Session);

            // expecting a value like 1a or 12b
            var dayspec = args[0];
            var match = DayPattern.Match(dayspec);

            if (match.Success) {
                var day = Int32.Parse(match.Groups["daynum"].Value);
                var part = match.Groups["daypart"].Value.ToDayPart();
                
                // Create our day object
                var dayInstance = GetDayInstance(config.Year, day);

                if (dayInstance == null) {
                    UsageAndExit($"Day {day} could not be found");
                }

                // If we need input, get it
                var input = "";

                if (dayInstance.NeedsInput) {
                    input = GetInputForDay(config.InputDirectory, day);
                }

                // Run the right day part
                Console.WriteLine($"Running day {day} {part}");

                (var result, var elapsed) = RunDay(input, dayInstance, part);

                // Output the results
                Console.WriteLine($"ELAPSED: {elapsed}ms");
                Console.WriteLine($"RESULT : {result}");

            } else {
                UsageAndExit();
            }
        }

        static void UsageAndExit(string message = null) {
            if (message != null) {
                Console.WriteLine(message);
            }

            Console.WriteLine("ERROR: You must specify a day between 1 and 25 and a part e.g. 3a, 12b");
            Environment.Exit(1);
        }

        static IEnumerable<Type> GetDayTypes() {
            return Assembly.GetExecutingAssembly()
               .ExportedTypes
               .Where(t => t.IsSubclassOf(typeof(DayBase)))
               .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(DayAttribute)));
        }

        static Type GetDayType(DayAttribute day) {
            return GetDayTypes().Single(t => t.GetCustomAttribute<DayAttribute>().Year == day.Year &&
                                             t.GetCustomAttribute<DayAttribute>().Day == day.Day);
        }

        static DayBase GetDayInstance(int year, int day) {
            try {
                var dayType = GetDayType(new DayAttribute(year, day));
                var instance = (DayBase)Activator.CreateInstance(dayType);
                return instance;
            } catch (InvalidOperationException) {
                // No such day
                return null;
            }
        }

        static string GetInputForDay(string path, int day) {
            // Create the input filename
            var inputPath = Path.Combine(Path.GetFullPath(path), $"day{day:D2}.txt");
            Console.WriteLine("Loading input from " + inputPath);

            if (File.Exists(inputPath)) {
                return File.ReadAllText(inputPath);
            } else {
                throw new NotImplementedException("Still need to write the input downloader");
            }
        }

        static (string result, long elapsed) RunDay(string input, DayBase runner, DayPart part) {
            string result;
            var sw = Stopwatch.StartNew();

            if (part == DayPart.PartOne) {
                result = runner.PartOne(input);
            } else {
                result = runner.PartTwo(input);
            }

            return (result, sw.ElapsedMilliseconds);
        }
    }
}
