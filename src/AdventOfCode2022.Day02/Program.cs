var lines = await File.ReadAllLinesAsync("input.txt");

var puzzle1 = lines.Select(ParseLine).Select(PlayRound).Sum();

Console.WriteLine($"Day 2 - Puzzle 1: {puzzle1}");

static int PlayRound((Hands, Hands) round)
{
    var (opponent, advice) = round;

    return (int)advice + 1 + Outcome(opponent, advice);
}

static int Outcome(Hands opponent, Hands player)
{
    return (opponent, player) switch
    {
        // win
        (Hands.Rock, Hands.Paper) => 6,
        (Hands.Paper, Hands.Scissors) => 6,
        (Hands.Scissors, Hands.Rock) => 6,

        // loose
        (Hands.Rock, Hands.Scissors) => 0,
        (Hands.Paper, Hands.Rock) => 0,
        (Hands.Scissors, Hands.Paper) => 0,

        // draw
        _ => 3,
    };
}

static (Hands, Hands) ParseLine(string line)
{
    return ((Hands)(line[0] - 'A'), (Hands)(line[2] - 'X'));
}

enum Hands
{
    Rock,
    Paper,
    Scissors,
}