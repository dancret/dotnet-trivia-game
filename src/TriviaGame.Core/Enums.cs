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
