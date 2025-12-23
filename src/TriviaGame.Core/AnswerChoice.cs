namespace TriviaGame;

/// <summary>
/// Represents an answer to a trivia question.
/// </summary>
public sealed class AnswerChoice
{
    /// <summary>
    /// Unique identifier for the answer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A descriptive label for the trivia answer, typically used to provide a distinguishable identifier or key.
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Represents the textual representation of an answer to a trivia question.
    /// </summary>
    public string Answer { get; set; } = string.Empty;
    
    /// <summary>
    /// The text or media type of the answer content.
    /// </summary>
    public ContentType ContentType { get; set; } = ContentType.Text;
    
    /// <summary>
    /// The content of the answer.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier for the related trivia question.
    /// </summary>
    public Guid QuestionId { get; set; }
}
