using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;

public sealed record CreateEmployeeRequest(
    string Name, string Email, string Password) :
     IRequest<CreateEmployeeResponse>;
