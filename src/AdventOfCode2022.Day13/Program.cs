using AdventOfCode2022.Day13;

var lines = File.ReadAllLines("input.txt");

var pairs = lines
    .Chunk(3)
    .Select(c => c.Take(2).Select(ParsePacket).ToArray())
    .ToArray();

var comparer = new ItemComparer();

var puzzle1 = pairs
    .Select((pair, i) => comparer.Compare(pair[0], pair[1]) == -1 ? i + 1 : 0)
    .Sum();

Console.WriteLine($"Day 13 - Puzzle 1: {puzzle1}");

var divider1 = BuildDivider(2);

var divider2 = BuildDivider(6);

var packets = pairs
    .SelectMany(pair => pair)
    .Concat(new[] { divider1, divider2 })
    .Order(new ItemComparer())
    .ToList();

var puzzle2 =
    (packets.IndexOf(divider1) + 1) *
    (packets.IndexOf(divider2) + 1);

Console.WriteLine($"Day 13 - Puzzle 2: {puzzle2}");

Item ParsePacket(
    string line)
{
    var item = new Item(null);

    ParseItem(
        item,
        line.AsSpan()[1..^1]);

    return item;
}

ReadOnlySpan<char> ParseItem(
    Item parent,
    ReadOnlySpan<char> span)
{
    if (span.Length == 0)
    {
        return span;
    }

    switch (span[0])
    {
        case '[': // start of list
            {
                var item = new Item(parent);

                parent.List.Add(item);

                return ParseItem(
                    item,
                    span[1..]);
            }

        case ']': // end of list

            return ParseItem(
                parent.Parent!,
                span[1..]);

        case ',': // next item

            return ParseItem(
                parent,
                span[1..]);
    }

    // value

    var i = 0;

    for (; i < span.Length; i++)
    {
        if (span[i] < '0' || span[i] > '9')
        {
            break;
        }
    }

    parent.List.Add(new(int.Parse(span[0..i])));

    return ParseItem(
        parent,
        span[i..]);
}

static Item BuildDivider(
    int value)
{
    return new Item(null)
    {
        List =
        {
            new(null)
            {
                List =
                {
                    new Item(value),
                },
            },
        },
    };
}
