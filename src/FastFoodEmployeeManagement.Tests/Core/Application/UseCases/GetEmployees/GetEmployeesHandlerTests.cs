using AutoMapper;
using FastFoodEmployeeManagement.Application.UseCases.GetEmployees;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Entities;
using Moq;

namespace FastFoodEmployeeManagement.Tests.Core.Application.UseCases.GetEmployees;

public class GetEmployeesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsEmployeesSuccessfully()
    {
        // Arrange
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var mapperMock = new Mock<IMapper>();
        var validator = new GetEmployeesValidator();
        var handler = new GetEmployeesHandler(EmployeeRepositoryMock.Object, mapperMock.Object);
        var cancellationToken = new CancellationToken();

        var expectedEmployees = new List<EmployeeEntity>
            {
                new EmployeeEntity { Name = "John Doe", Email = "john@example.com" },
                new EmployeeEntity { Name = "Jane Smith", Email = "jane@example.com" }
            };

        EmployeeRepositoryMock.Setup(r => r.GetEmployeesAsync(cancellationToken)).ReturnsAsync(expectedEmployees);

        mapperMock.Setup(m => m.Map<IEnumerable<Employee>>(expectedEmployees)).Returns(expectedEmployees.Select(e => new Employee()));

        // Act
        var response = await handler.Handle(new GetEmployeesRequest(), cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(expectedEmployees.Count, response.Employees.Count());
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoEmployeesFound()
    {
        // Arrange
        var EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        var mapperMock = new Mock<IMapper>();

        var handler = new GetEmployeesHandler(EmployeeRepositoryMock.Object, mapperMock.Object);
        var cancellationToken = new CancellationToken();

        var expectedEmployees = new List<EmployeeEntity>();

        EmployeeRepositoryMock.Setup(r => r.GetEmployeesAsync(cancellationToken)).ReturnsAsync(expectedEmployees);

        // Act
        var response = await handler.Handle(new GetEmployeesRequest(), cancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response.Employees);
    }
}
