var lines = File.ReadAllLines("input.txt");

var puzzle1 = Simulate(lines, 2).Count;

Console.WriteLine($"Day 9 - Puzzle 1: {puzzle1}");

var puzzle2 = Simulate(lines, 10).Count;

Console.WriteLine($"Day 9 - Puzzle 2: {puzzle2}");

HashSet<(int, int)> Simulate(
    string[] lines,
    int length)
{
    var knots = new (int x, int y)[length];

    var hits = new HashSet<(int, int)>()
    {
        knots[^1],
    };

    foreach (var line in lines)
    {
        var parts = line.Split(' ');

        var n = int.Parse(parts[1]);

        for (var i = 0; i < n; i++)
        {
            knots[0] = MoveHead(
                knots[0],
                parts[0][0]);

            for (var j = 1; j < knots.Length; j++)
            {
                knots[j] = MoveKnot(
                    knots[j - 1],
                    knots[j]);
            }

            hits.Add(knots[^1]);
        }
    }

    return hits;
}

(int, int) MoveHead(
    (int x, int y) head,
    char direction)
    => direction switch
    {
        'L' => (head.x - 1, head.y),
        'R' => (head.x + 1, head.y),
        'U' => (head.x, head.y - 1),
        'D' => (head.x, head.y + 1),
        _ => throw new ArgumentOutOfRangeException(nameof(direction)),
    };

(int, int) MoveKnot(
    (int x, int y) previous,
    (int x, int y) knot)
{
    var dx = previous.x - knot.x;

    var dy = previous.y - knot.y;

    var threshold = dx * dx + dy * dy > 2 ? 0 : 1;

    return (
        knot.x + (dx > threshold ? 1 : dx < -threshold ? -1 : 0),
        knot.y + (dy > threshold ? 1 : dy < -threshold ? -1 : 0));
}
