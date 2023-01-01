var lines = File.ReadAllLines("input.txt");

var paths = lines.Select(ParseLine);

var map = BuildMap(paths);

var puzzle1 = SettleSand(map);

Console.WriteLine($"Day 14 - Puzzle 1: {puzzle1}");

static (int x, int y)[] ParseLine(
    string line)
{
    var segments = line.Split(" -> ");

    return segments
        .Select(s => s.Split(','))
        .Select(s => (int.Parse(s[0]), int.Parse(s[1])))
        .ToArray();
}

static HashSet<(int x, int y)> BuildMap(
    IEnumerable<(int x, int y)[]> paths)
{
    var map = new HashSet<(int x, int y)>();

    foreach (var path in paths)
    {
        for (var i = 0; i < path.Length - 1; i++)
        {
            if (path[i].x == path[i + 1].x)
            {
                // vertical segment

                var (from, to) = (Math.Min(path[i].y, path[i + 1].y), Math.Max(path[i].y, path[i + 1].y));

                for (var y = from; y <= to; y++)
                {
                    map.Add((path[i].x, y));
                }
            }
            else
            {
                // horizontal segment

                var (from, to) = (Math.Min(path[i].x, path[i + 1].x), Math.Max(path[i].x, path[i + 1].x));

                for (var x = from; x <= to; x++)
                {
                    map.Add((x, path[i].y));
                }
            }
        }
    }

    return map;
}

static int SettleSand(
    HashSet<(int x, int y)> map)
{
    var (minX, maxX) = (map.Select(m => m.x).Min(), map.Select(m => m.x).Max());

    var maxY = map.Select(m => m.y).Max();

    var source = (500, 0);

    for (var settled = 0; ; settled++)
    {
        for (var sand = source; ;)
        {
            if (TrySettleSand(
                map,
                sand,
                out var next))
            {
                map.Add(sand);

                break;
            }

            if (next.x < minX ||
                next.x > maxX ||
                next.y > maxY)
            {
                // into the abyss

                return settled;
            }

            sand = next;
        }
    }
}

static bool TrySettleSand(
    HashSet<(int x, int y)> map,
    (int x, int y) sand,
    out (int x, int y) next)
{
    next = (sand.x, sand.y + 1);

    if (!map.Contains(next))
    {
        return false;
    }

    next = (sand.x - 1, sand.y + 1);

    if (!map.Contains(next))
    {
        return false;
    }

    next = (sand.x + 1, sand.y + 1);

    if (!map.Contains(next))
    {
        return false;
    }

    return true;
}
