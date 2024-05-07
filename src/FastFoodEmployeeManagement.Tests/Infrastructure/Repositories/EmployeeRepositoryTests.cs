using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using FastFoodEmployeeManagement.Domain.Entities;
using FastFoodManagement.Infrastructure.Persistance.Repositories;
using Moq;
using System.Net;

namespace FastFoodEmployeeManagement.Tests.Infrastructure.Repositories;

public class EmployeeRepositoryTests
{
    [Fact]
    public async Task AddEmployeeAsync_ValidEmployee_ReturnsTrue()
    {
        // Arrange
        var dynamoDbMock = new Mock<IAmazonDynamoDB>();

        var repository = new EmployeeRepository(dynamoDbMock.Object);
        var cancellationToken = new CancellationToken();
        var employee = new EmployeeEntity { Email = "test@example.com", Password = "password123" };
        var expectedTableName = "TestTable";

        Environment.SetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO", expectedTableName);

        var response = new PutItemResponse { HttpStatusCode = HttpStatusCode.OK };
        dynamoDbMock.Setup(d => d.PutItemAsync(It.IsAny<PutItemRequest>(), cancellationToken))
                    .ReturnsAsync(response);

        // Act
        var result = await repository.AddEmployeeAsync(employee, cancellationToken);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetEmployeeByEmailAsync_ExistingEmployee_ReturnsEmployee()
    {
        // Arrange
        var dynamoDbMock = new Mock<IAmazonDynamoDB>();

        var repository = new EmployeeRepository(dynamoDbMock.Object);
        var cancellationToken = new CancellationToken();
        var email = "test@example.com";
        var expectedTableName = "TestTable";

        Environment.SetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO", expectedTableName);

        var response = new ScanResponse
        {
            Items = new List<Dictionary<string, AttributeValue>>
                {
                    new Dictionary<string, AttributeValue>
                    {
                        { "email", new AttributeValue { S = email } },
                        { "password", new AttributeValue { S = "password123" } }
                        // Add other attributes as needed
                    }
                }
        };
        dynamoDbMock.Setup(d => d.ScanAsync(It.IsAny<ScanRequest>(), cancellationToken))
                    .ReturnsAsync(response);

        // Act
        var result = await repository.GetEmployeeByEmailAsync(email, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
    }

    [Fact]
    public async Task GetEmployeeByEmailAsync_NonExistingEmployee_ReturnsNull()
    {
        // Arrange
        var dynamoDbMock = new Mock<IAmazonDynamoDB>();

        var repository = new EmployeeRepository(dynamoDbMock.Object);
        var cancellationToken = new CancellationToken();
        var email = "nonexisting@example.com";
        var expectedTableName = "TestTable";

        Environment.SetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO", expectedTableName);

        var response = new ScanResponse { Items = new List<Dictionary<string, AttributeValue>>() };
        dynamoDbMock.Setup(d => d.ScanAsync(It.IsAny<ScanRequest>(), cancellationToken))
                    .ReturnsAsync(response);

        // Act
        var result = await repository.GetEmployeeByEmailAsync(email, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEmployeesAsync_ExistingEmployees_ReturnsEmployees()
    {
        // Arrange
        var dynamoDbMock = new Mock<IAmazonDynamoDB>();

        var repository = new EmployeeRepository(dynamoDbMock.Object);
        var cancellationToken = new CancellationToken();
        var expectedTableName = "TestTable";

        Environment.SetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO", expectedTableName);

        var response = new ScanResponse
        {
            Items = new List<Dictionary<string, AttributeValue>>
                {
                    new Dictionary<string, AttributeValue>
                    {
                        { "email", new AttributeValue { S = "test1@example.com" } },
                        { "password", new AttributeValue { S = "password1" } }
                    },
                    new Dictionary<string, AttributeValue>
                    {
                        { "email", new AttributeValue { S = "test2@example.com" } },
                        { "password", new AttributeValue { S = "password2" } }
                    }
                }
        };
        dynamoDbMock.Setup(d => d.ScanAsync(It.IsAny<ScanRequest>(), cancellationToken))
                    .ReturnsAsync(response);

        // Act
        var result = await repository.GetEmployeesAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetEmployeesAsync_NoEmployees_ReturnsNull()
    {
        // Arrange
        var dynamoDbMock = new Mock<IAmazonDynamoDB>();

        var repository = new EmployeeRepository(dynamoDbMock.Object);
        var cancellationToken = new CancellationToken();
        var expectedTableName = "TestTable";

        Environment.SetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO", expectedTableName);

        var response = new ScanResponse { Items = new List<Dictionary<string, AttributeValue>>() };
        dynamoDbMock.Setup(d => d.ScanAsync(It.IsAny<ScanRequest>(), cancellationToken))
                    .ReturnsAsync(response);

        // Act
        var result = await repository.GetEmployeesAsync(cancellationToken);

        // Assert
        Assert.Null(result);
    }
}
