namespace AdventOfCode.Days;

internal static class OutcomeScores
{
    public const int Loss = 0;
    public const int Draw = 3;
    public const int Win = 6;
}

internal abstract class Move
{
    protected abstract int GetMoveScore();
    protected abstract int GetOutcomeScore(Move opponentMove);
    public abstract Move GenerateLosingMove();
    public abstract Move GenerateWinningMove();
    public Move GenerateDrawMove() => this;

    public int GetRoundScore(Move opponentMove) => GetMoveScore() + GetOutcomeScore(opponentMove);
}

internal sealed class Rock : Move
{
    protected override int GetMoveScore() => 1;

    protected override int GetOutcomeScore(Move opponentMove) => opponentMove switch
    {
        Rock => OutcomeScores.Draw,
        Paper => OutcomeScores.Loss,
        Scissor => OutcomeScores.Win
    };

    public override Move GenerateLosingMove() => new Scissor();
    public override Move GenerateWinningMove() => new Paper();
}

internal sealed class Paper : Move
{
    protected override int GetMoveScore() => 2;

    protected override int GetOutcomeScore(Move opponentMove) => opponentMove switch
    {
        Rock => OutcomeScores.Win,
        Paper => OutcomeScores.Draw,
        Scissor => OutcomeScores.Loss
    };

    public override Move GenerateLosingMove() => new Rock();
    public override Move GenerateWinningMove() => new Scissor();
}

internal sealed class Scissor : Move
{
    protected override int GetMoveScore() => 3;

    protected override int GetOutcomeScore(Move opponentMove) => opponentMove switch
    {
        Rock => OutcomeScores.Loss,
        Paper => OutcomeScores.Win,
        Scissor => OutcomeScores.Draw,
    };

    public override Move GenerateLosingMove() => new Paper();
    public override Move GenerateWinningMove() => new Rock();
}

internal static class MoveCodes
{
    public const string OpponentRock = "A";
    public const string OpponentPaper = "B";
    public const string OpponentScissors = "C";
    public const string PlayerRock = "X";
    public const string PlayerPaper = "Y";
    public const string PlayerScissors = "Z";
}

internal static class StrategyCodes
{
    public const string Lose = "X";
    public const string Draw = "Y";
    public const string Win = "Z";
}

public class Day2 : AdventOfCodeDay
{
    protected override int GetDay() => 2;

    protected override string GetTask1Solution()
    {
        string inputPath = Path.Combine("Assets/day2.txt");

        return File.ReadLines(inputPath)
            .Select(round =>
            {
                string[] moves = round.Split(' ');
                Move opponentMove = ParseMove(moves[0]);
                Move playerMove = ParseMove(moves[1]);

                return playerMove.GetRoundScore(opponentMove);
            })
            .Sum()
            .ToString();
    }

    protected override string GetTask2Solution()
    {
        string inputPath = Path.Combine("Assets/day2.txt");

        return File.ReadLines(inputPath)
            .Select(round =>
            {
                string[] moves = round.Split(' ');
                Move opponentMove = ParseMove(moves[0]);
                Move playerMove = GetStrategicMove(opponentMove, moves[1]);
                
                return playerMove.GetRoundScore(opponentMove);
            })
            .Sum()
            .ToString();

        Move GetStrategicMove(Move opponentMove, string strategy) => strategy switch
        {
            StrategyCodes.Lose => opponentMove.GenerateLosingMove(),
            StrategyCodes.Draw => opponentMove.GenerateDrawMove(),
            StrategyCodes.Win => opponentMove.GenerateWinningMove(),
            _ => throw new ArgumentException("Invalid strategy")
        };
    }

    private static Move ParseMove(string rawMove) => rawMove switch
    {
        MoveCodes.OpponentRock or MoveCodes.PlayerRock => new Rock(),
        MoveCodes.OpponentPaper or MoveCodes.PlayerPaper => new Paper(),
        MoveCodes.OpponentScissors or MoveCodes.PlayerScissors => new Scissor(),
        _ => throw new ArgumentException("Invalid move value")
    };
}