using System.Runtime.InteropServices.Marshalling;

var lines = File.ReadAllLines("input.txt");

var pairs = lines
    .Chunk(3)
    .Select(c => c.Take(2).Select(ParsePacket).ToArray())
    .ToArray();

foreach (var pair in pairs)
{
    Console.WriteLine(pair[0]);
    Console.WriteLine(pair[1]);
    Console.WriteLine();
}

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

sealed class Item
{
    public readonly bool IsList;

    public readonly int Value;

    public readonly Item? Parent;

    public readonly List<Item> List = new();

    public Item(
        int value)
    {
        IsList = false;

        Value = value;
    }

    public Item(Item? parent)
    {
        IsList = true;

        Parent = parent;
    }

    public override string ToString()
    {
        return IsList ?
            $"[{string.Join(",", List)}]" :
            Value.ToString();
    }
}
