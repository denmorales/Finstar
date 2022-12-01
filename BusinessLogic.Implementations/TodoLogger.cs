using Business_Logic.Services;

namespace BusinessLogic.Implementations;

public class TodoLogger : ITodoLogger
{
    private readonly ITodoLogRepository _logRepository;

    public TodoLogger(ITodoLogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public Task Log(string message) => _logRepository.Log(message);
}