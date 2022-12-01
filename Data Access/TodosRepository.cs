using Application.Contracts.Models;
using AutoMapper;
using Business_Logic.Models;
using Business_Logic.Services;
using Data_Access.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data_Access
{
    public class TodosRepository : ITodosRepository
    {
        private readonly FinstarContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TodosRepository> _logger;

        public TodosRepository(FinstarContext context, IMapper mapper, ILogger<TodosRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task DeleteRowByIdAsync(long id, CancellationToken token)
        {
            try
            {
                var row = await _context.Todos.FindAsync(id, token);
                if (row != null)
                    _context.Todos.Remove(row);
                await _context.SaveChangesAsync(token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<TodoBl[]> GetAllRows(CancellationToken token)
        {
            try
            {
                var rows = await _context.Todos
                    .Include(td => td.Comments)
                    .ToArrayAsync(token);
                var todoBl = _mapper.Map<TodoBl[]>(rows);
                return todoBl;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<CommentBl[]> GetCommentsByRowIdAsync(long id, CancellationToken token)
        {
            try
            {
                var response = await _context.Comments.Where(i => i.TodoId == id).ToArrayAsync(token);
                var result = _mapper.Map<CommentBl[]>(response);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<TodoBl> GetRowByIdAsync(long id, CancellationToken token)
        {
            try
            {
                var row = await _context.Todos.Include(x=>x.Comments).FirstOrDefaultAsync(x=>x.Id==id, token);
                var result = _mapper.Map<TodoBl>(row);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<long> InsertTodo(TodoInsertBl request, CancellationToken token)
        {
            try
            {
                var result = _mapper.Map<TodoEntity>(request);
                _context.Todos.Add(result);
                await _context.SaveChangesAsync(token);
                return result.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<long> InsertComment(long id, string comment, CancellationToken token)
        {
            try
            {
                var todo = await _context.Todos.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id, token);
                if (todo == null) return 0;
                todo.Comments.Add(new CommentEntity { Text = comment, TodoId = id });
                await _context.SaveChangesAsync(token);
                return id;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<long> UpdateHeaderByRowIdAsync(TodoUpsertBl request, CancellationToken token)
        {
            try
            {
                var result = await _context.Todos.Where(i => i.Id == request.Id).FirstOrDefaultAsync(token);
                if (result == null) return 0;
                result.Header = request.Header;
                await _context.SaveChangesAsync(token);
                return result.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }
    }
}
