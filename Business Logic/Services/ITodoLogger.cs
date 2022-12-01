namespace Business_Logic.Services;

public interface ITodoLogger
{
    Task Log(string message);
}