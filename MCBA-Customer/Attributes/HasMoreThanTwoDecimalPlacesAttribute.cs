using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Utilities;

namespace MCBA_Customer.Attributes;

// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
// https://www.jetbrains.com/help/resharper/ConvertIfStatementToReturnStatement.html
// https://stackoverflow.com/questions/23780943/how-to-create-custom-validation-attribute

public class HasMoreThanTwoDecimalPlacesAttribute : ValidationAttribute
{
    private static string GetErrorMessage() => "Amount cannot have more than two decimal places.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var amount = (decimal)(value ?? throw new NullReferenceException());
        return amount.HasMoreThanTwoDecimalPlaces()
            ? new ValidationResult(GetErrorMessage())
            : ValidationResult.Success;
    }
}