using System.ComponentModel.DataAnnotations;
using s3910902_a2.Models;
using s3910902_a2.Utilities;

namespace s3910902_a2.Validations;

// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
// https://www.jetbrains.com/help/resharper/ConvertIfStatementToReturnStatement.html

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