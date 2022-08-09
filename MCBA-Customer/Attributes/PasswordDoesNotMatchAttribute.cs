using System.ComponentModel.DataAnnotations;
using MCBA_Customer.ViewModels;

namespace MCBA_Customer.Attributes;

public class PasswordDoesNotMatchAttribute : ValidationAttribute
{
    private static string GetErrorMessage() => "Password doesn't match.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var login = (ChangePasswordViewModel)validationContext.ObjectInstance;
        return login.ConfirmPassword.Equals(login.Password)
            ? ValidationResult.Success
            : new ValidationResult(GetErrorMessage());
    }
}