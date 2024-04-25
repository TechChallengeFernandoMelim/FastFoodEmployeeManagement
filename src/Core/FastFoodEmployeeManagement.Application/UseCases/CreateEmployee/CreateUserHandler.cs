using AutoMapper;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Entities;
using FastFoodEmployeeManagement.Domain.Validations;
using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;

public class CreateEmployeeHandler(
    IEmployeeRepository EmployeeRepository, 
    IMapper mapper, IValidationNotifications 
    validationNotifications, 
    IEmployeeCreation EmployeeCreation) : IRequestHandler<CreateEmployeeRequest, CreateEmployeeResponse>
{
    public async Task<CreateEmployeeResponse> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var Employee = mapper.Map<EmployeeEntity>(request);
        Employee.Identification = Employee.Identification.Replace(".", string.Empty).Replace("-", string.Empty);

        var existingCustomer = await EmployeeRepository.GetEmployeeByCPFOrEmailAsync(Employee.Identification, Employee.Email, cancellationToken);

        if (existingCustomer != null)
            validationNotifications.AddError("Identification", "Já existe um usuário cadastrado com esse CPF ou e-mail.");
        else
        {
            var cognitoEmployeeIdentification = await EmployeeCreation.CreateEmployee(Employee, cancellationToken);
            Employee.CognitoEmployeeIdentification = cognitoEmployeeIdentification;
            await EmployeeRepository.AddEmployeeAsync(Employee, cancellationToken);
        }

        return new CreateEmployeeResponse();
    }
}
