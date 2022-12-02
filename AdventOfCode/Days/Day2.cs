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

public class Day2 : AdventOfCodeDay
{
    protected override int GetDay() => 2;

    protected override string GetTask1Solution()
    {
        return GetScores()
            .Sum()
            .ToString();
    }

    protected override string GetTask2Solution()
    {
        return base.GetTask2Solution();
    }

    private static IEnumerable<int> GetScores()
    {
        string inputPath = Path.Combine("Assets/day2.txt");

        return File.ReadLines(inputPath)
            .Select(round =>
            {
                string[] moves = round.Split(' ');
                Move opponentMove = ParseMove(moves[0]);
                Move playerMove = ParseMove(moves[1]);

                return playerMove.GetRoundScore(opponentMove);
            });
    }

    private static Move ParseMove(string rawMove) => rawMove switch
    {
        MoveCodes.OpponentRock or MoveCodes.PlayerRock => new Rock(),
        MoveCodes.OpponentPaper or MoveCodes.PlayerPaper => new Paper(),
        MoveCodes.OpponentScissors or MoveCodes.PlayerScissors => new Scissor(),
        _ => throw new ArgumentException("Invalid move value")
    };
}