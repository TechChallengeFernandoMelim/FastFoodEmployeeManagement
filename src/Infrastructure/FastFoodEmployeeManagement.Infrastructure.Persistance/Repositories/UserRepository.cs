using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using FastFoodEmployeeManagement.Domain.Contracts.Repositories;
using FastFoodEmployeeManagement.Domain.Entities;
using System.Net;
using System.Text.Json;

namespace FastFoodManagement.Infrastructure.Persistance.Repositories;

public class EmployeeRepository(IAmazonDynamoDB dynamoDb) : IEmployeeRepository
{
    public async Task<bool> AddEmployeeAsync(EmployeeEntity Employee, CancellationToken cancellationToken)
    {
        var EmployeeAsJson = JsonSerializer.Serialize(Employee);
        var itemAsDocument = Document.FromJson(EmployeeAsJson);
        var itemAsAttribute = itemAsDocument.ToAttributeMap();

        var createItemRequest = new PutItemRequest
        {
            TableName = Environment.GetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO"),
            Item = itemAsAttribute
        };

        var response = await dynamoDb.PutItemAsync(createItemRequest, cancellationToken);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<EmployeeEntity> GetEmployeeByCPFOrEmailAsync(string identification, string email, CancellationToken cancellationToken)
    {
        var request = new ScanRequest
        {
            TableName = Environment.GetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO"),
            FilterExpression = "identification = :identification OR email = :email",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":identification", new AttributeValue { S = identification } },
                { ":email", new AttributeValue { S = email } }
            }
        };

        var response = await dynamoDb.ScanAsync(request, cancellationToken);

        if (response.Items.Count == 0)
            return null;

        var itemAsDocument = Document.FromAttributeMap(response.Items.First());
        return JsonSerializer.Deserialize<EmployeeEntity>(itemAsDocument.ToJson());
    }

    public async Task<IEnumerable<EmployeeEntity>> GetEmployeesAsync(CancellationToken cancellationToken)
    {
        var request = new ScanRequest
        {
            TableName = Environment.GetEnvironmentVariable("AWS_TABLE_NAME_DYNAMO")
        };

        var response = await dynamoDb.ScanAsync(request, cancellationToken);
        if (response.Items.Count == 0)
            return null;

        var Employees = response.Items.Select(item =>
        {
            return new EmployeeEntity
            {
                Identification = item.ContainsKey("identification") ? item["identification"].S : null,
                Name = item.ContainsKey("name") ? item["name"].S : null,
                Email = item.ContainsKey("email") ? item["email"].S : null,
                CognitoEmployeeIdentification = item.ContainsKey("clientid") ? item["clientid"].S : null
            };
        });

        return Employees;
    }
}
