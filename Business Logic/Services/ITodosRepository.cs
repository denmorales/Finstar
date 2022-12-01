using Application.Contracts.Models;
using Business_Logic.Models;

namespace Business_Logic.Services
{
    public interface ITodosRepository
    {
        Task<TodoBl[]> GetAllRows(CancellationToken token);

        Task<long> InsertTodo(TodoInsertBl request, CancellationToken token);

        Task<long> InsertComment(long id, string comment, CancellationToken token);

        Task<long> DeleteRowByIdAsync(long id, CancellationToken token);

        Task<TodoBl> GetRowByIdAsync(long id, CancellationToken token);
        Task<CommentModel[]> GetCommentsByRowIdAsync(long id, CancellationToken token);

        Task<long> UpsertHeaderByRowIdAsync(TodoUpsertBl request, CancellationToken token);
    }
}
