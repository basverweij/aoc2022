var lines = File.ReadAllLines("input.txt");

var size = lines.Length;

var rows = lines.Select(line => line.ToCharArray()).ToArray();

var scores = new int[size][];

var puzzle1 = (size - 1) * 4; // edges are always visible

for (var i = 1; i < size - 1; i++)
{
    scores[i] = new int[size];

    for (var j = 1; j < size - 1; j++)
    {
        var (isVisible, score) = CheckTree(i, j);

        if (isVisible)
        {
            puzzle1++;
        }

        scores[i][j] = score;
    }
}

Console.WriteLine($"Day 8 - Puzzle 1: {puzzle1}");

var puzzle2 = scores[1..^1].SelectMany(s => s[1..^1]).Max();

Console.WriteLine($"Day 8 - Puzzle 2: {puzzle2}");

(bool, int) CheckTree(int i, int j)
{
    var height = rows[i][j];

    var sides = 0;

    var score = 1;

    int k;

    for (k = i - 1; k >= 0; k--)
    {
        if (rows[k][j] >= height)
        {
            sides++;

            break;
        }
    }

    score *= (i - k - (k == -1 ? 1 : 0));

    for (k = i + 1; k < size; k++)
    {
        if (rows[k][j] >= height)
        {
            sides++;

            break;
        }
    }

    score *= (k - i - (k == size ? 1 : 0));

    for (k = j - 1; k >= 0; k--)
    {
        if (rows[i][k] >= height)
        {
            sides++;

            break;
        }
    }

    score *= (j - k - (k == -1 ? 1 : 0));

    for (k = j + 1; k < size; k++)
    {
        if (rows[i][k] >= height)
        {
            sides++;

            break;
        }
    }

    score *= (k - j - (k == size ? 1 : 0));

    return (sides < 4, score);
}
