namespace TriviaGame;

/// <summary>
/// Represents a trivia question.
/// </summary>
public sealed class Question
{
    /// <summary>
    /// Unique identifier for the question.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the text of the trivia question.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the text of the trivia question.
    /// </value>
    public string QuestionText { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of the question.
    /// </summary>
    public QuestionType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the difficulty level assigned to the question.
    /// </summary>
    public QuestionDifficultyLevel DifficultyLevel { get; set; }

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
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of tags associated with the question.
    /// </summary>
    public ICollection<QuestionTag> Tags { get; set; } = new List<QuestionTag>();
    
    /// <summary>
    /// Gets or sets the collection of answers associated with the question.
    /// </summary>
    public ICollection<AnswerChoice> Choices { get; set; } = new List<AnswerChoice>();
}
