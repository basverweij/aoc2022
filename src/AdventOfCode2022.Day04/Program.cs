var lines = await File.ReadAllLinesAsync("input.txt");

var puzzle1 = lines.Select(Parse).Select(FullyOverlaps).Sum();

Console.WriteLine($"Day 4 - Puzzle 1: {puzzle1}");

var puzzle2 = lines.Select(Parse).Select(Overlaps).Sum();

Console.WriteLine($"Day 4 - Puzzle 2: {puzzle2}");

static ((int, int), (int, int)) Parse(
    string line)
{
    var parts = line.Split(',', '-').Select(int.Parse).ToArray();

    return ((parts[0], parts[1]), (parts[2], parts[3]));
}

static int FullyOverlaps(
    ((int start, int end), (int start, int end)) pair)
{
    var (a, b) = pair;

    return
        (a.start >= b.start && a.end <= b.end) ||
        (b.start >= a.start && b.end <= a.end) ?
        1 :
        0;
}

static int Overlaps(
    ((int start, int end), (int start, int end)) pair)
{
    var (a, b) = pair;

    return
        (a.end < b.start || a.start > b.end) ?
        0 :
        1;
}
