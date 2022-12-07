var input = File.ReadAllText("input.txt").AsSpan();

var puzzle1 = -1;

for (var i = 4; i < input.Length; i++)
{
    if (AreAllDifferent(input[(i - 4)..i]))
    {
        puzzle1 = i;

        break;
    }
}

Console.WriteLine($"Day 6 - Puzzle 1: {puzzle1}");

var puzzle2 = -1;

for (var i = 14; i < input.Length; i++)
{
    if (AreAllDifferent(input[(i - 14)..i]))
    {
        puzzle2 = i;

        break;
    }
}

Console.WriteLine($"Day 6 - Puzzle 2: {puzzle2}");

static bool AreAllDifferent(
    ReadOnlySpan<char> chars)
{
    for (var i = 0; i < chars.Length; i++)
    {
        for (var j = i + 1; j < chars.Length; j++)
        {
            if (chars[i] == chars[j])
            {
                return false;
            }
        }
    }

    return true;
}