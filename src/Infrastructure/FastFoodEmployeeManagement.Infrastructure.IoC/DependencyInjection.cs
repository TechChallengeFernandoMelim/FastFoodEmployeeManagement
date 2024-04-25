using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using FastFoodManagement.Infrastructure.Persistance.Repositories;
using FastFoodEmployeeManagement.Application.UseCases;
using FastFoodEmployeeManagement.Domain.Contracts.Authentication;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Validations;
using FastFoodEmployeeManagement.Infrastructure.Cognito.Authentication;
using FastFoodEmployeeManagement.Infrastructure.Cognito.Creation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Amazon;
using Amazon.SQS;
using FastFoodEmployeeManagement.Domain.Contracts.Logs;
using FastFoodEmployeeManagement.Infrastructure.SQS.Logger;

namespace FastFoodEmployeeManagement.Infrastructure.IoC;

public static class DependencyInjection
{
    private static string pathToApplicationAssembly = Path.Combine(AppContext.BaseDirectory, "FastFoodEmployeeManagement.Application.dll");

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureCognito(services);
        ConfigureRepositories(services);
        ConfigureDatabase(services);
        ConfigureNotificationServices(services);
        ConfigureValidators(services);
        ConfigureMediatr(services);
        ConfigureAutomapper(services);
        ConfigureSQS(services);
    }

    private static void ConfigureSQS(IServiceCollection services)
    {
        string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO");
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO");

        AWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);
        var sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);

        services.AddSingleton(sqsClient);
        services.AddSingleton<ILogger, Logger>();
    }

    private static void ConfigureCognito(IServiceCollection services)
    {
        string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO");
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO");

        AWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);

        var cognitoProvider = new AmazonCognitoIdentityProviderClient(credentials, Amazon.RegionEndpoint.USEast1);

        services.AddSingleton(cognitoProvider);
        services.AddSingleton<IEmployeeCreation, CognitoEmployeeCreation>();
        services.AddSingleton<IEmployeeAuthentication, CognitoEmployeeAuthentication>();
    }

    private static void ConfigureAutomapper(IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.LoadFrom(pathToApplicationAssembly));
    }

    private static void ConfigureMediatr(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.LoadFrom(pathToApplicationAssembly)));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void ConfigureDatabase(IServiceCollection services)
    {
        var clientConfig = new AmazonDynamoDBConfig();
        clientConfig.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
        string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO");
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO");

        AWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);

        services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(credentials, clientConfig));
    }

    private static void ConfigureRepositories(IServiceCollection services)
        => services.AddScoped<IEmployeeRepository, EmployeeRepository>();

    private static void ConfigureNotificationServices(IServiceCollection services)
        => services.AddScoped<IValidationNotifications, ValidationNotifications>();


    private static void ConfigureValidators(IServiceCollection services)
        => services.AddValidatorsFromAssembly(Assembly.LoadFrom(pathToApplicationAssembly));
}

