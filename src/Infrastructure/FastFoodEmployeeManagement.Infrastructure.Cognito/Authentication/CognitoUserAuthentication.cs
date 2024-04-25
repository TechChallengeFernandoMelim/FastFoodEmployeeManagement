﻿using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace FastFoodEmployeeManagement.Infrastructure.Cognito.Authentication;

public class CognitoEmployeeAuthentication(AmazonCognitoIdentityProviderClient cognito, IMemoryCache cache) : IEmployeeAuthentication
{
    public async Task<string> AuthenticateEmployee(EmployeeEntity Employee, CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(Employee.Identification, out string cachedToken))
            return cachedToken;

        var userPoolId = Environment.GetEnvironmentVariable("AWS_EMPLOYEE_POOL_ID");
        var clientId = Environment.GetEnvironmentVariable("AWS_CLIENT_ID_COGNITO");

        var authParameters = new Dictionary<string, string>
        {
            { "USERNAME", Employee.Email },
            { "PASSWORD", Employee.Identification }
        };

        var request = new AdminInitiateAuthRequest()
        {
            AuthParameters = authParameters,
            ClientId = clientId,
            AuthFlow = "ADMIN_USER_PASSWORD_AUTH",
            UserPoolId = userPoolId
        };

        var response = await cognito.AdminInitiateAuthAsync(request, cancellationToken);

        cache.Set(Employee.Identification, response.AuthenticationResult.IdToken, TimeSpan.FromMinutes(30));

        return response.AuthenticationResult.IdToken;
    }
}