namespace TriviaGame;

/// <summary>
/// Represents a game session in the trivia game.
/// </summary>
public sealed class GameSession
{
    /// <summary>
    /// Unique identifier for the game session.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the trivia game session.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rules that govern the current game session.
    /// </summary>
    public GameSessionRules Rules { get; set; } = GameSessionRules.Default;
}

public readonly record struct PlayerId(string Id);

/// <summary>
/// Game session participant.
/// </summary>
/// <remarks>Can also represent a team.</remarks>
/// <param name="Name"></param>
public sealed record Player(PlayerId Id, string Name);

public sealed record Answer(Player Player, DateTimeOffset SubmittedAtUtc, string Value);

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
