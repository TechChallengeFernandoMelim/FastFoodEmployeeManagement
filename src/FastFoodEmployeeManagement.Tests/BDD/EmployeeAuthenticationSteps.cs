using AutoMapper;
using FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Entities;
using Moq;
using System.Threading;
using TechTalk.SpecFlow;

namespace FastFoodEmployeeManagement.Tests.BDD;

[Binding]
public class EmployeeAuthenticationSteps
{
    private readonly Mock<IEmployeeRepository> employeeRepositoryMock = new Mock<IEmployeeRepository>();
    private readonly Mock<IEmployeeAuthentication> employeeAuthenticationMock = new Mock<IEmployeeAuthentication>();
    private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
    private AuthenticateEmployeeRequest request;
    private AuthenticateEmployeeResponse response;
    private string expectedToken = "testToken";
    private EmployeeEntity employeeEntity;

    [Given(@"an employee with email ""(.*)"" and password ""(.*)"" exists in the system")]
    public void GivenAnEmployeeExistsInTheSystem(string email, string password)
    {
        employeeEntity = new EmployeeEntity { Email = email, Password = password };

        employeeRepositoryMock.Setup(u => u.GetEmployeeByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employeeEntity);

        employeeAuthenticationMock.Setup(u => u.AuthenticateEmployee(employeeEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedToken);
    }

    [When(@"the employee attempts to authenticate")]
    public async void WhenTheEmployeeAttemptsToAuthenticate()
    {
        var cancellationToken = new CancellationToken();
        request = new AuthenticateEmployeeRequest(employeeEntity.Email, employeeEntity.Password);

        var handler = new AuthenticateEmployeeHandler(employeeRepositoryMock.Object, mapperMock.Object, employeeAuthenticationMock.Object);

        var expectedResponse = new AuthenticateEmployeeResponse { Email = employeeEntity.Email, Token = expectedToken };
        mapperMock.Setup(m => m.Map<AuthenticateEmployeeResponse>(employeeEntity)).Returns(expectedResponse);

        response = await handler.Handle(request, cancellationToken);
    }

    [Then(@"the system should return a token")]
    public void ThenTheSystemShouldReturnAToken()
    {
        Assert.NotNull(response.Token);
    }

    [Then(@"the token should be valid")]
    public void ThenTheTokenShouldBeValid()
    {
        Assert.Equal(expectedToken, response.Token);
    }
}
