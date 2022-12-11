using static MoreLinq.Extensions.ForEachExtension;

namespace Advent.Year2022 {
    [Day(2022, 7)]
    public class Day07 : DayBase {

        static readonly Regex cd_re = new Regex(@"^\$ cd (?<target>.*)$", RegexOptions.IgnoreCase);
        static readonly Regex dir_re = new Regex(@"^dir (?<dirname>.*)$", RegexOptions.IgnoreCase);

        public override async Task<string> PartOne(string input) {

            var root = FindFileSystem(new Dir { Name = "/" }, input.AsLines());
            
            var smallDirs = root.DirsSmallerThan(100000).ToList();
            return smallDirs.Sum(d => d.TotalSize).ToString();
        }

        public override async Task<string> PartTwo(string input) {
            //input = @"$ cd /
            //        $ ls
            //        dir a
            //        14848514 b.txt
            //        8504156 c.dat
            //        dir d
            //        $ cd a
            //        $ ls
            //        dir e
            //        29116 f
            //        2557 g
            //        62596 h.lst
            //        $ cd e
            //        $ ls
            //        584 i
            //        $ cd ..
            //        $ cd ..
            //        $ cd d
            //        $ ls
            //        4060174 j
            //        8033020 d.log
            //        5626152 d.ext
            //        7214296 k";

            var root = FindFileSystem(new Dir { Name = "/" }, input.AsLines());

            var diskSpace = 70000000L;
            var required = 30000000L;
            var used = root.TotalSize;
            var available = diskSpace - used;
            var needed = required - available;

            WriteLine($"Disk has {available} bytes free, needs {needed} more bytes");

            var result = root.EnumerateDirs()
                .Where(d => d.TotalSize > needed)
                .OrderBy(d => d.TotalSize)
                .First();

            WriteLine(result);
            return result.TotalSize.ToString();
        }

        Dir FindFileSystem(Dir root, IEnumerable<string> lines) {
            var cwd = root;

            foreach (var line in lines) {

                if (line.StartsWith("$ ls")) {
                    // ls is a no-op
                    continue;
                }

                // cd command
                Match match = cd_re.Match(line);
                if (match.Success) {
                    var target = match.Groups["target"].Value;
                    switch (target) {
                        case "/":
                            //WriteLine($"Changing dir to root");
                            cwd = root;
                            break;

                        case "..":
                            //WriteLine($"Changing dir to {cwd.Parent.Name}");
                            cwd = cwd.Parent;
                            break;

                        default:
                            //WriteLine($"Changing dir to {target}");
                            cwd = cwd.Subdirs.First(d => d.Name == target);
                            break;
                    }
                    continue;
                }

                // dir entry
                match = dir_re.Match(line);
                if (match.Success) {
                    cwd.Subdirs.Add(new Dir { Name = match.Groups["dirname"].Value, Parent = cwd });
                    continue;
                }

                // must be a file entry
                var gap = line.IndexOf(' ');
                var size = long.Parse(line.AsSpan(0, gap));
                cwd.Files.Add(new FileItem { Name = line.AsSpan(gap + 1).ToString(), Size = size });
            }

            return root;
        }
    }

    public class FileItem {
        public string Name { get; init; }
        public long Size { get; init; }
        public override string ToString() => $"File name='{Name}', size={Size}";
    }

    public class Dir {
        public string Name { get; init; }

        public Dir Parent { get; init; }

        public IList<Dir> Subdirs { get; init; }

        public IList<FileItem> Files { get; init; }

        public Dir() {
            Parent = null; // If you don't give a parent you're at the root.
            Subdirs = new List<Dir>();
            Files = new List<FileItem>();
        }

        public long TotalSize => Files.Sum(f => f.Size) + Subdirs.Sum(d => d.TotalSize);

        public IEnumerable<Dir> DirsSmallerThan(long maxSize) {
            var dirs = new List<Dir>();
            if (TotalSize < maxSize) {
                dirs.Add(this);
            }

            foreach (var d in Subdirs) {
                dirs.AddRange(d.DirsSmallerThan(maxSize));
            }

            return dirs;
        }

        public IEnumerable<Dir> EnumerateDirs() {
            var dirs = new List<Dir> { this };

            foreach (var d in Subdirs) {
                dirs.AddRange(d.EnumerateDirs());
            }

            return dirs;
        }

        public override string ToString() => $"Dir name='{Name}', total size={TotalSize}";
    }
}
