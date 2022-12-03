var lines = await File.ReadAllLinesAsync("input.txt");

var elves = new List<int>();

var currentCalories = 0;

foreach (var line in lines)
{
    if (line == string.Empty)
    {
        elves.Add(currentCalories);

        currentCalories = 0;

        continue;
    }

    currentCalories += int.Parse(line);
}

elves.Sort();

Console.WriteLine($"Day 1 - Puzzle 1: {elves[^1]}");
