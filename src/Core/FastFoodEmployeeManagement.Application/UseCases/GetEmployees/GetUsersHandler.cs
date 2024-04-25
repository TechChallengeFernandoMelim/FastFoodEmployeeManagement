using AutoMapper;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.GetEmployees;

public class GetEmployeesHandler(IEmployeeRepository EmployeeRepository, IMapper mapper) : IRequestHandler<GetEmployeesRequest, GetEmployeesResponse>
{
    public async Task<GetEmployeesResponse> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        var customers = await EmployeeRepository.GetEmployeesAsync(cancellationToken);

        var customersDto = mapper.Map<IEnumerable<Employee>>(customers);

        return new GetEmployeesResponse() { Employees = customersDto };
    }
}
