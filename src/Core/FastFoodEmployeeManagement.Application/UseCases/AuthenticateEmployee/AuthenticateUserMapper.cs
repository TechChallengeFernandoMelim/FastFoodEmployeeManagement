using AutoMapper;
using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

public class AuthenticateEmployeeMapper: Profile
{
    public AuthenticateEmployeeMapper()
    {
        CreateMap<EmployeeEntity, AuthenticateEmployeeResponse>();
    }
}
