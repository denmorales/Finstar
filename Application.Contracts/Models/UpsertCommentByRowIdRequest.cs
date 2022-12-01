namespace Application.Contracts.Models
{
    public class UpsertHeaderByRowIdRequest
    {
        public long Id { get; set; }
        public string Comment { get; set; }
    }
}
