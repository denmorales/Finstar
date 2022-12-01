using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Models;

namespace Business_Logic.Services
{
    public interface ITodoService
    {
        public Task<GetAllRowsResponse> GetAllRowsAsync(CancellationToken cancellationToken);

        public Task<long> InsertNewRowAsync(InsertNewRowRequest request, CancellationToken token);

        public Task<GetRowDyIdResponse> GetRowByIdAsync(long id, CancellationToken token);

        public Task<long> DeleteRowByIdAsync(long id, CancellationToken token);

        public Task<long> UpsertHeaderByRowIdAsync(UpsertHeaderByRowIdRequest request, CancellationToken token);

        public Task<GetCommentByRowIdResponse> GetCommentsByRowIdAsync(long id, CancellationToken token);

        public Task<long> AddNewCommentByRowIdAsync(long id, string comment, CancellationToken cancellationToken);

    }
}
