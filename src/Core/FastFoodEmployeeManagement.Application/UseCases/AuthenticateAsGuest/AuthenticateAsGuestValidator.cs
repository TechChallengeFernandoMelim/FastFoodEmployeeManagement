using FluentValidation;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateAsGuest;

public class AuthenticateAsGuestValidator : AbstractValidator<AuthenticateAsGuestRequest>
{
    public AuthenticateAsGuestValidator()
    {

    }
}
