namespace Business_Logic.Models;

public class TodoBl
{
    public long Id { get; set; }
    public string Header { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Status { get; set; }
    public string Category { get; set; }
    public string Colour { get; set; }
    public CommentBl[] Comments { get; set; }
    public string Hash { get; set; }
}