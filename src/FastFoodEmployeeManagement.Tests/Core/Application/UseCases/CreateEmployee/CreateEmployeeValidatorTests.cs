using FastFoodEmployeeManagement.Application.UseCases.CreateEmployee;
using FluentValidation.TestHelper;

namespace FastFoodEmployeeManagement.Tests.Core.Application.UseCases.CreateEmployee;

public class CreateEmployeeValidatorTests
{
    private readonly CreateEmployeeValidator _validator;

    public CreateEmployeeValidatorTests()
    {
        _validator = new CreateEmployeeValidator();
    }

    [Fact]
    public void Name_WhenLengthLessThan3_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest("ab", "test@example.com", "12345678901");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("O nome deve ter no minimo 3 e no máximo 255 caracteres.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Name_WhenLengthGreaterThan255_ShouldFailValidation()
    {
        // Arrange
        var longName = new string('a', 256);
        var request = new CreateEmployeeRequest(longName, "test@example.com", "12345678901");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("O nome deve ter no minimo 3 e no máximo 255 caracteres.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Name_WhenNullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest(null, "test@example.com", "12345678901");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("O nome deve estar preenchido.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Email_WhenInvalidFormat_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest("John Doe", "invalidemail", "12345678901");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("O e-mail fornecido não é válido.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Email_WhenNullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest("John Doe", null, "12345678901");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("O e-mail deve estar preenchido.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Password_WhenLengthLessThan11_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest("John Doe", "test@example.com", "1234567890");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("A senha deve ter 11 caracteres.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Password_WhenLengthGreaterThan11_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest("John Doe", "test@example.com", "123456789012");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("A senha deve ter 11 caracteres.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Password_WhenNullOrEmpty_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateEmployeeRequest("John Doe", "test@example.com", null);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("A senha deve estar preenchida.", result.Errors[0].ErrorMessage);
    }
}
