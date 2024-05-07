using FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

namespace FastFoodEmployeeManagement.Tests.Core.Application.UseCases.AuthenticateEmployee;

public class AuthenticateEmployeeValidatorTests
{
    private readonly AuthenticateEmployeeValidator _validator;

    public AuthenticateEmployeeValidatorTests()
    {
        _validator = new AuthenticateEmployeeValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_Email_NullOrEmpty_ShouldHaveValidationError(string email)
    {
        // Arrange
        var request = new AuthenticateEmployeeRequest(email, "password");

        // Act & Assert
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(AuthenticateEmployeeRequest.Email));
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("invalidemail@")]
    [InlineData("invalidemail.asdas")]
    public void Validate_Email_InvalidFormat_ShouldHaveValidationError(string email)
    {
        // Arrange
        var request = new AuthenticateEmployeeRequest(email, "password");

        // Act & Assert
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(AuthenticateEmployeeRequest.Email));
    }

    [Theory]
    [InlineData("validemail@example.com")]
    [InlineData("valid.email@example.com")]
    [InlineData("valid-email@example.com")]
    public void Validate_Email_ValidFormat_ShouldNotHaveValidationError(string email)
    {
        // Arrange
        var request = new AuthenticateEmployeeRequest(email, "passwordddd");

        // Act & Assert
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_Password_NullOrEmpty_ShouldHaveValidationError(string password)
    {
        // Arrange
        var request = new AuthenticateEmployeeRequest("email@example.com", password);

        // Act & Assert
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(AuthenticateEmployeeRequest.Password));
    }

    [Theory]
    [InlineData("1234567890")] // Password with 10 characters
    [InlineData("123456789012")] // Password with 12 characters
    public void Validate_Password_InvalidLength_ShouldHaveValidationError(string password)
    {
        // Arrange
        var request = new AuthenticateEmployeeRequest("email@example.com", password);

        // Act & Assert
        var result = _validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(AuthenticateEmployeeRequest.Password));
    }

    [Fact]
    public void Validate_Password_ValidLength_ShouldNotHaveValidationError()
    {
        // Arrange
        var request = new AuthenticateEmployeeRequest("email@example.com", "12345678901"); // Password with 11 characters

        // Act & Assert
        var result = _validator.Validate(request);
        Assert.True(result.IsValid);
    }
}
