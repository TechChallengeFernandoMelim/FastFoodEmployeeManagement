using FluentValidation;

namespace FastFoodEmployeeManagement.Application.UseCases.GetEmployees;

public class GetEmployeesValidator : AbstractValidator<GetEmployeesRequest>
{
    public GetEmployeesValidator()
    {

    }
}
