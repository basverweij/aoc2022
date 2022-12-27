var lines = File.ReadAllLines("input.txt");

var map = lines.Select(line => line.ToCharArray()).ToArray();

var sizeY = map.Length;

var sizeX = map[0].Length;

var start = (x: 0, y: 0);

var end = (x: 0, y: 0);

for (int y = 0; y < sizeY; y++)
{
    for (var x = 0; x < sizeX; x++)
    {
        if (map[y][x] == 'S')
        {
            start = (x, y);

            map[y][x] = 'a';
        }
        else if (map[y][x] == 'E')
        {
            end = (x, y);

            map[y][x] = 'z';
        }
    }
}

int puzzle1 = FindPath(map, sizeX, sizeY, start, end);

Console.WriteLine($"Day 12 - Puzzle 1: {puzzle1}");

var puzzle2 = map
    .SelectMany((row, y) => row.Select((elevation, x) => (x, y, elevation)))
    .Where(s => s.elevation == 'a')
    .Select(s => FindPath(map, sizeX, sizeY, (s.x, s.y), end))
    .Min();

Console.WriteLine($"Day 12 - Puzzle 2: {puzzle2}");

static int FindPath(char[][] map, int sizeX, int sizeY, (int x, int y) start, (int x, int y) end)
{
    var steps = Enumerable.Range(0, sizeY).Select(_ => Enumerable.Repeat(int.MaxValue, sizeX).ToArray()).ToArray();

    steps[start.y][start.x] = 0;

    var candidates = new Queue<(int x, int y)>();

    candidates.Enqueue(start);

    while (candidates.TryDequeue(out var candidate))
    {
        if (candidate == end)
        {
            break;
        }

        var (x, y) = candidate;

        var s = steps[y][x];

        if (x > 0 &&
            (map[y][x - 1] - map[y][x]) <= 1 &&
            steps[y][x - 1] > s + 1)
        {
            candidates.Enqueue((x - 1, y));

            steps[y][x - 1] = s + 1;
        }

        if (x < sizeX - 1 &&
            (map[y][x + 1] - map[y][x]) <= 1 &&
            steps[y][x + 1] > s + 1)
        {
            candidates.Enqueue((x + 1, y));

            steps[y][x + 1] = s + 1;
        }

        if (y > 0 &&
            (map[y - 1][x] - map[y][x]) <= 1 &&
            steps[y - 1][x] > s + 1)
        {
            candidates.Enqueue((x, y - 1));

            steps[y - 1][x] = s + 1;
        }

        if (y < sizeY - 1 &&
            (map[y + 1][x] - map[y][x]) <= 1 &&
            steps[y + 1][x] > s + 1)
        {
            candidates.Enqueue((x, y + 1));

            steps[y + 1][x] = s + 1;
        }
    }

    return steps[end.y][end.x];
}
