namespace Application.Contracts.Models;

/// <summary>
/// запись TODO
/// </summary>
public class TodoModel
{
    /// <summary>
    /// уникальный id
    /// </summary>
    public long Id { get; set; }
    public string Header { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Status { get; set; }
    public string Category { get; set; }
    public string Colour { get; set; }
    public CommentModel[] Comments { get; set; }
    public string Hash { get; set; }
}