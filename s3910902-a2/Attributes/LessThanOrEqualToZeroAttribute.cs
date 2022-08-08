using System.ComponentModel.DataAnnotations;
using s3910902_a2.Utilities;

namespace s3910902_a2.Attributes;

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