namespace TriviaGame;

/// <summary>
/// Represents a reusable category for trivia questions.
/// </summary>
public sealed class QuestionCategory
{
    /// <summary>
    /// Unique identifier for the category.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The name of the category.
    /// </summary>
    public required string Name { get; set; }
}

/// <summary>
/// Represents a reusable tag for trivia questions.
/// </summary>
public sealed class QuestionTag
{
    /// <summary>
    /// Unique identifier for the tag.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the tag.
    /// </summary>
    public required string Name { get; set; }
}
