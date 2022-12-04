using System.Linq;

var lines = await File.ReadAllLinesAsync("input.txt");

var puzzle1 = lines.Select(GetDuplicate).Select(GetPriority).Sum();

Console.WriteLine($"Day 3 - Puzzle 1: {puzzle1}");

static char GetDuplicate(
    string line) =>
    line[..(line.Length / 2)].Intersect(line[(line.Length / 2)..]).Single();

static int GetPriority(
    char item) =>
    item switch
    {
        >= 'a' and <= 'z' => item - 'a' + 1,
        >= 'A' and <= 'Z' => item - 'A' + 27,
        _ => 0,
    };