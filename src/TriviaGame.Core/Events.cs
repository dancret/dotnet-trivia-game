namespace TriviaGame;

public interface IGameEvent;

public sealed record CommandRejected(string Reason) : IGameEvent;

public sealed record AnswerSubmitted(GameRoundId RoundId, Answer Answer) : IGameEvent;
public sealed record AnsweringClosed(GameRoundId RoundId, DateTimeOffset ClosedAtUtc) : IGameEvent;

public sealed record RevealStarted(GameRoundId RoundId, DateTimeOffset StartedAtUtc) : IGameEvent;
public sealed record RevealEnded(GameRoundId RoundId, DateTimeOffset EndedAtUtc) : IGameEvent;

public sealed record IntermissionStarted(GameRoundId RoundId, DateTimeOffset StartedAtUtc) : IGameEvent;
public sealed record IntermissionEnded(GameRoundId RoundId, DateTimeOffset EndedAtUtc) : IGameEvent;

public sealed record RoundCompleted(GameRoundId RoundId, DateTimeOffset CompletedAtUtc) : IGameEvent;
