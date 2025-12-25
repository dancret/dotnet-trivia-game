using System.Collections.ObjectModel;

namespace TriviaGame;

/// <summary>
/// Stable identity for a round instance (can be session-scoped or globally unique).
/// </summary>
public readonly record struct GameRoundId(Guid Value)
{
    public static GameRoundId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString("N");
}

/// <summary>
/// Minimal phase vocabulary for the round runtime state machine.
/// </summary>
public enum RoundPhase
{
    Answering = 0,
    Reveal = 1,
    Intermission = 2,
    Completed = 3
}

/// <summary>
/// Persisted snapshot of a single trivia round.
/// Contains no behavior or authority — only facts.
/// </summary>
public sealed record GameRound(
    GameRoundId RoundId,
    int RoundNumber,
    Question Question,
    IReadOnlySet<Player> EligiblePlayers,
    RoundPhase Phase,
    DateTimeOffset StartedAtUtc,
    DateTimeOffset AnswerDeadlineUtc,
    DateTimeOffset? RevealEndsAtUtc,
    DateTimeOffset? IntermissionEndsAtUtc,
    IReadOnlyDictionary<PlayerId, Answer> AnswersByPlayer
)
{
    public static GameRound Create(
        GameRoundId roundId,
        int roundNumber,
        Question question,
        IReadOnlySet<Player> eligiblePlayers,
        GameSessionRules rules,
        DateTimeOffset nowUtc)
    {
        if (roundNumber <= 0) throw new ArgumentOutOfRangeException(nameof(roundNumber));
        if (question is null) throw new ArgumentNullException(nameof(question));
        if (eligiblePlayers is null) throw new ArgumentNullException(nameof(eligiblePlayers));
        if (eligiblePlayers.Count == 0) throw new ArgumentException("EligiblePlayers cannot be empty.", nameof(eligiblePlayers));
        if (rules is null) throw new ArgumentNullException(nameof(rules));
        if (nowUtc.Offset != TimeSpan.Zero) throw new ArgumentException("nowUtc must be UTC.", nameof(nowUtc));

        var answerDeadlineUtc = nowUtc + rules.AnswerTimeLimit;

        DateTimeOffset? revealEndsAtUtc = null;
        if (rules.RevealControlMode == PhaseControlMode.Authoritative)
        {
            if (rules.RevealDuration is null)
                throw new ArgumentException("RevealDuration must be set when RevealControlMode is Authoritative.", nameof(rules));

            revealEndsAtUtc = answerDeadlineUtc + rules.RevealDuration.Value;
        }

        DateTimeOffset? intermissionEndsAtUtc = null;
        if (rules.AdvanceControlMode == PhaseControlMode.Authoritative)
        {
            if (rules.IntermissionTimeLimit is null)
                throw new ArgumentException("IntermissionTimeLimit must be set when AdvanceControlMode is Authoritative.", nameof(rules));

            var intermissionStart = revealEndsAtUtc ?? answerDeadlineUtc;
            intermissionEndsAtUtc = intermissionStart + rules.IntermissionTimeLimit.Value;
        }

        return new GameRound(
            RoundId: roundId,
            RoundNumber: roundNumber,
            Question: question,
            EligiblePlayers: eligiblePlayers,
            Phase: RoundPhase.Answering,
            StartedAtUtc: nowUtc,
            AnswerDeadlineUtc: answerDeadlineUtc,
            RevealEndsAtUtc: revealEndsAtUtc,
            IntermissionEndsAtUtc: intermissionEndsAtUtc,
            AnswersByPlayer: new ReadOnlyDictionary<PlayerId, Answer>(new Dictionary<PlayerId, Answer>())
        );
    }
}