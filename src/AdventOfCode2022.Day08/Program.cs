var lines = File.ReadAllLines("input.txt");

var rows = lines.Select(line => line.ToCharArray()).ToArray();

var puzzle1 = (rows.Length - 1) * 4; // edges are always visible

for (var i = 1; i < rows.Length - 1; i++)
{
    var row = rows[i];

    for (var j = 1; j < row.Length - 1; j++)
    {
        if (IsVisible(rows, i, j))
        {
            puzzle1++;
        }
    }
}

Console.WriteLine($"Day 8 - Puzzle 1: {puzzle1}");

static bool IsVisible(char[][] rows, int i, int j)
{
    var height = rows[i][j];

    var sides = 0;

    for (var k = i - 1; k >= 0; k--)
    {
        if (rows[k][j] >= height)
        {
            sides++;

            break;
        }
    }

    for (var k = i + 1; k < rows.Length; k++)
    {
        if (rows[k][j] >= height)
        {
            sides++;

            break;
        }
    }

    for (var k = j - 1; k >= 0; k--)
    {
        if (rows[i][k] >= height)
        {
            sides++;

            break;
        }
    }

    for (var k = j + 1; k < rows[i].Length; k++)
    {
        if (rows[i][k] >= height)
        {
            sides++;

            break;
        }
    }

    return sides < 4;
}