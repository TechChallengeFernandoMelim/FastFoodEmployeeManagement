using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

public sealed record AuthenticateEmployeeRequest(string cpf) :
 IRequest<AuthenticateEmployeeResponse>;
