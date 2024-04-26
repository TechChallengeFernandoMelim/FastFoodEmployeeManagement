using FastFoodEmployeeManagement.Application.UseCases;
using FastFoodEmployeeManagement.Application.UseCases.AuthenticateAsGuest;
using FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;
using FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;
using FastFoodEmployeeManagement.Application.UseCases.GetEmployees;
using FastFoodEmployeeManagement.Domain.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodEmployeeManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(IValidationNotifications validationNotifications, IMediator mediator) : BaseController(validationNotifications)
{
    /// <summary>
    /// Create a new Employee.
    /// </summary>
    /// <returns>Id of customer created</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiBaseResponse<CreateEmployeeResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiBaseResponse<CreateEmployeeResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiBaseResponse))]
    [HttpPost("CreateEmployee")]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest customerCreateRequestDto, CancellationToken cancellationToken)
    {
        var data = await mediator.Send(customerCreateRequestDto, cancellationToken);
        return await Return(new ApiBaseResponse<CreateEmployeeResponse>() { Data = data });
    }

    /// <summary>
    /// Authenticate Employee.
    /// </summary>
    /// <returns>Employee with token</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiBaseResponse<AuthenticateEmployeeResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiBaseResponse<AuthenticateEmployeeResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiBaseResponse))]
    [HttpGet("AuthenticateEmployee/{cpf}")]
    public async Task<IActionResult> AuthenticateEmployee(string cpf, CancellationToken cancellationToken)
    {
        var data = await mediator.Send(new AuthenticateEmployeeRequest(cpf), cancellationToken);
        return await Return(new ApiBaseResponse<AuthenticateEmployeeResponse>() { Data = data });
    }

    /// <summary>
    /// Authenticate as guest
    /// </summary>
    /// <returns>Token</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiBaseResponse<AuthenticateAsGuestResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiBaseResponse<AuthenticateAsGuestResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiBaseResponse))]
    [HttpGet("AuthenticateAsGuest")]
    public async Task<IActionResult> AuthenticateAsGuest(CancellationToken cancellationToken)
    {
        var data = await mediator.Send(new AuthenticateAsGuestRequest(), cancellationToken);
        return await Return(new ApiBaseResponse<AuthenticateAsGuestResponse>() { Data = data });
    }

    /// <summary>
    /// Retrieve a list of all Employees.
    /// </summary>
    /// <returns>List of Employees</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiBaseResponse<GetEmployeesResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiBaseResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiBaseResponse<GetEmployeesResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiBaseResponse))]
    [HttpGet("GetEmployees")]
    public async Task<IActionResult> GetEmployees(CancellationToken cancellationToken)
    {
        var data = await mediator.Send(new GetEmployeesRequest(), cancellationToken);
        return await Return(new ApiBaseResponse<GetEmployeesResponse>() { Data = data });
    }

    [HttpGet("NotImplementedException")]
    public async Task<IActionResult> NotImplementedException(CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Endpoint para testar logger - FastFoodEmployeeManagement.");
    }
}
