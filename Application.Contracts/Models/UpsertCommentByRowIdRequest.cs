namespace Application.Contracts.Models
{
    public class UpdateHeaderByRowIdRequest
    {
        public long Id { get; set; }
        public string Header { get; set; }
    }
}
