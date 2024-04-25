namespace FastFoodEmployeeManagement.Application.UseCases.GetEmployees;

public sealed record GetEmployeesResponse
{
    public IEnumerable<Employee> Employees { get; set; }
}

public record Employee
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Identification { get; set; }
}
