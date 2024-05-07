using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using FastFoodEmployeeManagement.Domain.Entities;
using FastFoodEmployeeManagement.Infrastructure.Cognito.Creation;
using Moq;

namespace FastFoodEmployeeManagement.Tests.Infrastructure.Cognito;

public class CognitoEmployeeCreationTests
{
    AWSCredentials credentials = new BasicAWSCredentials("trest", "test");

    [Fact]
    public async Task CreateEmployee_ValidUser_ReturnsUsername()
    {
        // Arrange
        var cognitoMock = new Mock<AmazonCognitoIdentityProviderClient>(credentials, Amazon.RegionEndpoint.USEast1);
        var user = new EmployeeEntity { Email = "test@example.com", Password = "password123", Name = "teste" };
        var cancellationToken = new CancellationToken();
        var expectedUsername = "test@example.com";
        var expectedUserPoolId = "testUserPoolId";

        var userCreation = new CognitoEmployeeCreation(cognitoMock.Object);

        Environment.SetEnvironmentVariable("AWS_USER_POOL_ID", expectedUserPoolId);

        cognitoMock.Setup(x => x.AdminCreateUserAsync(It.IsAny<AdminCreateUserRequest>(), cancellationToken))
                   .ReturnsAsync(new AdminCreateUserResponse { User = new UserType { Username = expectedUsername } });

        cognitoMock.Setup(x => x.AdminSetUserPasswordAsync(It.IsAny<AdminSetUserPasswordRequest>(), cancellationToken))
                   .ReturnsAsync(new AdminSetUserPasswordResponse());

        // Act
        var result = await userCreation.CreateEmployee(user, cancellationToken);

        // Assert
        Assert.Equal(expectedUsername, result);
    }
}
