using Application.Contracts.Models;
using AutoMapper;
using Business_Logic.Models;
using Business_Logic.Services;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly ITodosRepository _iTodosRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoService> _logger;

        public TodoService(
            ITodosRepository iTodosRepository, IMapper mapper, ILogger<TodoService> logger)
        {
            _iTodosRepository = iTodosRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetAllRowsResponse> GetAllRowsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var rows = await _iTodosRepository.GetAllRows(cancellationToken);
                var response = _mapper.Map<GetAllRowsResponse>(rows);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        public async Task<long> InsertNewRowAsync(InsertNewRowRequest request, CancellationToken token)
        {
            try
            {
                var requestBl = _mapper.Map<TodoInsertBl>(request);
                var newRowId = await _iTodosRepository.InsertTodo(requestBl, token);
                return newRowId;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<GetRowDyIdResponse> GetRowByIdAsync(long id, CancellationToken token)
        {
            try
            {
                var result = await _iTodosRepository.GetRowByIdAsync(id, token);
                var response = _mapper.Map<GetRowDyIdResponse>(result);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task DeleteRowByIdAsync(long id, CancellationToken token)
        {
            try
            {
                await _iTodosRepository.DeleteRowByIdAsync(id, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<long> UpdateHeaderByRowIdAsync(UpdateHeaderByRowIdRequest request, CancellationToken token)
        {
            try
            {
                var result = _mapper.Map<TodoUpsertBl>(request);
                var rowId = await _iTodosRepository.UpdateHeaderByRowIdAsync(result, token);
                return rowId;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<GetCommentByRowIdResponse> GetCommentsByRowIdAsync(long id, CancellationToken token)
        {
            try
            {
                var response = await _iTodosRepository.GetCommentsByRowIdAsync(id, token);
                var result = _mapper.Map<GetCommentByRowIdResponse>(response);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<long> AddNewCommentByRowIdAsync(long id, string comment, CancellationToken cancellationToken)
        {
            try
            {
                var newRowId = await _iTodosRepository.InsertComment(id, comment, cancellationToken);
                return newRowId;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
