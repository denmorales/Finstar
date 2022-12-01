using Business_Logic.Services;
using Data_Access.Entities;

namespace Data_Access;

public class TodoLogRepository : ITodoLogRepository
{
    private readonly FinstarContext _context;

    public TodoLogRepository(FinstarContext context)
    {
        _context = context;
    }

    public async Task Log(string message)
    {
        _context.LogEntities.Add(new TodoLogEntity { Message = message });
        await _context.SaveChangesAsync();
    }
}