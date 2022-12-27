using AdventOfCode2022.Day13;

var lines = File.ReadAllLines("input.txt");

var pairs = lines
    .Chunk(3)
    .Select(c => c.Take(2).Select(ParsePacket).ToArray())
    .ToArray();

var puzzle1 = pairs
    .Select((pair, i) => IsInRightOrder(pair[0], pair[1]) is true ? i + 1 : 0)
    .Sum();

Console.WriteLine($"Day 13 - Puzzle 1: {puzzle1}");

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

bool? IsInRightOrder(
    Item left,
    Item right)
{
    if (!left.IsList && !right.IsList)
    {
        if (left.Value < right.Value)
        {
            return true;
        }

        if (left.Value > right.Value)
        {
            return false;
        }

        return null;
    }

    if (!left.IsList && right.IsList)
    {
        left = new(left)
        {
            List =
            {
                left,
            },
        };

        return IsInRightOrder(
            left,
            right);
    }

    if (left.IsList && !right.IsList)
    {
        right = new(right)
        {
            List =
            {
                right,
            },
        };

        return IsInRightOrder(
            left,
            right);
    }

    // both are lists

    for (var i = 0; i < left.List.Count; i++)
    {
        if (i >= right.List.Count)
        {
            return false;
        }

        var isInRightOrder = IsInRightOrder(
            left.List[i],
            right.List[i]);

        if (isInRightOrder != null)
        {
            return isInRightOrder;
        }
    }

    return left.List.Count == right.List.Count ?
        null :
        true;
}
