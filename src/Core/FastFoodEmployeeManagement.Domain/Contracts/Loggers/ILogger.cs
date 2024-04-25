namespace FastFoodEmployeeManagement.Domain.Contracts.Logs;

public interface ILogger
{
    Task Log(string stackTrace, string message, string exception);
}
