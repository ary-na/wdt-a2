using System.ComponentModel.DataAnnotations;
using s3910902_a2.Models;
using s3910902_a2.Utilities;

namespace s3910902_a2.Validations;

public class DecimalPlacesAttribute : ValidationAttribute
{
    private static string GetErrorMessage() => "Amount cannot have more than 2 decimal places.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var transaction = (Transaction)validationContext.ObjectInstance;

        return transaction.Amount.HasMoreThanTwoDecimalPlaces()
            ? new ValidationResult(GetErrorMessage())
            : ValidationResult.Success;
    }
}