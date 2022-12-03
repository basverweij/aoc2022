var lines = await File.ReadAllLinesAsync("input.txt");

var maxCalories = 0;

var currentCalories = 0;

foreach (var line in lines)
{
    if (line == string.Empty)
    {
        if (currentCalories > maxCalories)
        {
            maxCalories = currentCalories;
        }

        currentCalories = 0;

        continue;
    }

    currentCalories += int.Parse(line);
}

Console.WriteLine($"Day 1 - Puzzle 1: {maxCalories}");
