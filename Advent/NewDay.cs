using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Advent.Util;

namespace Advent {
    public static class NewDay {

        const string TemplateName = "DayTemplate.cs";

        private static string ThisFilePath([CallerFilePath] string path = null) {
            return path;
        }

        public static async Task<bool> Create(DaySpec day) {
            // calculate the substitutions we'll need
            var yearPart = $"Year{day.Year}";
            var nameSpace = $"Advent.{yearPart}";
            var className = $"Day{day.Day:D2}";
            var attribute = $"[Day({day.Year}, {day.Day})]";

            var sourceLocation = Path.GetDirectoryName(ThisFilePath());
            var templatePath = Path.Combine(sourceLocation, TemplateName);
            var outputPath = Path.Combine(sourceLocation, yearPart, $"{className}.cs");

            if (File.Exists(outputPath)) {
                Out.Print($"ERROR: Code for {day} already exists");
                return false;
            }

            Out.Print($"Creating solution file for {day} at '{outputPath}'");

            var text = await File.ReadAllTextAsync(templatePath);
            text = text.Replace("namespace Advent", $"namespace {nameSpace}")
                .Replace("// DAY_ATTRIBUTE", attribute)
                .Replace("DayTemplate", className);
            await File.WriteAllTextAsync(outputPath, text);

            return true;
        }
    }
}
