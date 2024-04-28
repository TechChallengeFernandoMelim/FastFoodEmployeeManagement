using FluentValidation;

namespace FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 255)
            .WithMessage("O nome deve ter no minimo 3 e no máximo 255 caracteres.")
            .NotEmpty()
            .WithMessage("O nome deve estar preenchido.");

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
