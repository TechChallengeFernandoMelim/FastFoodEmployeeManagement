using System.Text.Json.Serialization;

namespace FastFoodEmployeeManagement.Domain.Entities;

public class EmployeeEntity
{
    [JsonPropertyName("pk")]
    public string Pk => Email;

    [JsonPropertyName("sk")]
    public string Sk => Pk;

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("cognitoEmployeeIdentification")]
    public string CognitoEmployeeIdentification { get; set; }
}
