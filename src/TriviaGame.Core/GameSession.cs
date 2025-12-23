namespace TriviaGame;

/// <summary>
/// Represents a game session in the trivia game.
/// </summary>
public sealed class GameSession
{
    /// <summary>
    /// Unique identifier for the game session.
    /// </summary>
    public Guid Id { get; set; }
    
    public GameSessionRules Rules { get; set; } = GameSessionRules.Default;
}

/// <summary>
/// Represents the set of rules that govern a game session.
/// </summary>
public sealed record GameSessionRules(
    ParticipationMode ParticipationMode,
    ResolutionMode ResolutionMode,
    RevealMode RevealMode,
    PhaseControlMode RevealControlMode,
    PhaseControlMode AdvanceControlMode,
    TimeSpan AnswerTimeLimit,
    TimeSpan? RevealDuration, // optional, RevealControlMode == Authoritative
    TimeSpan? IntermissionTimeLimit // optional, AdvanceControlMode == Authoritative
)
{
    public static readonly GameSessionRules Default = new(
        ParticipationMode: ParticipationMode.SinglePlayer,
        ResolutionMode: ResolutionMode.PerPlayer,
        RevealMode: RevealMode.Deferred,
        RevealControlMode: PhaseControlMode.Manual,
        AdvanceControlMode: PhaseControlMode.Manual,
        AnswerTimeLimit: TimeSpan.FromSeconds(30),
        RevealDuration: TimeSpan.FromSeconds(10),
        IntermissionTimeLimit: TimeSpan.FromSeconds(15)
    );
}
