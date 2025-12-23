namespace TriviaGame;

/// <summary>
/// Represents the type of trivia question.
/// </summary>
public enum QuestionType
{
    MultipleChoice,
    TrueOrFalse,
    FreeForm
}

/// <summary>
/// Represents the difficulty level of a trivia question.
/// </summary>
public enum QuestionDifficultyLevel
{
    VeryEasy = 1,
    Easy = 2,
    Medium = 3,
    Hard = 4,
    VeryHard = 5,
}

/// <summary>
/// Represents the type of media associated with a trivia question.
/// </summary>
public enum ContentType
{
    Text,
    Image,
    Audio,
    Video
}

/// <summary>
/// Defines how players participate in a round.
/// </summary>
public enum ParticipationMode
{
    /// <summary>
    /// Turn-based, one player is eligible to answer each question.
    /// </summary>
    SinglePlayer,
    /// <summary>
    /// All players are eligible to answer each question.
    /// </summary>
    AllPlayers
}

/// <summary>
/// Specifies the available modes for determining how answers are resolved and scored in a game session.
/// </summary>
public enum ResolutionMode
{
    /// <summary>
    /// First correct answer wins.
    /// </summary>
    FirstCorrect,
    /// <summary>
    /// Each player can answer and is scored independently. 
    /// </summary>
    PerPlayer
}

/// <summary>
/// Specifies when the correct answer is revealed in a quiz or game round.
/// </summary>
public enum RevealMode
{
    /// <summary>
    /// Correct answer is revealed immediately after phase ends or when we have <see cref="ResolutionMode.FirstCorrect"/>.
    /// </summary>
    Immediate,
    /// <summary>
    /// Correct answer is revealed manually or scheduled later on.
    /// </summary>
    Deferred
}

/// <summary>
/// Specifies the modes for controlling the progression of phases in the game session.
/// </summary>
public enum PhaseControlMode
{
    /// <summary>
    /// Indicates that the phase deadlines authoritative and moves forward automatically.
    /// </summary>
    Authoritative,
    /// <summary>
    /// Indicates that the progression of phases in the game session is controlled manually,
    /// requiring explicit input or action to advance to the next phase.
    /// </summary>
    Manual
}
