using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Entities;

namespace FastFoodEmployeeManagement.Infrastructure.Cognito.Creation;

public class CognitoEmployeeCreation(AmazonCognitoIdentityProviderClient cognito) : IEmployeeCreation
{
    public async Task<string> CreateEmployee(EmployeeEntity Employee, CancellationToken cancellationToken)
    {
        var EmployeePoolId = Environment.GetEnvironmentVariable("AWS_EMPLOYEE_POOL_ID");

        var request = new AdminCreateUserRequest()
        {
            UserPoolId = EmployeePoolId,
            Username = Employee.Email
        };

        var response = await cognito.AdminCreateUserAsync(request, cancellationToken);

        var setPassword = new AdminSetUserPasswordRequest()
        {
            Password = Employee.Identification,
            Username = Employee.Email,
            UserPoolId = EmployeePoolId,
            Permanent = true
        };

        await cognito.AdminSetUserPasswordAsync(setPassword, cancellationToken);

        return response.User.Username;
    }
}
