var lines = File.ReadAllLines("input.txt");

var head = (x: 0, y: 0);

var tail = (x: 0, y: 0);

var hits = new HashSet<(int, int)>()
{
    tail,
};

foreach (var line in lines)
{
    var parts = line.Split(' ');

    var n = int.Parse(parts[1]);

    for (var i = 0; i < n; i++)
    {
        head = MoveHead(parts[0][0]);

        tail = MoveTail();

        hits.Add(tail);
    }
}

var puzzle1 = hits.Count;

Console.WriteLine($"Day 9 - Puzzle 1: {puzzle1}");

(int, int) MoveHead(char direction)
    => direction switch
    {
        'L' => (head.x - 1, head.y),
        'R' => (head.x + 1, head.y),
        'U' => (head.x, head.y - 1),
        'D' => (head.x, head.y + 1),
        _ => throw new ArgumentOutOfRangeException(nameof(direction)),
    };

(int, int) MoveTail()
{
    var dx = head.x - tail.x;

    var dy = head.y - tail.y;

    var threshold = dx * dx + dy * dy > 2 ? 0 : 1;

    return (
        tail.x + (dx > threshold ? 1 : dx < -threshold ? -1 : 0),
        tail.y + (dy > threshold ? 1 : dy < -threshold ? -1 : 0));
}
