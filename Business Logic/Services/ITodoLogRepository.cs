namespace Business_Logic.Services;

public interface ITodoLogRepository
{
    Task Log(string message);
}