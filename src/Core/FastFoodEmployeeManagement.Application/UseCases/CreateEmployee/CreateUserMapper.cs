using AutoMapper;
using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;

public class CreateEmployeeMapper : Profile
{
    public CreateEmployeeMapper()
    {
        CreateMap<CreateEmployeeRequest, EmployeeEntity>();
    }
}
