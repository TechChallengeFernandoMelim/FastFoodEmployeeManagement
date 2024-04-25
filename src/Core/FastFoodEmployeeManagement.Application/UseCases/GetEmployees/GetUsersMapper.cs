using AutoMapper;
using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Application.UseCases.GetEmployees;

public class GetEmployeesMapper : Profile
{
    public GetEmployeesMapper()
    {
        CreateMap<EmployeeEntity, Employee>();
    }
}
