using Application.Contracts.Models;
using AutoMapper;
using Business_Logic.Models;
using Business_Logic.Services;

namespace BusinessLogic.Implementations
{
    public class TodoService: ITodoService
    {
        private readonly ITodosRepository _iTodosRepository;
        private readonly IMapper _mapper;

        public TodoService(
            ITodosRepository iTodosRepository, IMapper mapper)
        {
            _iTodosRepository = iTodosRepository;
            _mapper = mapper;
        }
        public async Task<GetAllRowsResponse> GetAllRowsAsync(CancellationToken cancellationToken)
        {
            var rows =  await _iTodosRepository.GetAllRows(cancellationToken);
            var response = _mapper.Map<GetAllRowsResponse>(rows);
            return response;

        }
        public async Task<long> InsertNewRowAsync(InsertNewRowRequest request, CancellationToken token)
        {
            var requestBl = _mapper.Map<TodoInsertBl>(request);
            var newRowId = await _iTodosRepository.InsertTodo(requestBl, token);
            return newRowId;
        }

        public async Task<GetRowDyIdResponse> GetRowByIdAsync(long id, CancellationToken token)
        {
            var result = await _iTodosRepository.GetRowByIdAsync(id, token);
            var response = _mapper.Map<GetRowDyIdResponse>(result);
            return response;
        }

        public async Task<long> DeleteRowByIdAsync(long id, CancellationToken token)
        {
            var rowId = await _iTodosRepository.DeleteRowByIdAsync(id, token);
            return rowId;
        }

        public async Task<long> UpsertHeaderByRowIdAsync(UpsertHeaderByRowIdRequest request, CancellationToken token)
        {
            var result = _mapper.Map<TodoUpsertBl>(request);
            var rowId = await _iTodosRepository.UpsertHeaderByRowIdAsync(result, token);
            return rowId;
        }

        public async Task<GetCommentByRowIdResponse> GetCommentsByRowIdAsync(long id, CancellationToken token)
        {
            var response = await _iTodosRepository.GetCommentsByRowIdAsync(id, token);
            var result = _mapper.Map<GetCommentByRowIdResponse>(response);
            return result;
        }

        public async Task<long> AddNewCommentByRowIdAsync(long id, string comment, CancellationToken cancellationToken)
        {
            var newRowId = await _iTodosRepository.InsertComment(id, comment, cancellationToken);
            return newRowId;
        }
    }
}
