namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

public sealed record AuthenticateEmployeeResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
