using Business_Logic.Enums;

namespace Business_Logic.Models;

public class TodoInsertBl
{
    public string Header { get; set; }
    public Status Status { get; set; }
    public Category Category { get; set; }
    public Colour Colour { get; set; }
}