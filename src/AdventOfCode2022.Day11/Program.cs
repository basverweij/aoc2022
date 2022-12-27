var lines = File.ReadAllLines("input.txt");

var monkeys1 = lines.Chunk(7).Select(Parse).ToArray();

var puzzle1 = PerformRounds(
    monkeys1,
    20,
    3);

Console.WriteLine($"Day 11 - Puzzle 1: {puzzle1}");

var monkeys2 = lines.Chunk(7).Select(Parse).ToArray();

var puzzle2 = PerformRounds(
    monkeys2,
    10_000,
    1);

Console.WriteLine($"Day 11 - Puzzle 2: {puzzle2}");

Monkey Parse(
    string[] lines)
{
    var index = int.Parse(lines[0][7..^1]);

    var items = new Queue<long>(lines[1][18..].Split(", ").Select(long.Parse));

    var operation = ParseOperation(lines[2][19..]);

    var testValue = long.Parse(lines[3][21..]);

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
    ['o', 'l', 'd', ' ', '+', ' ', ..] => new(OperationTypes.Add, long.Parse(line[6..])),
    ['o', 'l', 'd', ' ', '*', ' ', ..] => new(OperationTypes.Multiply, long.Parse(line[6..])),
    _ => throw new ArgumentException(line, nameof(line)),
};

long ApplyOperation(
    long level,
    Operation operation,
    long testValuesProduct)
=> operation.Type switch
{
    OperationTypes.Square => (level * level) % testValuesProduct,
    OperationTypes.Add => level + operation.Value,
    OperationTypes.Multiply => (level * operation.Value) % testValuesProduct,
};

long PerformRounds(
    Monkey[] monkeys,
    int rounds,
    long worryLevelDivisor)
{
    var testValuesProduct = monkeys.Aggregate(worryLevelDivisor, (a, m) => a * m.TestValue);

    var inspectionCounts = new long[monkeys.Length];

    for (var round = 0; round < rounds; round++)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.TryDequeue(out var level))
            {
                inspectionCounts[monkey.Index]++;

                level = ApplyOperation(
                    level,
                    monkey.Operation,
                    testValuesProduct);

                level /= worryLevelDivisor;

                var to = level % monkey.TestValue == 0 ?
                    monkey.MonkeyIfTrue :
                    monkey.MonkeyIfFalse;

                monkeys[to].Items.Enqueue(level);
            }
        }
    }

    return inspectionCounts
        .OrderDescending()
        .Take(2)
        .Aggregate(1L, (a, c) => a * c);
}

sealed record Monkey(
    int Index,
    Queue<long> Items,
    Operation Operation,
    long TestValue,
    int MonkeyIfTrue,
    int MonkeyIfFalse);

sealed record Operation(
    OperationTypes Type,
    long Value = 0);

enum OperationTypes
{
    Add,
    Multiply,
    Square,
}
