namespace TriviaGame;

public interface ITriviaCommand { }

public abstract record TriviaCommand : ITriviaCommand;

public sealed record CreateSessionCommand(
    string SessionName,
    GameSessionRules Rules,
    Player[] Participants
) : TriviaCommand;

public sealed record StartGameCommand(string SessionId) : TriviaCommand;

public sealed record EndGameCommand(string SessionId) : TriviaCommand;

