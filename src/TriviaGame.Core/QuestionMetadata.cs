namespace TriviaGame;

/// <summary>
/// Represents a reusable category for trivia questions.
/// </summary>
/// <param name="Name">The name of the category.</param>
public sealed record QuestionCategory(string Name);

/// <summary>
/// Represents a reusable tag for trivia questions.
/// </summary>
/// <param name="Name">The name of the tag.</param>
public sealed class QuestionTag(string Name);
