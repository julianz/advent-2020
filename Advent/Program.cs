using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Advent {
    class Program {
        static readonly Regex DayPattern = new Regex(@"^(?<daynum>\d+)(?<daypart>[ab])$", RegexOptions.IgnoreCase);
        static Config Config;

        static void Main(string[] args) {
            if (args.Length == 0) {
                UsageAndExit();
            }

            // Load configuration
            var builder = new ConfigurationBuilder()
                .AddJsonFile("settings.json");
            Config = builder.Build().Get<Config>();

            // Expecting a value like 1a or 12b
            var dayspec = args[0];
            var match = DayPattern.Match(dayspec);

            if (match.Success) {
                var day = Int32.Parse(match.Groups["daynum"].Value);
                var part = match.Groups["daypart"].Value.ToDayPart();
                
                // Create our day object
                var dayInstance = GetDayInstance(Config.Year, day);

                if (dayInstance == null) {
                    UsageAndExit($"Day {day} could not be found");
                }

                // If we need input, get it
                var input = "";

                if (dayInstance.NeedsInput) {
                    input = GetInputForDay(day);
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

        static string GetInputForDay(int day) {
            // Create the input filename
            var inputPath = Path.Combine(Path.GetFullPath(Config.InputDirectory), $"day{day:D2}.txt");
            Console.WriteLine("Loading input from " + inputPath);

            if (!File.Exists(inputPath)) {
                DownloadInputForDay(day, inputPath);
            }

            return File.ReadAllText(inputPath);
        }

        private static bool DownloadInputForDay(int day, string downloadPath) {
            var url = $"https://adventofcode.com/{Config.Year}/day/{day}/input";

            if (Config.SessionCookie == "") {
                throw new InvalidDataException("You need to put the session cookie in the settings.json file");
            }

            using var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Cookie, Config.SessionCookie);
            Console.WriteLine("Downloading input file from " + url);
            client.DownloadFile(url, downloadPath);

            return true;
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
