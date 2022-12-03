var lines = await File.ReadAllLinesAsync("input.txt");

var puzzle1 = lines.Select(ParseLine1).Select(PlayRound).Sum();

Console.WriteLine($"Day 2 - Puzzle 1: {puzzle1}");

var puzzle2 = lines.Select(ParseLine2).Select(DetermineHand).Select(PlayRound).Sum();

Console.WriteLine($"Day 2 - Puzzle 2: {puzzle2}");

static (Hands, Hands) DetermineHand((Hands, Outcomes) round)
{
    var (opponent, outcome) = round;

    var hand = (opponent, outcome) switch
    {
        (Hands.Rock, Outcomes.Lose) => Hands.Scissors,
        (Hands.Paper, Outcomes.Lose) => Hands.Rock,
        (Hands.Scissors, Outcomes.Lose) => Hands.Paper,

        (Hands.Rock, Outcomes.Win) => Hands.Paper,
        (Hands.Paper, Outcomes.Win) => Hands.Scissors,
        (Hands.Scissors, Outcomes.Win) => Hands.Rock,

        _ => opponent,
    };

    return (opponent, hand);
}

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

        // lose
        (Hands.Rock, Hands.Scissors) => 0,
        (Hands.Paper, Hands.Rock) => 0,
        (Hands.Scissors, Hands.Paper) => 0,

        // draw
        _ => 3,
    };
}

static (Hands, Hands) ParseLine1(string line) => ((Hands)(line[0] - 'A'), (Hands)(line[2] - 'X'));

static (Hands, Outcomes) ParseLine2(string line) => ((Hands)(line[0] - 'A'), (Outcomes)(line[2] - 'X'));

enum Hands
{
    Rock,
    Paper,
    Scissors,
}

enum Outcomes
{
    Lose,
    Draw,
    Win,
}