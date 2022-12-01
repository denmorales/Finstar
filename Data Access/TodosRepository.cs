using Application.Contracts.Models;
using AutoMapper;
using Business_Logic.Models;
using Business_Logic.Services;
using Data_Access.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access
{
    public class TodosRepository : ITodosRepository
    {
        private readonly FinstarContext _context;
        private readonly IMapper _mapper;

        public TodosRepository(FinstarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> DeleteRowByIdAsync(long id, CancellationToken token)
        {
            var row = await _context.Todos.Where(i => i.Id == id).FirstOrDefaultAsync(token);
            if (row == null) return 0;
            _context.Todos.Remove(row);
            return row.Id;
        }

        public async Task<TodoBl[]> GetAllRows(CancellationToken token)
        {
            var rows = await _context.Todos
                .Include(td => td.Comments)
                .ToArrayAsync(token);
            var todoBl = _mapper.Map<TodoBl[]>(rows);
            return todoBl;
        }

        public async Task<CommentModel[]> GetCommentsByRowIdAsync(long id, CancellationToken token)
        {
            var response = await _context.Comments.Where(i => i.TodoId == id).ToArrayAsync(token);
            var result = _mapper.Map<CommentModel[]>(response);
            return result;
        }

        public async Task<TodoBl> GetRowByIdAsync(long id, CancellationToken token)
        {
            var row = await _context.Todos.Where(i => i.Id == id).SingleOrDefaultAsync(token);
            var result = _mapper.Map<TodoBl>(row);
            return result;
        }

        public async Task<long> InsertTodo(TodoInsertBl request, CancellationToken token)
        {
            var result = _mapper.Map<TodoEntity>(request);
            _context.Todos.Add(result);
            await _context.SaveChangesAsync(token);
            return result.Id;
        }

        public async Task<long> InsertComment(long id, string comment, CancellationToken token)
        {
            _context.Comments.Add(new CommentEntity { Text = comment, TodoId = id });
            await _context.SaveChangesAsync(token);
            return id;
        }

        public async Task<long> UpsertHeaderByRowIdAsync(TodoUpsertBl request, CancellationToken token)
        {
            var result = await _context.Todos.Where(i => i.Id == request.Id).FirstOrDefaultAsync(token);
            if (result == null) return 0;
            result.Header = request.Header;
            await _context.SaveChangesAsync(token);
            return result.Id;

        }
    }
}
