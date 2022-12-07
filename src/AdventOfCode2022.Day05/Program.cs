var input = await File.ReadAllLinesAsync("input.txt");

var start = input.TakeWhile(line => !line.StartsWith(" 1")).ToArray();

var stacks = BuildStacks(
    input,
    start);

var moves = input.Skip(start.Length + 2).Select(ParseMove).ToArray();

foreach (var (count, from, to) in moves)
{
    for (var i = 0; i < count; i++)
    {
        stacks[to - 1].Push(stacks[from - 1].Pop());
    }
}

var puzzle1 = new string(stacks.Select(s => s.Peek()).ToArray());

Console.WriteLine($"Day 5 - Puzzle 1: {puzzle1}");

stacks = BuildStacks(
    input,
    start);

foreach (var (count, from, to) in moves)
{
    var stack = new Stack<char>(count);

    for (var i = 0; i < count; i++)
    {
        stack.Push(stacks[from - 1].Pop());
    }

    for (var i = 0; i < count; i++)
    {
        stacks[to - 1].Push(stack.Pop());
    }
}

var puzzle2 = new string(stacks.Select(s => s.Peek()).ToArray());

Console.WriteLine($"Day 5 - Puzzle 2: {puzzle2}");

static Stack<char>[] BuildStacks(string[] input, string[] start)
{
    var stacks = new Stack<char>[(input[start.Length].Length + 1) / 4];

    for (var j = 0; j < stacks.Length; j++)
    {
        stacks[j] = new();

        for (var i = start.Length - 1; i >= 0; i--)
        {
            var c = start[i][j * 4 + 1];

            if (c == ' ')
            {
                continue;
            }

            stacks[j].Push(c);
        }
    }

    return stacks;
}

static (int count, int from, int to) ParseMove(
    string line)
{
    var parts = line.Split(' ');

    return (int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]));
}
