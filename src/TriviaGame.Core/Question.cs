namespace TriviaGame;

/// <summary>
/// Represents a trivia question.
/// </summary>
public sealed class Question
{
    /// <summary>
    /// Unique identifier for the question.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the text prompt of the trivia question.
    /// </summary>
    public required string Prompt { get; set; }
    
    /// <summary>
    /// Gets or sets the type of the question.
    /// </summary>
    public QuestionType Type { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level assigned to the question.
    /// </summary>
    public QuestionDifficultyLevel DifficultyLevel { get; set; } = QuestionDifficultyLevel.Medium;

    /// <summary>
    /// The content of the question.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The text or media type of the question.
    /// Defines the type for <see cref="Content"/>.
    /// </summary>
    public ContentType ContentType { get; set; }

    /// <summary>
    /// Gets or sets the category of the question.
    /// </summary>
    public required string Category { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of tags associated with the question.
    /// </summary>
    public ICollection<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the collection of answers associated with the question.
    /// </summary>
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();

    /// <summary>
    /// Represents a choice to answer a trivia question when it's <see cref="QuestionType.MultipleChoice"/>.
    /// </summary>
    public sealed record Choice(
        string Label, 
        string Content);
}
