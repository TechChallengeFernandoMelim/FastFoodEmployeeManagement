using AutoMapper;
using FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Entities;
using FastFoodEmployeeManagement.Domain.Exceptions;
using FastFoodEmployeeManagement.Domain.Validations;
using Moq;

namespace FastFoodEmployeeManagement.Tests.Core.Application.UseCases.CreateEmployee;

public class CreateEmployeeHandlerTests
{
    [Fact]
    public async Task Handle_NewEmployee_CreatedSuccessfully()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var validationNotificationsMock = new Mock<IValidationNotifications>();
        var EmployeeCreationMock = new Mock<IEmployeeCreation>();

        var request = new CreateEmployeeRequest("John Doe", "john@example.com", "12345678901");
        var cancellationToken = new CancellationToken();

        var handler = new CreateEmployeeHandler(EmployeeRepositoryMock.Object, mapperMock.Object, validationNotificationsMock.Object, EmployeeCreationMock.Object);

        mapperMock.Setup(m => m.Map<EmployeeEntity>(request)).Returns(new EmployeeEntity());

        EmployeeRepositoryMock.Setup(r => r.GetEmployeeByEmailAsync(It.IsAny<string>(), cancellationToken)).ReturnsAsync((EmployeeEntity)null);

        EmployeeCreationMock.Setup(ec => ec.CreateEmployee(It.IsAny<EmployeeEntity>(), cancellationToken)).ReturnsAsync("cognitoId");

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(response);
        EmployeeRepositoryMock.Verify(r => r.AddEmployeeAsync(It.IsAny<EmployeeEntity>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingEmployee_AddValidationError()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var validationNotificationsMock = new Mock<IValidationNotifications>();
        var EmployeeCreationMock = new Mock<IEmployeeCreation>();

        var existingEmployee = new EmployeeEntity();
        var request = new CreateEmployeeRequest("John Doe", "john@example.com", "12345678901");
        var cancellationToken = new CancellationToken();

        var handler = new CreateEmployeeHandler(EmployeeRepositoryMock.Object, mapperMock.Object, validationNotificationsMock.Object, EmployeeCreationMock.Object);

        mapperMock.Setup(m => m.Map<EmployeeEntity>(request)).Returns(existingEmployee);

        EmployeeRepositoryMock.Setup(r => r.GetEmployeeByEmailAsync(It.IsAny<string>(), cancellationToken)).ReturnsAsync(existingEmployee);

        var response = await handler.Handle(request, cancellationToken);

        // Act & Assert
        Assert.NotNull(response);
        validationNotificationsMock.Verify(r => r.AddError(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}
