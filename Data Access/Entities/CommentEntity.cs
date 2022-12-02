using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Entities
{
    public class CommentEntity
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long TodoId { get; set; }
        public TodoEntity Todo { get; set; }
    }
}
