using AutoMapper;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Exceptions;
using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

public class AuthenticateEmployeeHandler(IEmployeeRepository EmployeeRepository, IMapper mapper, IEmployeeAuthentication EmployeeAuthentication) : IRequestHandler<AuthenticateEmployeeRequest, AuthenticateEmployeeResponse>
{
    public async Task<AuthenticateEmployeeResponse> Handle(AuthenticateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var cpf = request.cpf.Replace(".", string.Empty).Replace("-", string.Empty);

        var Employee = await EmployeeRepository.GetEmployeeByCPFOrEmailAsync(cpf, string.Empty, cancellationToken)
            ?? throw new ObjectNotFoundException("Usuário não encontrado para esse CPF");

        var response = mapper.Map<AuthenticateEmployeeResponse>(Employee);
        response.Token = await EmployeeAuthentication.AuthenticateEmployee(Employee, cancellationToken);

        return response;
    }
}
