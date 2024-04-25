using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.GetEmployees;

public sealed record GetEmployeesRequest : IRequest<GetEmployeesResponse>
{

}
