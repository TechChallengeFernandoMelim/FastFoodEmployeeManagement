using AutoMapper;
using FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Entities;
using FastFoodEmployeeManagement.Domain.Exceptions;
using Moq;

namespace FastFoodEmployeeManagement.Tests.Core.Application.UseCases.AuthenticateEmployee;

public class AuthenticateEmployeeHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsResponseWithToken()
    {
        // Arrange
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var EmployeeAuthenticationMock = new Mock<IEmployeeAuthentication>();
        var mapperMock = new Mock<IMapper>();

        var handler = new AuthenticateEmployeeHandler(EmployeeRepositoryMock.Object, mapperMock.Object, EmployeeAuthenticationMock.Object);
        var cancellationToken = new CancellationToken();
        var request = new AuthenticateEmployeeRequest("test@example.com", "password");

        var EmployeeEntity = new EmployeeEntity { Email = "test@example.com", Password = "password" };
        var expectedToken = "testToken";

        EmployeeRepositoryMock.Setup(u => u.GetEmployeeByEmailAsync(request.Email, cancellationToken))
            .ReturnsAsync(EmployeeEntity);

        EmployeeAuthenticationMock.Setup(u => u.AuthenticateEmployee(EmployeeEntity, cancellationToken))
            .ReturnsAsync(expectedToken);

        var expectedResponse = new AuthenticateEmployeeResponse { Email = EmployeeEntity.Email, Token = expectedToken };
        mapperMock.Setup(m => m.Map<AuthenticateEmployeeResponse>(EmployeeEntity)).Returns(expectedResponse);

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.Equal(expectedResponse.Email, response.Email);
        Assert.Equal(expectedResponse.Token, response.Token);
    }

    [Fact]
    public async Task Handle_EmployeeNotFound_ThrowsObjectNotFoundException()
    {
        // Arrange
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var EmployeeAuthenticationMock = new Mock<IEmployeeAuthentication>();
        var mapperMock = new Mock<IMapper>();

        var handler = new AuthenticateEmployeeHandler(EmployeeRepositoryMock.Object, mapperMock.Object, EmployeeAuthenticationMock.Object);
        var cancellationToken = new CancellationToken();
        var request = new AuthenticateEmployeeRequest("test@example.com", "password");

        EmployeeRepositoryMock.Setup(u => u.GetEmployeeByEmailAsync(request.Email, cancellationToken))
            .ReturnsAsync((EmployeeEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<ObjectNotFoundException>(() => handler.Handle(request, cancellationToken));
    }

    [Fact]
    public async Task Handle_AuthenticationFails_ThrowsException()
    {
        // Arrange
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var EmployeeAuthenticationMock = new Mock<IEmployeeAuthentication>();
        var mapperMock = new Mock<IMapper>();

        var handler = new AuthenticateEmployeeHandler(EmployeeRepositoryMock.Object, mapperMock.Object, EmployeeAuthenticationMock.Object);
        var cancellationToken = new CancellationToken();
        var request = new AuthenticateEmployeeRequest("test@example.com", "password");

        var EmployeeEntity = new EmployeeEntity { Email = "test@example.com", Password = "password" };
        EmployeeRepositoryMock.Setup(u => u.GetEmployeeByEmailAsync(request.Email, cancellationToken))
            .ReturnsAsync(EmployeeEntity);

        EmployeeAuthenticationMock.Setup(u => u.AuthenticateEmployee(EmployeeEntity, cancellationToken))
            .ThrowsAsync(new Exception("Test exception"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, cancellationToken));
    }
}
