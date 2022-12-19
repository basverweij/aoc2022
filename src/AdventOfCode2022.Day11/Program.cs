var lines = File.ReadAllLines("input.txt");

var monkeys = lines.Chunk(7).Select(Parse).ToArray();

for (var round = 0; round < 20; round++)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.TryDequeue(out var level))
        {
            monkey.InspectionCount++;

            level = ApplyOperation(
                level,
                monkey.Operation);

            level /= 3;

            var to = level % monkey.TestValue == 0 ?
                monkey.MonkeyIfTrue :
                monkey.MonkeyIfFalse;

            monkeys[to].Items.Enqueue(level);
        }
    }
}

var puzzle1 = monkeys
    .OrderByDescending(m => m.InspectionCount)
    .Take(2)
    .Aggregate(1, (a, m) => a * m.InspectionCount);

Console.WriteLine($"Day 10 - Puzzle 1: {puzzle1}");

Monkey Parse(
string[] lines)
{
    var index = int.Parse(lines[0][7..^1]);

    var items = new Queue<int>(lines[1][18..].Split(", ").Select(int.Parse));

    var operation = ParseOperation(lines[2][19..]);

    var testValue = int.Parse(lines[3][21..]);

    var monkeyIfTrue = int.Parse(lines[4][29..]);

    var monkeyIfFalse = int.Parse(lines[5][30..]);

    return new(
        index,
        items,
        operation,
        testValue,
        monkeyIfTrue,
        monkeyIfFalse);
}

Operation ParseOperation(
    string line)
=> line switch
{
    ['o', 'l', 'd', ' ', '*', ' ', 'o', 'l', 'd'] => new(OperationTypes.Square),
    ['o', 'l', 'd', ' ', '+', ' ', ..] => new(OperationTypes.Add, int.Parse(line[6..])),
    ['o', 'l', 'd', ' ', '*', ' ', ..] => new(OperationTypes.Multiply, int.Parse(line[6..])),
    _ => throw new ArgumentException(line, nameof(line)),
};

int ApplyOperation(
    int level,
    Operation operation)
=> operation.Type switch
{
    OperationTypes.Square => level * level,
    OperationTypes.Add => level + operation.Value,
    OperationTypes.Multiply => level * operation.Value,
};

sealed record Monkey(
    int Index,
    Queue<int> Items,
    Operation Operation,
    int TestValue,
    int MonkeyIfTrue,
    int MonkeyIfFalse)
{
    public int InspectionCount { get; set; }
}

sealed record Operation(
    OperationTypes Type,
    int Value = 0);

enum OperationTypes
{
    Add,
    Multiply,
    Square,
}
