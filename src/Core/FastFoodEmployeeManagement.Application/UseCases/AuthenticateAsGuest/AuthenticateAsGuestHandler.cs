using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Entities;
using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateAsGuest;

public class AuthenticateAsGuestHandler(IEmployeeAuthentication EmployeeAuthentication) : IRequestHandler<AuthenticateAsGuestRequest, AuthenticateAsGuestResponse>
{
    public async Task<AuthenticateAsGuestResponse> Handle(AuthenticateAsGuestRequest request, CancellationToken cancellationToken)
    {
        var Employee = new EmployeeEntity()
        {
            Email = Environment.GetEnvironmentVariable("GUEST_EMAIL"),
            Identification = Environment.GetEnvironmentVariable("GUEST_IDENTIFICATION")
        };

        var response = new AuthenticateAsGuestResponse
        {
            Token = await EmployeeAuthentication.AuthenticateEmployee(Employee, cancellationToken)
        };

        return response;
    }
}
