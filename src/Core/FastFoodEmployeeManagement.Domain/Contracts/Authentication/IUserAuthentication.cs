using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Domain.Contracts.Authentication;

public interface IEmployeeAuthentication
{
    Task<string> AuthenticateEmployee(EmployeeEntity Employee, CancellationToken cancellationToken);
}
