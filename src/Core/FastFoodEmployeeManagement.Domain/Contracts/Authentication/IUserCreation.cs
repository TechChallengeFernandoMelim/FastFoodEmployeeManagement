using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Domain.Contracts.Authentication;

public interface IEmployeeCreation
{
    Task<string> CreateEmployee(EmployeeEntity Employee, CancellationToken cancellationToken);
}
