using FastFoodEmployeeManagement.Domain.Validations;

namespace FastFoodEmployeeManagement.Domain.Exceptions;

public class ValidationException : Exception
{
    public IValidationNotifications ValidationNotifications { get; private set; }

    public ValidationException(IValidationNotifications validationNotifications)
    {
        ValidationNotifications = validationNotifications ?? throw new ArgumentNullException(nameof(validationNotifications));
    }
}
