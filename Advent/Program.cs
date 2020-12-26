using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Advent.Util;

namespace Advent {
    class Program {
        static readonly Regex DayPattern = new Regex(@"^(?<daynum>\d+)(?<daypart>[ab])$", RegexOptions.IgnoreCase);
        static readonly Regex YearPattern = new Regex(@"^20[12]\d$");
        static Config Config;

        static void Main(string[] args) {
            if (args.Length == 0) {
                UsageAndExit();
            }

            // Load configuration and sort out current working directory
            var builder = new ConfigurationBuilder()
                .AddJsonFile("settings.json");
            Config = builder.Build().Get<Config>();

            if (Config.ApplicationDirectory == "") {
                Config.ApplicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }

            // Read the command line params
            //  - year is optional, otherwise we use the default from config
            //  - day must be expressed in the form 1a or 12b

            var day = new DaySpec { Year = Config.DefaultYear };

            foreach (var arg in args) {
                if (YearPattern.IsMatch(arg)) {
                    day.Year = Int32.Parse(YearPattern.Match(arg).Value);
                } else {
                    var match = DayPattern.Match(arg);

                    if (match.Success) {
                        day.Day = Int32.Parse(match.Groups["daynum"].Value);
                        day.Part = match.Groups["daypart"].Value.ToDayPart();
                    }
                }
            }

            if (!day.IsSet) {
                UsageAndExit("Day was not specified on the command line");
            }

            // Create our day object
            var dayInstance = GetDayInstance(day);

            if (dayInstance == null) {
                UsageAndExit($"Code for {day.Year} day {day.Day} could not be found");
            }

            // If we need input, get it
            var input = "";

            if (dayInstance.NeedsInput) {
                input = GetInputForDay(day);
            }

            // Run the right day part
            (var result, var elapsed) = RunDay(day, dayInstance, input);

            // Output the results
            Out.NewLine();
            Out.Print($"ELAPSED: {elapsed.Ticks / 10000.0}ms");
            Out.Print($"RESULT : {result}");
        }

        static void UsageAndExit(string message = null) {
            if (message != null) {
                Out.Print(message);
            }

            Out.Print("ERROR: You must specify a day between 1 and 25 and a part e.g. 3a, 12b");
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

        static DayBase GetDayInstance(DaySpec day) {
            try {
                var dayType = GetDayType(new DayAttribute(day.Year, day.Day));
                var instance = (DayBase)Activator.CreateInstance(dayType);
                return instance;
            } catch (InvalidOperationException) {
                // No such day
                return null;
            }
        }

        static string GetInputForDay(DaySpec day) {
            // Find out where the input dir is.
            string inputDir;

            if (Config.InputDirectory == Path.GetFullPath(Config.InputDirectory)) {
                // The input directory was overridden so just use the overridden value.
                inputDir = Path.Combine(Config.InputDirectory, day.Year.ToString());
            } else {
                // Use the application directory.
                inputDir = Path.Combine(Config.ApplicationDirectory, Config.InputDirectory, day.Year.ToString());
            }

            // Complain if the directory doesn't exist.
            if (!Directory.Exists(inputDir)) {
                throw new DirectoryNotFoundException($"Input directory not found: '{inputDir}'");
            }
            
            var inputPath = Path.Combine(inputDir, $"day{day.Day:D2}.txt");
            Out.Print("Loading input from " + inputPath);

            if (!File.Exists(inputPath)) {
                DownloadInputForDay(day, inputPath);
            }

            return File.ReadAllText(inputPath);
        }

        private static bool DownloadInputForDay(DaySpec day, string downloadPath) {
            var url = $"https://adventofcode.com/{day.Year}/day/{day.Day}/input";

            if (Config.SessionCookie == "") {
                throw new InvalidDataException("You need to put the session cookie in the settings.json file");
            }

            using var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Cookie, Config.SessionCookie);
            Out.Print("Downloading input file from " + url);
            client.DownloadFile(url, downloadPath);

            return true;
        }

        static (string result, TimeSpan elapsed) RunDay(DaySpec day, DayBase instance, string input) {
            Out.Print($"Running {day}" + Environment.NewLine);

            string result;
            var sw = Stopwatch.StartNew();

            if (day.Part == DayPart.PartOne) {
                result = instance.PartOne(input);
            } else {
                result = instance.PartTwo(input);
            }

            sw.Stop();
            return (result, sw.Elapsed);
        }
    }
}
