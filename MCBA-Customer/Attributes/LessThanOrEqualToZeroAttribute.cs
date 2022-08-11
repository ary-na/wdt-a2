using System.ComponentModel.DataAnnotations;
using MCBA_Model.Utilities;

namespace MCBA_Customer.Attributes;

public class LessThanOrEqualToZeroAttribute : ValidationAttribute
{
    private static string GetErrorMessage() => "Amount is less than or equal to zero.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var amount = (decimal)(value ?? throw new NullReferenceException());
        return amount.LessThanOrEqualToZero()
            ? new ValidationResult(GetErrorMessage())
            : ValidationResult.Success;
    }
}