﻿using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Domain.Contracts.Repositories;

public interface IEmployeeRepository
{
    Task<bool> AddEmployeeAsync(EmployeeEntity customer, CancellationToken cancellationToken);
    Task<EmployeeEntity> GetEmployeeByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<EmployeeEntity>> GetEmployeesAsync(CancellationToken cancellationToken);
}
