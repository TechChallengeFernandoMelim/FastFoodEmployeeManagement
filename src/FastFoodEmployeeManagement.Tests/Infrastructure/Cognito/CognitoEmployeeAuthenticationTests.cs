using Amazon.CognitoIdentityProvider.Model;
using Amazon.CognitoIdentityProvider;
using FastFoodEmployeeManagement.Domain.Entities;
using FastFoodEmployeeManagement.Infrastructure.Cognito.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Net;
using Amazon.Runtime;

namespace FastFoodEmployeeManagement.Tests.Infrastructure.Cognito;

public class CognitoEmployeeAuthenticationTests
{
    AWSCredentials credentials = new BasicAWSCredentials("trest", "test");

    [Fact]
    public async Task AuthenticateEmployee_ValidEmployee_ReturnsToken()
    {
        // Arrange
        var cognitoMock = new Mock<AmazonCognitoIdentityProviderClient>(credentials, Amazon.RegionEndpoint.USEast1);
        var cacheMock = new Mock<IMemoryCache>();

        var employee = new EmployeeEntity { Email = "test@example.com", Password = "password123" };
        var cancellationToken = new CancellationToken();
        var expectedToken = "testToken";
        var expectedUserPoolId = "testEmployeePoolId";
        var expectedClientId = "testClientId";

        Environment.SetEnvironmentVariable("AWS_EMPLOYEE_POOL_ID", expectedUserPoolId);
        Environment.SetEnvironmentVariable("AWS_CLIENT_ID_COGNITO", expectedClientId);

        cognitoMock.Setup(x => x.AdminInitiateAuthAsync(It.IsAny<AdminInitiateAuthRequest>(), cancellationToken))
                   .ReturnsAsync(new AdminInitiateAuthResponse { AuthenticationResult = new AuthenticationResultType { IdToken = expectedToken } });

        var authentication = new CognitoEmployeeAuthentication(cognitoMock.Object, cacheMock.Object);

        // Act
        var result = await authentication.AuthenticateEmployee(employee, cancellationToken);

        // Assert
        Assert.Equal(expectedToken, result);
    }
}
