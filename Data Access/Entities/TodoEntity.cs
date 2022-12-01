using Business_Logic.Enums;

namespace Data_Access.Entities
{
    public class TodoEntity
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }
        public Colour Colour { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }    

    }
}
