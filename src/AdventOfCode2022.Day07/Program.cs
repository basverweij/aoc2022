using System.Security.Cryptography;

var lines = File.ReadAllLines("input.txt");

var dirs = BuildDirs(lines);

var dirSizes = dirs.ToDictionary(
    kvp => kvp.Key,
    kvp => kvp.Value.Values.Sum());

var totalDirSizes = dirs.ToDictionary(
    kvp => kvp.Key,
    kvp => dirSizes.Where(dir => dir.Key.StartsWith(kvp.Key)).Sum(dir => dir.Value));

var puzzle1 = totalDirSizes.Values.Where(size => size <= 100000).Sum();

Console.WriteLine($"Day 7 - Puzzle 1: {puzzle1}");

static IReadOnlyDictionary<string, Dictionary<string, int>> BuildDirs(
    string[] lines)
{
    var dirs = new Dictionary<string, Dictionary<string, int>>();

    var cwd = new Stack<string>();

    foreach (var line in lines)
    {
        if (line[0..2] == "$ ")
        {
            // command

            switch (line[2..])
            {
                case "cd /":
                    cwd.Clear();

                    cwd.Push("");

                    break;

                case "cd ..":

                    cwd.Pop();

                    break;

                case "ls":
                    continue;

                default:
                    var dir = line[5..];

                    cwd.Push(dir);

                    break;
            }

            var cwdKey = ToString(cwd);

            if (!dirs.ContainsKey(cwdKey))
            {
                dirs[cwdKey] = new();
            }

            continue;
        }

        // output

        if (line[0..4] != "dir ")
        {
            var parts = line.Split(' ', 2);

            var size = int.Parse(parts[0]);

            var cwdKey = ToString(cwd);

            dirs[cwdKey].Add(parts[1], size);
        }
    }

    return dirs;
}

static string ToString(Stack<string> cwd)
    => cwd.Count == 1 ? "/" : string.Join("/", cwd.Reverse());
