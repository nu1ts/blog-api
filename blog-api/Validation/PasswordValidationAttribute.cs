using System.ComponentModel.DataAnnotations;

namespace blog_api.Validation;

public class PasswordValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is string password && !string.IsNullOrEmpty(password) && password.Any(char.IsDigit);
    }

    public override string FormatErrorMessage(string name)
    {
        return "Password must contain at least one digit";
    }
}