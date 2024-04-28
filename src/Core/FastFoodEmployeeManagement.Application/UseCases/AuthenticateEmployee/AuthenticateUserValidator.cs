using FluentValidation;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

public class AuthenticateEmployeeValidator : AbstractValidator<AuthenticateEmployeeRequest>
{
    public AuthenticateEmployeeValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .WithMessage("O e-mail deve estar preenchido.")
            .EmailAddress()
            .WithMessage("O e-mail fornecido não é válido.");

        RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage("A senha deve estar preenchida.")
            .Length(11, 11)
            .WithMessage("A senha deve ter 11 caracteres.");
    }

}
