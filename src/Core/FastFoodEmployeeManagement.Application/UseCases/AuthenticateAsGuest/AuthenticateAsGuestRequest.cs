using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateAsGuest;

public sealed record AuthenticateAsGuestRequest : IRequest<AuthenticateAsGuestResponse>
{
}
