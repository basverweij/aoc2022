var lines = File.ReadAllLines("input.txt");

var instructions = lines.Select(Parse).ToArray();

var screen = new char[240];

var i = -1;

var signalStrengths = new List<int>();

var x = 1;

var remainingCycles = 0;

for (var cycle = 1; ; cycle++)
{
    if (remainingCycles == 0)
    {
        if (++i == instructions.Length)
        {
            break;
        }

        remainingCycles = instructions[i].cycles;
    }

    if ((cycle - 20) % 40 == 0)
    {
        signalStrengths.Add(cycle * x);
    }

    DrawPixel(cycle);

    if (--remainingCycles == 0)
    {
        x += instructions[i].dx;
    }
}

var puzzle1 = signalStrengths.Sum();

Console.WriteLine($"Day 10 - Puzzle 1: {puzzle1}");

Console.WriteLine($"Day 10 - Puzzle 2:");

for (i = 0; i < 6; i++)
{
    Console.WriteLine(screen[(i * 40)..(i * 40 + 40)]);
}

static (int cycles, int dx) Parse(string line)
    => line switch
    {
        "noop" => (1, 0),
        ['a', 'd', 'd', 'x', ' ', ..] => (2, int.Parse(line[5..])),
        _ => throw new ArgumentOutOfRangeException(nameof(line)),
    };

void DrawPixel(
    int cycle)
{
    var i = (cycle - 1) % 240; // position in screen buffer

    var j = i % 40; // position in current row

    screen[i] =
        j >= (x - 1) && j <= (x + 1) ?
        '#' :
        '.';
}
